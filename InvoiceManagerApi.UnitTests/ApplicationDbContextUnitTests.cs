using AutoFixture;
using InvoiceManagerApi.Models;
using System;
using System.Linq;

namespace InvoiceManagerApi.UnitTests
{
    public class ApplicationDbContextUnitTests : IDisposable
    {
        protected readonly Fixture Fixture;
        protected readonly TestApplicationDbContextProvider _testApplicationDbContextProvider = new TestApplicationDbContextProvider();
        protected readonly IApplicationDbContext DbContext;

        protected ApplicationDbContextUnitTests()
        {
            Fixture = new Fixture();
            
            Fixture
                .Behaviors
                .OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => Fixture.Behaviors.Remove(b));

            Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            
            DbContext = _testApplicationDbContextProvider.DbContextInstance;
        }

        public void Dispose()
        {
            _testApplicationDbContextProvider.Dispose();
        }
    }
}
