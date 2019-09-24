using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms
{
    public static class Functor
    {
        public static KeyValuePair<E, B> Select<A, B, E>(this KeyValuePair<E, A> pair, Func<A, B> func) =>
            KeyValuePair.Create(pair.Key, func(pair.Value));
    }
}
