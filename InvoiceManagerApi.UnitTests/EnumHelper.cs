using System;
using System.Linq;

namespace InvoiceManagerApi.UnitTests
{
    public class EnumHelper
    {
        public static TEnum GetInvalidValue<TEnum>()
            where TEnum : Enum
        {
            var values = Enum
                .GetValues(typeof(TEnum))
                .Cast<int>();

            return (TEnum)(object)(values.Max() + 1);
        }
    }
}
