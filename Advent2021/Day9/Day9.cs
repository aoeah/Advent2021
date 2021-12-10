using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent2021.Days
{
    public class Day9
    {
        public string Part1()
        {
            var data = GetData().ToList();

            var width = data.First().Count;
            var height = data.Count();

            var result = new List<int>();

            for(var i = 0; i < height; i++)
            {
                for(var j = 0; j < width; j++)
                {
                    var t = data[i][j];

                    var min = t;

                    if (j != 0 && data[i][j - 1] == t) continue;
                    if (i != 0 && data[i - 1][j] == t) continue;
                    if (j != width - 1 && data[i][j + 1] == t) continue;
                    if (i != height - 1 && data[i + 1][j] == t) continue;

                    if (j != 0) min = Math.Min(min, data[i][j - 1]);
                    if (i != 0) min = Math.Min(min, data[i - 1][j]);
                    if (j != width - 1) min = Math.Min(min, data[i][j + 1]);
                    if (i != height - 1) min = Math.Min(min, data[i + 1][j]);

                    if (min == t) result.Add(t);
                }
            }

            return result.Sum(t => t + 1).ToString();
        }

   
        public string Part2() 
        {
            return string.Empty;
        }



        private static IEnumerable<IList<int>> GetData()
        {
            var lines = File.ReadAllLines("Day9\\day9.txt").Select(t => t.Select(n => n - '0').ToList()).ToList();

            

            return lines;
        }
    }
}
