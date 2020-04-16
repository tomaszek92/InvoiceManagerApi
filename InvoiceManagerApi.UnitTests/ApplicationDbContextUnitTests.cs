using AutoFixture;
using InvoiceManagerApi.Models;
using System;

namespace InvoiceManagerApi.UnitTests
{
    public class ApplicationDbContextUnitTests : IDisposable
    {
        protected readonly Fixture Fixture = new Fixture();
        protected readonly TestApplicationDbContextProvider _testApplicationDbContextProvider = new TestApplicationDbContextProvider();
        protected readonly IApplicationDbContext DbContext;

        protected ApplicationDbContextUnitTests()
        {
            DbContext = _testApplicationDbContextProvider.DbContextInstance;
        }

        public void Dispose()
        {
            _testApplicationDbContextProvider.Dispose();
        }
    }
}
