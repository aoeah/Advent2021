using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent2021.Days
{
    public class Day5
    {
        public string Part1() 
        {
            var data = GetData().Where(t => t.IsVerticalOrHorizontal);

            var maxX = Math.Max(data.Max(t => t.Start.X), data.Max(t => t.End.X));
            var maxY = Math.Max(data.Max(t => t.Start.Y), data.Max(t => t.End.Y));

            var spaces = new Dictionary<string, int>();

            foreach(var line in data)
            {
                var done = false;
                for (var i = Math.Min(line.Start.X, line.End.X); line.Start.X != line.End.X && i <= Math.Max(line.Start.X, line.End.X); i++)
                {
                    var key = $"X{i}Y{line.End.Y}";
                    if (spaces.ContainsKey(key)) spaces[key]++;
                    else spaces.Add(key, 1);
                    done = true;
                }

                if (done) continue;

                for (var i = Math.Min(line.Start.Y, line.End.Y); line.Start.Y != line.End.Y && i <= Math.Max(line.Start.Y, line.End.Y); i++)
                {
                    var key = $"X{line.End.X}Y{i}";
                    if (spaces.ContainsKey(key)) spaces[key]++;
                    else spaces.Add(key, 1);
                }
            }

            return spaces.Count(t => t.Value > 1).ToString();
        }

   
        public string Part2() 
        {
            var data = GetData();
            var spaces = new Dictionary<string, int>();

            foreach (var line in data)
            {
                var done = false;

                if (line.Start.X != line.End.X && line.Start.Y != line.End.Y)
                {
                    var diffX = line.Start.X - line.End.X;
                    var diffY = line.Start.Y - line.End.Y;

                    var adderX = diffX < 0 ? 1 : -1;
                    var adderY = diffY < 0 ? 1 : -1;

                    var x = line.Start.X;
                    var y = line.Start.Y;

                    while(x != line.End.X + adderX) { 
                        var key = $"X{x}Y{y}";
                        if (spaces.ContainsKey(key)) spaces[key]++;
                        else spaces.Add(key, 1);

                        x += adderX;
                        y += adderY;
                    }

                    continue;
                }

                for (var i = Math.Min(line.Start.X, line.End.X); line.Start.X != line.End.X && i <= Math.Max(line.Start.X, line.End.X); i++)
                {
                    var key = $"X{i}Y{line.End.Y}";
                    if (spaces.ContainsKey(key)) spaces[key]++;
                    else spaces.Add(key, 1);
                    done = true;
                }

                if (done) continue;

                for (var i = Math.Min(line.Start.Y, line.End.Y); line.Start.Y != line.End.Y && i <= Math.Max(line.Start.Y, line.End.Y); i++)
                {
                    var key = $"X{line.End.X}Y{i}";
                    if (spaces.ContainsKey(key)) spaces[key]++;
                    else spaces.Add(key, 1);
                }
            }

            return spaces.Count(t => t.Value > 1).ToString();
        }

        private static IEnumerable<Line> GetData()
        {
            var lines = File.ReadAllLines("Day5\\day5.txt");

            return lines.Select(line =>
            {
                var sides = line.Trim().Split(" -> ");
                var left = sides[0].Split(',');
                var right = sides[1].Split(',');

                var leftPoint = new Point(Convert.ToInt32(left[0]), Convert.ToInt32(left[1]));
                var rightPoint = new Point(Convert.ToInt32(right[0]), Convert.ToInt32(right[1]));

                return new Line
                {
                    Start = leftPoint,
                    End = rightPoint
                };
            });
        }

        private class Line
        {
            public Point Start;
            public Point End;

            public bool IsVerticalOrHorizontal
            {
                get
                {
                    {
                        return Start.X == End.X || Start.Y == End.Y;
                    }
                }
            }
        }
    }
}
