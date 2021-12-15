using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent2021.Days
{
    public class Day13
    {
        public string Part1()
        {
            var points = new HashSet<Point>();
            
            foreach(var point in GetPoints())
            {
                points.Add(point);
            }

            var firstFold = GetFolds().First();

            DoFold(points, firstFold);

            return points.Count.ToString();
        }

        public void DoFold(HashSet<Point> points, Fold fold)
        {
            if (fold.X != null)
            {

                foreach (var point in points.ToList())
                {
                    if (point.X < fold.X) continue;

                    var diff = point.X - fold.X.Value;
                    var newX = fold.X.Value - diff;

                    if(newX >= 0)
                    {
                        points.Remove(point);
                        var newPoint = new Point(newX, point.Y);

                        if (!points.Contains(newPoint)) points.Add(newPoint);
                    }
                }
            }
            else
            {
                foreach (var point in points.ToList())
                {
                    if (point.Y < fold.Y) continue;

                    var diff = point.Y - fold.Y.Value;
                    var newY = fold.Y.Value - diff;

                    if (newY >= 0)
                    {
                        points.Remove(point);
                        var newPoint = new Point(point.X, newY);

                        if (!points.Contains(newPoint)) points.Add(newPoint);
                    }
                }
            }
        }


        public string Part2() 
        {
            var points = new HashSet<Point>();

            foreach (var point in GetPoints())
            {
                points.Add(point);
            }

            foreach (var fold in GetFolds())
            {
                DoFold(points, fold);
            }

            return PrintPoints(points);
        }

        string PrintPoints(HashSet<Point> points)
        {
            var maxX = points.Max(t => t.X) + 1;
            var maxY = points.Max(t => t.Y) + 1;

            var pointArray = new int[maxX, maxY];
            foreach(var point in points)
            {
                pointArray[point.X, point.Y] = 1;
            }

            var output = new StringBuilder();
            output.AppendLine();

            for(var i = 0; i < maxY; i++)
            {
                for(var j = 0; j < maxX; j++)
                {
                    output.Append(pointArray[j, i] == 0 ? "." : "#");
                }

                output.AppendLine();
            }

            return output.ToString();
        }    

        private static IEnumerable<Point> GetPoints()
        {
            var lines = File.ReadAllLines("Day13\\day13.txt");

            foreach(var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) break;

                var parts = line.Split(',');
                yield return new Point(int.Parse(parts[0]), int.Parse(parts[1]));
            }
        }

        private static IEnumerable<Fold> GetFolds()
        {
            var lines = File.ReadAllLines("Day13\\day13.txt");
            var folds = false;
            foreach(var line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line) && !folds) continue;
                if(string.IsNullOrWhiteSpace(line)) { folds = true; continue; }


                var fold = new Fold();

                var parts = line.Split(' ')[2].Split('=');
                if (parts[0] == "x") fold.X = int.Parse(parts[1]);
                else fold.Y = int.Parse(parts[1]);

                yield return fold;
            }
        }

        public class Fold
        {
            public int? X { get; set; }
            public int? Y { get; set; }
        }
    }
}
