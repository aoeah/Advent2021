using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent2021.Days
{
    public class Day7
    {
        public string Part1() 
        {
            var data = GetData().ToList();
            var minTotal = int.MaxValue;

            for(var i = 0; i < data.Count; i++)
            {
                var total = 0;
                for(var j = 0; j < data.Count; j++)
                {
                    total += Math.Abs(data[j] - data[i]);
                }

                minTotal = Math.Min(minTotal, total);
            }

            return minTotal.ToString();
        }

   
        public string Part2() 
        {
            var data = GetData().ToList();
            var minTotal = int.MaxValue;

            for (var i = 0; i < data.Max(); i++)
            {
                var total = 0;
                for (var j = 0; j < data.Count; j++)
                {
                    var diff = Math.Abs(data[j] - i);

                    total += (diff * (diff + 1)) / 2;
                }

                minTotal = Math.Min(minTotal, total);
            }

            return minTotal.ToString();
        }

        private static IEnumerable<int> GetData()
        {
            var lines = File.ReadAllLines("Day7\\day7.txt").Single().Trim().Split(',');

            return lines.Select(t => Convert.ToInt32(t));
        }
    }
}
