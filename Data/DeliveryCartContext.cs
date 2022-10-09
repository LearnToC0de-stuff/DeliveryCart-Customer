using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DeliveryCart.Models;

namespace Data
{
    public class DeliveryCartContext : DbContext
    {
        public DeliveryCartContext (DbContextOptions<DeliveryCartContext> options)
            : base(options)
        {
        }

        public DbSet<DeliveryCart.Models.Customer> Customer { get; set; } = default!;
        public DbSet<DeliveryCart.Models.DeliveryDriver> DeliveryDriver { get; set; } = default!;

        #region snippet1
        public async virtual Task<List<Customer>> GetCustomersAsync()
        {
            return await Customer
                .OrderBy(customer => customer.customerName)
                .AsNoTracking()
                .ToListAsync();
        }
        #endregion

        #region snippet2
        public async virtual Task AddCustomerAsync(Customer customer)
        {
            await Customer.AddAsync(customer);
            await SaveChangesAsync();
        }
        #endregion

        #region snippet3
        public async virtual Task DeleteAllCustomersAsync()
        {
            foreach (Customer customer in Customer)
            {
                Customer.Remove(customer);
            }

            await SaveChangesAsync();
        }
        #endregion

        #region snippet4
        public async virtual Task DeleteCustomerAsync(int id)
        {
            var customer = await Customer.FindAsync(id);

            if (customer != null)
            {
                Customer.Remove(customer);
                await SaveChangesAsync();
            }
        }
        #endregion

        public void Initialize()
        {
            Customer.AddRange(GetSeedingCustomers());
            SaveChanges();
        }

        public static List<Customer> GetSeedingCustomers()
        {
            return new List<Customer>()
            {
                new Customer(){ customerName = "I don't understand." },
                new Customer(){ customerName = "I really don't" },
                new Customer(){ customerName = "Bill" }
            };
        }

    }
}
