using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Data;
using DeliveryCart.Models;

namespace DeliveryCart_Customer.Tests.UnitTests
{
    public class DataAccessLayerTest
    {
        [Fact]
        public async Task GetCustomersAsync_CustomersAreReturned()
        {
            using (var db = new DeliveryCartContext(Utilities.TestDbContextOptions()))
            {
                // Arrange
                var expectedCustomers = DeliveryCartContext.GetSeedingCustomers();
                await db.AddRangeAsync(expectedCustomers);
                await db.SaveChangesAsync();

                // Act
                var result = await db.GetCustomersAsync();

                // Assert
                var actualCustomers = Assert.IsAssignableFrom<List<Customer>>(result);
                Assert.Equal(
                    expectedCustomers.OrderBy(m => m.customerID).Select(m => m.customerName),
                    actualCustomers.OrderBy(m => m.customerID).Select(m => m.customerName));
            }
        }

        [Fact]
        public async Task AddCustomerAsync_CustomerIsAdded()
        {
            using (var db = new DeliveryCartContext(Utilities.TestDbContextOptions()))
            {
                // Arrange
                var recId = 10;
                var expectedCustomer = new Customer() { customerID = recId, customerName = "Name", customerAddress = "333 South Lane", customerEmail = "ggg@g.com", customerPhoneNumber = 1};

                // Act
                await db.AddCustomerAsync(expectedCustomer);

                // Assert
                var actualCustomer = await db.FindAsync<Customer>(recId);
                Assert.Equal(expectedCustomer, actualCustomer);
            }
        }

        [Fact]
        public async Task DeleteAllCustomersAsync_CustomersAreDeleted()
        {
            using (var db = new DeliveryCartContext(Utilities.TestDbContextOptions()))
            {
                // Arrange
                var seedCustomers = DeliveryCartContext.GetSeedingCustomers();
                await db.AddRangeAsync(seedCustomers);
                await db.SaveChangesAsync();

                // Act
                await db.DeleteAllCustomersAsync();

                // Assert
                Assert.Empty(await db.Customer.AsNoTracking().ToListAsync());
            }
        }

        [Fact]
        public async Task DeleteCustomerAsync_CustomerIsDeleted_WhenCustomerIsFound()
        {
            using (var db = new DeliveryCartContext(Utilities.TestDbContextOptions()))
            {
                #region snippet1
                // Arrange
                var seedCustomers = DeliveryCartContext.GetSeedingCustomers();
                await db.AddRangeAsync(seedCustomers);
                await db.SaveChangesAsync();
                var recId = 1;
                var expectedCustomers =
                    seedCustomers.Where(customer => customer.customerID != recId).ToList();
                #endregion

                #region snippet2
                // Act
                await db.DeleteCustomerAsync(recId);
                #endregion

                #region snippet3
                // Assert
                var actualCustomers = await db.Customer.AsNoTracking().ToListAsync();
                Assert.Equal(
                    expectedCustomers.OrderBy(m => m.customerID).Select(m => m.customerName),
                    actualCustomers.OrderBy(m => m.customerID).Select(m => m.customerName));
                #endregion
            }
        }

        #region snippet4
        [Fact]
        public async Task DeleteCustomerAsync_NoCustomerIsDeleted_WhenCustomerIsNotFound()
        {
            using (var db = new DeliveryCartContext(Utilities.TestDbContextOptions()))
            {
                // Arrange
                var expectedCustomers = DeliveryCartContext.GetSeedingCustomers();
                await db.AddRangeAsync(expectedCustomers);
                await db.SaveChangesAsync();
                var recId = 4;

                // Act
                try
                {
                    await db.DeleteCustomerAsync(recId);
                }
                catch
                {
                    // recId doesn't exist
                }

                // Assert
                var actualCustomers = await db.Customer.AsNoTracking().ToListAsync();
                Assert.Equal(
                    expectedCustomers.OrderBy(m => m.customerID).Select(m => m.customerName),
                    actualCustomers.OrderBy(m => m.customerID).Select(m => m.customerName));
            }
        }
        #endregion
    }
}