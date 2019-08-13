using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Graph
{
    /// <summary>
    /// Pathfinding algorithmsand utilities for graphs.
    /// </summary>
    /// <typeparam name="P">Vertex type of the graph</typeparam>
    public static class Pathfinding<P>
    {
        /// <summary>
        /// Performs the A* search algorithm over a given graph, an heuristic function and
        /// a function to obtain the neighbors of a vertex.
        /// To use an adyascence matrix or an edge list, use
        /// <seealso cref="Edge{P}.NeighborsFromAdyascenceMatrix(IEnumerable{Edge{P}})"/>
        /// </summary>
        /// <param name="start">Starting vertex</param>
        /// <param name="goal">Ending vertex</param>
        /// <param name="neighbors">A function to obtain the neighbors of a vertex</param>
        /// <param name="heuristic">A function for an heuristic of a vertex</param>
        /// <returns>A list of edges representing the best route.</returns>
        public static LinkedList<Edge<P>> AStar(P start, P goal, Func<P, IEnumerable<Edge<P>>> neighbors, Func<P, double> heuristic)
        {
            HashSet<P> openSet = new HashSet<P>() { start };
            HashSet<P> closedSet = new HashSet<P>();
            Dictionary<P, Edge<P>> cameFrom = new Dictionary<P, Edge<P>>();
            Dictionary<P, double> gScore = new Dictionary<P, double>() { { start, 0 } };
            Dictionary<P, double> fScore = new Dictionary<P, double>() { { start, heuristic(start) } };
            LinkedList<Edge<P>> totalPath = new LinkedList<Edge<P>>();

            while (openSet.Count > 0)
            {
                P current = fScore
                    .Where(p => openSet.Contains(p.Key))
                    .Aggregate((l, r) => l.Value < r.Value ? l : r).Key;

                if (current.Equals(goal))
                {
                    while (cameFrom.ContainsKey(current))
                    {
                        totalPath.AddFirst(cameFrom[current]);
                        current = cameFrom[current].From;
                    }

                    return totalPath;
                }

                openSet.Remove(current);
                closedSet.Add(current);

                foreach (Edge<P> neighbor in neighbors(current))
                {
                    if (closedSet.Contains(neighbor.To)) continue;

                    double tentativeGScore = gScore[current] + neighbor.Weight;
                    if (!openSet.Contains(neighbor.To))
                        openSet.Add(neighbor.To);
                    else if (tentativeGScore >= gScore.GetValueOrDefault(neighbor.To, double.MaxValue))
                        continue;

                    cameFrom[neighbor.To] = neighbor;
                    gScore[neighbor.To] = tentativeGScore;
                    fScore[neighbor.To] = tentativeGScore + heuristic(neighbor.To);
                }
            }

            return totalPath;
        }
    }
}
