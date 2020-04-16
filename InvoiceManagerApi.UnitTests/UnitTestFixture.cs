using AutoFixture;
using System.Linq;

namespace InvoiceManagerApi.UnitTests
{
    public class UnitTestFixture
    {
        public static IFixture Get()
        {
            var fixture = new Fixture();

            fixture
                .Behaviors
                .OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));

            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            return fixture;
        }
    }
}
