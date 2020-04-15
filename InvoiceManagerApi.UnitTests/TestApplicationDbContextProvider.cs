using InvoiceManagerApi.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace InvoiceManagerApi.UnitTests
{
    public class TestApplicationDbContextProvider : IDisposable
    {
        public ApplicationDbContext DbContextInstance { get; }
       
        public TestApplicationDbContextProvider()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            DbContextInstance = new ApplicationDbContext(options);
        }

        public void Dispose()
        {
            DbContextInstance.Dispose();
        }
    }
}
