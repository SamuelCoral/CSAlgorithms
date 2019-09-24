using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms
{
    public static class Monad
    {
        public static KeyValuePair<E, B> SelectMany<A, B, E>(this KeyValuePair<E, A> pair, Func<A, KeyValuePair<E, B>> func) where E : ISemigroup
        {
            KeyValuePair<E, B> res = func(pair.Value);
            return KeyValuePair.Create(pair.Key.Mult(res.Key), res.Value);
        }
    }
}
