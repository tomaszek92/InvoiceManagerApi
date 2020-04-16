using AutoFixture;
using InvoiceManagerApi.Models;
using System;
using System.Linq;

namespace InvoiceManagerApi.UnitTests
{
    public class ApplicationDbContextUnitTests : IDisposable
    {
        protected readonly IFixture Fixture;
        protected readonly TestApplicationDbContextProvider _testApplicationDbContextProvider = new TestApplicationDbContextProvider();
        protected readonly IApplicationDbContext DbContext;

        protected ApplicationDbContextUnitTests()
        {
            Fixture = UnitTestFixture.Get();
            DbContext = _testApplicationDbContextProvider.DbContextInstance;
        }

        public void Dispose()
        {
            _testApplicationDbContextProvider.Dispose();
        }
    }
}
