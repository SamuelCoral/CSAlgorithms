using System;

namespace Algorithms.Utilities
{
    /// <summary>
    /// Provides utilities for functions.
    /// </summary>
    public static class Function
    {
        /// <summary>
        /// Provides a function which ignores the argument and returns a constant value.
        /// </summary>
        /// <typeparam name="A">Argument type to ignore</typeparam>
        /// <typeparam name="B">Return type</typeparam>
        /// <param name="result">Constant value to always return when calling the function</param>
        /// <returns>A constant function with some value</returns>
        public static Func<A, B> Const<A, B>(B result) => _ => result;
    }
}
