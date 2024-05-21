using Microsoft.EntityFrameworkCore;
using NotificationMachineApp.Core.Models;

namespace NotificationMachineApp.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Customer145> Customers145 { get; set; }
        public DbSet<Customer2> Customers2 { get; set; }
        public DbSet<Customer101> Customers101 { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<Event145> Events145 { get; set; }
        public DbSet<Event2> Events2 { get; set; }
        public DbSet<Event101> Events101 { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<NotificationBroker> NotificationsBrokers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //// Fluent API configurations if needed

            modelBuilder.Entity<Customer145>(entity =>
            {
                entity.ToTable("Customer_145");
                entity.Property(e => e.UserId).HasMaxLength(128).IsRequired();
                entity.Property(e => e.Name).HasMaxLength(256);
                entity.Property(e => e.Email).HasMaxLength(128).IsRequired();
                entity.Property(e => e.Password).HasMaxLength(128);
            });

            modelBuilder.Entity<Customer2>(entity =>
            {
                entity.ToTable("Customer_2");
                entity.Property(e => e.GivenName).IsRequired();
                entity.Property(e => e.FamilyName).IsRequired();
                entity.Property(e => e.JobPosition).HasMaxLength(128);
                entity.Property(e => e.Email).HasMaxLength(128).IsRequired();
                entity.Property(e => e.PasswordHash).HasMaxLength(128);
            });

            modelBuilder.Entity<Customer101>(entity =>
            {
                entity.ToTable("Customer_101");
                entity.Property(e => e.FirstName).IsRequired();
                entity.Property(e => e.LastName).IsRequired();
                entity.Property(e => e.DateOfBirth).IsRequired();
                entity.Property(e => e.Email).HasMaxLength(128).IsRequired();
                entity.Property(e => e.Salutation).HasMaxLength(10);
                entity.Property(e => e.PasswordHash).HasMaxLength(128);
            });

            modelBuilder.Entity<EventType>(entity =>
            {
                entity.ToTable("EventTypes_2");
                entity.Property(e => e.Name).HasMaxLength(64).IsRequired();
            });

            modelBuilder.Entity<Event145>(entity =>
            {
                entity.ToTable("Events_145");
                entity.Property(e => e.CustomerId).HasMaxLength(128).IsRequired();
                entity.Property(e => e.EventDate).IsRequired();
                entity.Property(e => e.EventType).IsRequired();
            });

            modelBuilder.Entity<Event2>(entity =>
            {
                entity.ToTable("Events_2");
                entity.Property(e => e.CustomerId).IsRequired();
                entity.Property(e => e.EventDate).IsRequired();
                entity.Property(e => e.EventType).IsRequired();
            });

            modelBuilder.Entity<Event101>(entity =>
            {
                entity.ToTable("Events_101");
                entity.Property(e => e.CustomerId).IsRequired();
                entity.Property(e => e.EventDate).IsRequired();
                entity.Property(e => e.EventType).IsRequired();
            });

            modelBuilder.Entity<Tenant>(entity =>
            {
                entity.ToTable("Tenants");
                entity.Property(e => e.OrganisationName).HasMaxLength(128).IsRequired();
            });

            modelBuilder.Entity<NotificationBroker>(entity =>
            {
                entity.ToTable("NotificationsBroker");
                entity.HasNoKey();
                entity.Property(e => e.Email).HasMaxLength(128).IsRequired();
                entity.Property(e => e.FirstName).HasMaxLength(128).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(128).IsRequired();
                entity.Property(e => e.FinHash).HasMaxLength(128);
            });
        }
    }
}
