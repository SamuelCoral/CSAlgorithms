using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Graph
{
    /// <summary>
    /// Edge between 2 vertices with weight.
    /// </summary>
    /// <typeparam name="P">Vertex type</typeparam>
    public struct Edge<P>
    {
        /// <summary>
        /// Starting vertex.
        /// </summary>
        public P From { get; set; }

        /// <summary>
        /// Ending vertex.
        /// </summary>
        public P To { get; set; }

        /// <summary>
        /// Edge weight.
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Creates a new instance of Edge.
        /// </summary>
        /// <param name="from">Starting vertex</param>
        /// <param name="to">Ending vertex.</param>
        /// <param name="weight">Edge weight</param>
        public Edge(P from, P to, double weight)
        {
            From = from;
            To = to;
            Weight = weight;
        }


        /// <summary>
        /// Provides a function that returns the neighbor vertices of a givenvertex given
        /// a list of edges.
        /// </summary>
        /// <param name="edges">Edges list</param>
        /// <returns>A function for the neighbors of a vertex</returns>
        public static Func<P, IEnumerable<Edge<P>>> NeighborsFromAdyascenceMatrix(IEnumerable<Edge<P>> edges) =>
            p => edges.Where(e => e.From.Equals(p));

        /// <summary>
        /// Provides a function that returns the neighbor vertices of a given vertex given
        /// an adyascence dictionary.
        /// </summary>
        /// <param name="matrix">Adyascence matrix</param>
        /// <returns>A function for the neighbors of a vertex</returns>
        public static Func<P, IEnumerable<Edge<P>>> NeighborsFromAdyascenceMatrix(IReadOnlyDictionary<P, IDictionary<P, double>> matrix) =>
            p => from e in matrix.GetValueOrDefault(p, new Dictionary<P, double>())
                 select new Edge<P>(p, e.Key, e.Value);

        /// <summary>
        /// Provides a function that returns the neighbor vertices of a given vertex given
        /// a list of edges in dictionary form.
        /// </summary>
        /// <param name="matrix">Edges dictionary</param>
        /// <returns>A function for the neighbors of a vertex</returns>
        public static Func<P, IEnumerable<Edge<P>>> NeighborsFromAdyascenceMatrix(IDictionary<KeyValuePair<P, P>, double> matrix) =>
            p => from e in matrix
                 where e.Key.Key.Equals(p)
                 select new Edge<P>(e.Key.Key, e.Key.Value, e.Value);

        /// <summary>
        /// Calculates the partial sums of weights up to every vertex in the path
        /// for the given list of edges.
        /// </summary>
        /// <param name="path">Edges list</param>
        /// <returns>List of partial sums of weights</returns>
        public static IEnumerable<double> AccumulatedWeight(IEnumerable<Edge<P>> path)
        {
            double totalWeight = 0;
            foreach (Edge<P> edge in path)
            {
                totalWeight += edge.Weight;
                yield return totalWeight;
            }
        }
    }
}
