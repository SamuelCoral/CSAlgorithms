using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms.Utilities;

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
        public static LinkedList<KeyValuePair<S, Edge<P>>> AStarState<S>(
            P start,
            P goal,
            S initial,
            Func<P, S, IEnumerable<KeyValuePair<S, Edge<P>>>> neighbors,
            Func<P, double> heuristic
        ) {

            HashSet<P> openSet = new HashSet<P>() { start };
            HashSet<P> closedSet = new HashSet<P>();
            Dictionary<P, Edge<P>> cameFrom = new Dictionary<P, Edge<P>>();
            Dictionary<P, S> states = new Dictionary<P, S>() { { start, initial } };
            Dictionary<P, double> gScore = new Dictionary<P, double>() { { start, 0 } };
            Dictionary<P, double> fScore = new Dictionary<P, double>() { { start, heuristic(start) } };
            LinkedList<KeyValuePair<S, Edge<P>>> totalPath = new LinkedList<KeyValuePair<S, Edge<P>>>();

            while (openSet.Count > 0)
            {
                P current = fScore
                    .Where(p => openSet.Contains(p.Key))
                    .Aggregate((l, r) => l.Value < r.Value ? l : r).Key;
                S currentState = states[current];

                if (current.Equals(goal))
                {
                    while (cameFrom.ContainsKey(current))
                    {
                        totalPath.AddFirst(KeyValuePair.Create(states[current], cameFrom[current]));
                        current = cameFrom[current].From;
                    }

                    return totalPath;
                }

                openSet.Remove(current);
                closedSet.Add(current);

                foreach (KeyValuePair<S, Edge<P>> neighbor in neighbors(current, currentState))
                {
                    P destinationPoint = neighbor.Value.To;
                    if (closedSet.Contains(destinationPoint)) continue;

                    double tentativeGScore = gScore[current] + neighbor.Value.Weight;
                    if (!openSet.Contains(destinationPoint))
                        openSet.Add(destinationPoint);
                    else if (tentativeGScore >= gScore.GetValueOrDefault(destinationPoint, double.MaxValue))
                        continue;

                    states[destinationPoint] = neighbor.Key;
                    cameFrom[destinationPoint] = neighbor.Value;
                    gScore[destinationPoint] = tentativeGScore;
                    fScore[destinationPoint] = tentativeGScore + heuristic(destinationPoint);
                }
            }

            return totalPath;
        }

        public static IEnumerable<Edge<P>> AStar(
            P start,
            P goal,
            Func<P, IEnumerable<Edge<P>>> neighbors,
            Func<P, double> heuristic
        ) => AStarState(
            start,
            goal,
            true,
            (p, _) => neighbors(p).Select(r => KeyValuePair.Create(true, r)),
            heuristic
        ).Select(p => p.Value);

        public static IEnumerable<Edge<P>> Dijkstra(P start, P goal, Func<P, IEnumerable<Edge<P>>> neighbors) =>
            AStar(start, goal, neighbors, Function.Const<P, double>(0));
    }
}
