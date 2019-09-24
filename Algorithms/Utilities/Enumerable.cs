using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Utilities
{
    public static class EnumerableUtils
    {
        public static IEnumerable<int> InfiniteList(int start, int step = 0)
        {
            yield return start;
            while(true) yield return start += step;
        }

        public static IEnumerable<double> InfiniteList(double start, double step = 0)
        {
            yield return start;
            while (true) yield return start += step;
        }
    }
}
