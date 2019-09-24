using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms.Graph;
using Algorithms.Utilities;

namespace AlgorithmsTests
{
    class Program
    {
        public struct Point
        {
            public double X { get; set; }
            public double Y { get; set; }

            public Point(double x, double y)
            {
                X = x;
                Y = y;
            }

            public static Func<Point, double> Dist(Point p) => q =>
                 (p.X - q.X) * (p.X - q.X) + (p.Y - q.Y) * (p.Y - q.Y);


        }

        static void Main(string[] args)
        {
            List<Edge<Point>> edges = new List<Edge<Point>>()
            {
                new Edge<Point>(new Point(0, 0), new Point(0, 1), 1),
                new Edge<Point>(new Point(0, 1), new Point(0, 2), 1),
                new Edge<Point>(new Point(0, 2), new Point(0, 3), 10),
                new Edge<Point>(new Point(0, 3), new Point(0, 4), 1),
                new Edge<Point>(new Point(0, 4), new Point(0, 5), 1),
                new Edge<Point>(new Point(0, 5), new Point(0, 6), 1),

                new Edge<Point>(new Point(0, 2), new Point(1, 2), 1),
                new Edge<Point>(new Point(1, 2), new Point(2, 2), 1),
                new Edge<Point>(new Point(2, 2), new Point(3, 2), 1),
                new Edge<Point>(new Point(3, 2), new Point(4, 2), 1),
            };

            Point goal = new Point(0, 6);
            IEnumerable<Edge<Point>> search = Pathfinding<Point>.AStar(
                new Point(0, 0),
                goal,
                Edge<Point>.NeighborsFromAdyascenceMatrix(edges),
                Point.Dist(goal)
            );
            IEnumerable<double> weights = Edge<Point>.AccumulatedWeight(search);

            Console.WriteLine(":v");
        }
    }
}
