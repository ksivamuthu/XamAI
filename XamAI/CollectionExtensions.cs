using System;
using System.Collections.Generic;
using System.Linq;

namespace XamAI
{
    public static class CollectionExtension
    {
        private static Random rng = new Random();

        public static T RandomElement<T>(this ICollection<T> list)
        {
            return list.ElementAt(rng.Next(list.Count));
        }

        public static List<T> Randomize<T>(this List<T> source)
        {
            Random rnd = new Random();
            return source.OrderBy((arg) => rnd.Next()).ToList();
        }
    }
}
