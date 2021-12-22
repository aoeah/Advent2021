using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent2021.Days
{
    public class Day15
    {
        public string Part1()
        {
            return ShortestPath(GetData().ToList()).ToString();
        }

        public string Part2()
        {
            return ShortestPath(GetData2().ToList()).ToString();
        }

        public int ShortestPath(IList<IList<int>> data)
        {
            //item1 = from, item2 = to
            var queue = new Queue<Tuple<Point, Point>>();

            var n = data.Count;

            var board = new int[n, n];
            var paths = new int[n, n];

            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    board[j, i] = data[i][j];
                    paths[j, i] = int.MaxValue;
                }
            }

            paths[0, 0] = 0;
            EnqueueNeighbors(queue, new Point(0, 0));

            while (queue.TryDequeue(out var point))
            {
                //Allows us to just add all possible options without much logic
                if (point.Item2.X < 0 || point.Item2.Y < 0 || point.Item2.X >= n || point.Item2.Y >= n) continue;

                if (paths[point.Item2.X, point.Item2.Y] <= paths[point.Item1.X, point.Item1.Y] + board[point.Item2.X, point.Item2.Y]) continue;

                paths[point.Item2.X, point.Item2.Y] = paths[point.Item1.X, point.Item1.Y] + board[point.Item2.X, point.Item2.Y];

                EnqueueNeighbors(queue, point.Item2);
            }

            return paths[n - 1, n - 1];
        }

        public void EnqueueNeighbors(Queue<Tuple<Point, Point>> queue, Point from)
        {
            queue.Enqueue(Tuple.Create(from, new Point(from.X, from.Y + 1)));
            queue.Enqueue(Tuple.Create(from, new Point(from.X + 1, from.Y)));
            queue.Enqueue(Tuple.Create(from, new Point(from.X - 1, from.Y)));
            queue.Enqueue(Tuple.Create(from, new Point(from.X, from.Y - 1)));
        }

        private static IEnumerable<IList<int>> GetData()
        {
            var lines = File.ReadAllLines("Day15\\day15.txt").Select(t => t.Select(n => n - '0').ToList());

            return lines;
        }

        private static IEnumerable<IList<int>> GetData2()
        {
            var lines = File.ReadAllLines("Day15\\day15.txt").Select(t => t.Select(n => n - '0').ToList());

            for (var j = 0; j < 5; j++)
            {
                foreach (var line in lines)
                {
                    var newLine = new List<int>();

                    for (var i = 0; i < 5; i++)
                    {
                        newLine.AddRange(line.Select(t =>
                        {
                            t = t + i + j;
                            t = t % 10;

                            if (t == 0) t = 1;

                            return t;
                        }));
                    }

                    yield return newLine;
                }
            }
        }
    }
}
