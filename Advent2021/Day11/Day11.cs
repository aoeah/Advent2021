using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent2021.Days
{
    public class Day11
    {
        public string Part1()
        {
            var data = GetData().ToList();
            var total = 0;
            var positions = new Dictionary<string, int>();

            //Load into a dicitonary for easier boundary control
            for(var i = 0; i < data.Count; i++)
            {
                for(var j = 0; j < data[i].Count; j++)
                {
                    positions.Add(GetKey(j,i), data[i][j]);
                }
            }

            for(var year = 0; year < 100; year++)
            {
                //Increase everything by 1
                foreach (var key in positions.Keys.ToList()) positions[key]++;

                while(positions.Values.Any(t => t > 9))
                {
                    foreach(var p in positions.Where(t => t.Value > 9).ToList())
                    {
                        total++;

                        var t = GetVals(p.Key);

                       
                        if (ShouldIncrement(positions, t.X - 1, t.Y - 1)) positions[GetKey(t.X - 1, t.Y - 1)]++;
                        if (ShouldIncrement(positions, t.X - 1, t.Y)) positions[GetKey(t.X - 1, t.Y)]++;
                        if (ShouldIncrement(positions, t.X - 1, t.Y + 1)) positions[GetKey(t.X - 1, t.Y + 1)]++;

                        if (ShouldIncrement(positions, t.X, t.Y - 1)) positions[GetKey(t.X, t.Y - 1)]++;
                        if (ShouldIncrement(positions, t.X, t.Y + 1)) positions[GetKey(t.X, t.Y + 1)]++;

                        if (ShouldIncrement(positions, t.X + 1, t.Y - 1)) positions[GetKey(t.X + 1, t.Y - 1)]++;
                        if (ShouldIncrement(positions, t.X + 1, t.Y)) positions[GetKey(t.X + 1, t.Y)]++;
                        if (ShouldIncrement(positions, t.X + 1, t.Y + 1)) positions[GetKey(t.X + 1, t.Y + 1)]++;

                        positions[GetKey(t.X,t.Y)] = -1;
                    }


                }

                foreach (var p in positions.Where(t => t.Value == -1).ToList())
                {
                    positions[p.Key] = 0;
                }
            }

            return total.ToString();
        }

        private bool ShouldIncrement(Dictionary<string,int> positions, int x, int y)
        {
            return positions.ContainsKey(GetKey(x, y)) && positions[GetKey(x,y)] >= 0;
        }


        private string GetKey(int x, int y)
        {
            return $"{x}-{y}";
        }

        private Point GetVals(string position)
        {
            var x = position.Trim().Split('-');
            return new Point(int.Parse(x[0]), int.Parse(x[1]));
        }


        public string Part2() 
        {
            var data = GetData().ToList();
            var total = 0;
            var positions = new Dictionary<string, int>();

            //Load into a dicitonary for easier boundary control
            for (var i = 0; i < data.Count; i++)
            {
                for (var j = 0; j < data[i].Count; j++)
                {
                    positions.Add(GetKey(j, i), data[i][j]);
                }
            }

            for (var year = 0; year < 1000000; year++)
            {
                var yearTotal = 0;
                //Increase everything by 1
                foreach (var key in positions.Keys.ToList()) positions[key]++;

                while (positions.Values.Any(t => t > 9))
                {
                    foreach (var p in positions.Where(t => t.Value > 9).ToList())
                    {
                        total++;
                        yearTotal++;

                        var t = GetVals(p.Key);


                        if (ShouldIncrement(positions, t.X - 1, t.Y - 1)) positions[GetKey(t.X - 1, t.Y - 1)]++;
                        if (ShouldIncrement(positions, t.X - 1, t.Y)) positions[GetKey(t.X - 1, t.Y)]++;
                        if (ShouldIncrement(positions, t.X - 1, t.Y + 1)) positions[GetKey(t.X - 1, t.Y + 1)]++;

                        if (ShouldIncrement(positions, t.X, t.Y - 1)) positions[GetKey(t.X, t.Y - 1)]++;
                        if (ShouldIncrement(positions, t.X, t.Y + 1)) positions[GetKey(t.X, t.Y + 1)]++;

                        if (ShouldIncrement(positions, t.X + 1, t.Y - 1)) positions[GetKey(t.X + 1, t.Y - 1)]++;
                        if (ShouldIncrement(positions, t.X + 1, t.Y)) positions[GetKey(t.X + 1, t.Y)]++;
                        if (ShouldIncrement(positions, t.X + 1, t.Y + 1)) positions[GetKey(t.X + 1, t.Y + 1)]++;

                        positions[GetKey(t.X, t.Y)] = -1;
                    }
                }

                foreach (var p in positions.Where(t => t.Value == -1).ToList())
                {
                    positions[p.Key] = 0;
                }

                if(positions.Values.Count(t => t == 0) == 100) return (year + 1).ToString();
            }

            return 0.ToString();
        }



        private static List<List<int>> GetData()
        {
            var lines = File.ReadAllLines("Day11\\day11.txt").Select(t => t.Select(n => n - '0').ToList()).ToList();

            return lines;
        }
    }
}
