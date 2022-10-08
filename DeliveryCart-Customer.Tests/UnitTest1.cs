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
        public async Task GetMessagesAsync_MessagesAreReturned()
        {
            using (var db = new DeliveryCartContext(Utilities.TestDbContextOptions()))
            {
                // Arrange
                var expectedMessages = DeliveryCartContext.GetSeedingMessages();
                await db.AddRangeAsync(expectedMessages);
                await db.SaveChangesAsync();

                // Act
                var result = await db.GetMessagesAsync();

                // Assert
                var actualMessages = Assert.IsAssignableFrom<List<Customer>>(result);
                Assert.Equal(
                    expectedMessages.OrderBy(m => m.customerID).Select(m => m.customerName),
                    actualMessages.OrderBy(m => m.customerID).Select(m => m.customerName));
            }
        }

        [Fact]
        public async Task AddMessageAsync_MessageIsAdded()
        {
            using (var db = new DeliveryCartContext(Utilities.TestDbContextOptions()))
            {
                // Arrange
                var recId = 10;
                var expectedMessage = new Customer() { customerID = recId, customerName = "Name" };

                // Act
                await db.AddMessageAsync(expectedMessage);

                // Assert
                var actualMessage = await db.FindAsync<Customer>(recId);
                Assert.Equal(expectedMessage, actualMessage);
            }
        }

        [Fact]
        public async Task DeleteAllMessagesAsync_MessagesAreDeleted()
        {
            using (var db = new DeliveryCartContext(Utilities.TestDbContextOptions()))
            {
                // Arrange
                var seedMessages = DeliveryCartContext.GetSeedingMessages();
                await db.AddRangeAsync(seedMessages);
                await db.SaveChangesAsync();

                // Act
                await db.DeleteAllMessagesAsync();

                // Assert
                Assert.Empty(await db.Customer.AsNoTracking().ToListAsync());
            }
        }

        [Fact]
        public async Task DeleteMessageAsync_MessageIsDeleted_WhenMessageIsFound()
        {
            using (var db = new DeliveryCartContext(Utilities.TestDbContextOptions()))
            {
                #region snippet1
                // Arrange
                var seedMessages = DeliveryCartContext.GetSeedingMessages();
                await db.AddRangeAsync(seedMessages);
                await db.SaveChangesAsync();
                var recId = 1;
                var expectedMessages =
                    seedMessages.Where(customer => customer.customerID != recId).ToList();
                #endregion

                #region snippet2
                // Act
                await db.DeleteMessageAsync(recId);
                #endregion

                #region snippet3
                // Assert
                var actualMessages = await db.Customer.AsNoTracking().ToListAsync();
                Assert.Equal(
                    expectedMessages.OrderBy(m => m.customerID).Select(m => m.customerName),
                    actualMessages.OrderBy(m => m.customerID).Select(m => m.customerName));
                #endregion
            }
        }

        #region snippet4
        [Fact]
        public async Task DeleteMessageAsync_NoMessageIsDeleted_WhenMessageIsNotFound()
        {
            using (var db = new DeliveryCartContext(Utilities.TestDbContextOptions()))
            {
                // Arrange
                var expectedMessages = DeliveryCartContext.GetSeedingMessages();
                await db.AddRangeAsync(expectedMessages);
                await db.SaveChangesAsync();
                var recId = 4;

                // Act
                try
                {
                    await db.DeleteMessageAsync(recId);
                }
                catch
                {
                    // recId doesn't exist
                }

                // Assert
                var actualMessages = await db.Customer.AsNoTracking().ToListAsync();
                Assert.Equal(
                    expectedMessages.OrderBy(m => m.customerID).Select(m => m.customerName),
                    actualMessages.OrderBy(m => m.customerID).Select(m => m.customerName));
            }
        }
        #endregion
    }
}