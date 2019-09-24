using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms
{
    /// <summary>
    /// An interface for an abstract object with an operation and a property which
    /// should satisfy the associativity law.
    /// If s1, s2 & s3 are semigroups, then
    /// 
    /// s1.Mult(s2.Mult(s3)) == (s1.Mult(s2)).Mult(s3)
    /// </summary>
    public interface ISemigroup
    {
        T Mult<T>(T m) where T : ISemigroup;
    }
}
