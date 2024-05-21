using Microsoft.EntityFrameworkCore;
using NotificationMachineApp.Core.Interfaces.Repositories;
using NotificationMachineApp.Core.Models;
using NotificationMachineApp.Core.Models.Interfaces;
using NotificationMachineApp.Infrastructure.Data;

namespace NotificationMachineApp.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ICustomer>> GetQuietCustomersAsync<T>(DateTime startDate, DateTime endDate) where T : class, ICustomer
        {
            if (typeof(T) == typeof(Customer145))
            {
                return await _context.Customers145
                    .Where(c => _context.Events145
                        .Count(e => e.CustomerId == c.UserId && e.EventDate >= startDate && e.EventDate <= endDate) < 3)
                    .Cast<ICustomer>()
                    .ToListAsync();
            }
            else if (typeof(T) == typeof(Customer2))
            {
                return await _context.Customers2
                    .Where(c => _context.Events2
                        .Count(e => e.CustomerId == c.Id && e.EventDate >= startDate && e.EventDate <= endDate) < 3)
                    .Cast<ICustomer>()
                    .ToListAsync();
            }
            else if (typeof(T) == typeof(Customer101))
            {
                return await _context.Customers101
                    .Where(c => _context.Events101
                        .Count(e => e.CustomerId == c.Id && e.EventDate >= startDate && e.EventDate <= endDate) < 3)
                    .Cast<ICustomer>()
                    .ToListAsync();
            }

            throw new ArgumentException("Unsupported customer type");
        }
    }
}
