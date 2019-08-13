using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms
{
    /// <summary>
    /// Provides comonadic implementations forsome interfaces.
    /// </summary>
    public static class Comonad
    {
        /// <summary>
        /// Applies a function over every subsequence of an IEnumerable.
        /// </summary>
        /// <typeparam name="A">Enumerable input type</typeparam>
        /// <typeparam name="B">Enumerable output type</typeparam>
        /// <param name="list">Source enumerable to iterate</param>
        /// <param name="func">Function to apply to every subsequence</param>
        /// <returns>An enumerable with the results of every function application</returns>
        public static IEnumerable<B> Extend<A, B>(this IEnumerable<A> list, Func<IEnumerable<A>, B> func) =>
            list.Any() ? new List<B>() { func(list) }.Concat(list.Skip(1).Extend(func)) : new List<B>();
    }
}
