using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent2021.Days
{
    public class Day6
    {
        public string Part1() 
        {
            var data = GetData().ToList();

            for(var i = 0; i < 80; i++)
            {
                var count = data.Count;
                for(var j = 0; j < count; j++)
                {
                    if (data[j] == 0) 
                    { 
                        data.Add(8);
                        data[j] = 6;
                        continue;
                    }

                    data[j]--;
                }
            }

            return data.Count.ToString();
        }

   
        public string Part2() 
        {
            var data = GetData().ToList();

            var ages = new long[9];

            foreach (var fish in data)
            {
                ages[fish]++;
            }

            for (var i = 0; i < 256; i++)
            {
                var temp = ages[0];

                for (var t = 1; t < 9; t++)
                {
                    ages[t - 1] = ages[t];
                }

                ages[6] += temp;
                ages[8] = temp;
            }

            return ages.Sum().ToString();
        }

        private static IEnumerable<int> GetData()
        {
            var lines = File.ReadAllLines("Day6\\day6.txt").Single().Trim().Split(',');


            return lines.Select(t => Convert.ToInt32(t));
        }
    }
}
