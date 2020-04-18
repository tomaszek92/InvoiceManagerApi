using System;
using System.Collections.Generic;

namespace InvoiceManagerApi.UnitTests
{
    public static class IListExtensions
    {
        private static Random _rand = new Random();

        public static T GetRandom<T>(this IList<T> list)
        {
            if (list.Count == 0)
            {
                throw new ArgumentException("List has to have at least one element.", nameof(list));
            }

            return list[_rand.Next(0, list.Count)];
        }
    }
}
