using NotificationMachineApp.Core.Interfaces.Services;
using NotificationMachineApp.Infrastructure.Services;
using NotificationMachineApp.Infrastructure.Kafka;
using Serilog;
using Microsoft.EntityFrameworkCore;
using NotificationMachineApp.Core.Interfaces.Repositories;
using NotificationMachineApp.Infrastructure.Data;
using NotificationMachineApp.Infrastructure.Email;
using NotificationMachineApp.Infrastructure.Repositories;
using Serilog.Events;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Configure logging with Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container
builder.Services.AddControllers();

//Add the BackgroundWorkerService
builder.Services.AddHostedService<BackgroundWorkerService>();

// Add Swagger/OpenAPI support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Entity Framework DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

// Register services
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IClientCodeService, ClientCodeService>();
builder.Services.AddScoped<INotificationService, NotificationService>();

// Configure EmailSettings using IOptions pattern
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

// Register SmtpClient based on EmailSettings
builder.Services.AddScoped<SmtpClient>((serviceProvider) => {
    var emailSettings = serviceProvider.GetRequiredService<IOptions<EmailSettings>>().Value;
    var client = new SmtpClient(emailSettings.SmtpServer)
    {
        Port = emailSettings.SmtpPort,
        Credentials = new NetworkCredential(emailSettings.SmtpUsername, emailSettings.SmtpPassword),
        EnableSsl = true
    };
    return client;
});

// Register EmailService with all required parameters
builder.Services.AddScoped<IEmailService, EmailService>((serviceProvider) => {
    var emailSettings = serviceProvider.GetRequiredService<IOptions<EmailSettings>>().Value;
    var smtpClient = new SmtpClient(emailSettings.SmtpServer)
    {
        Port = emailSettings.SmtpPort,
        Credentials = new NetworkCredential(emailSettings.SmtpUsername, emailSettings.SmtpPassword),
        EnableSsl = true
    };
    var logger = serviceProvider.GetRequiredService<ILogger<EmailService>>();
    return new EmailService(smtpClient, emailSettings.FromEmail, logger);
});


// Configure Kafka Producer and Consumer Services
var kafkaBootstrapServers = builder.Configuration["Kafka:BootstrapServers"];
var kafkaGroupId = builder.Configuration["Kafka:GroupId"];
builder.Services.AddSingleton<IKafkaProducerService>(provider =>
    new KafkaProducerService(kafkaBootstrapServers, 
    provider.GetRequiredService<ILogger<KafkaProducerService>>()));
builder.Services.AddSingleton<IKafkaConsumerService>(provider =>
    new KafkaConsumerService(
        kafkaBootstrapServers,
        kafkaGroupId,
        provider.GetRequiredService<IServiceScopeFactory>(),
        provider.GetRequiredService<ILogger<KafkaConsumerService>>()
    ));

var app = builder.Build();

// HTTP request pipeline setup
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();
app.Run();
