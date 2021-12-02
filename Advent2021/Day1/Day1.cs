using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Advent2021.Days
{
    public class Day1
    {
        public string Part1() 
        {
            bool first = true;
            var lastValue = 0;
            var result = 0;

            foreach (var line in GetData())
            {
                var currentValue = int.Parse(line);

                if (first)
                {
                    first = false;
                    lastValue = currentValue;
                    continue;
                }

                if (currentValue - lastValue > 0) result++;
                lastValue = currentValue;
            }

            return result.ToString();
        }

        public string Part2() 
        {
            var result = 0;

            var counters = new List<int>();
            var runningNumber = 0;

            foreach (var line in GetData())
            {
                var currentValue = int.Parse(line);
                counters.Add(0);

                for (var i = runningNumber; i >= 0 && i > runningNumber - 3; i--)
                {
                    counters[i] += currentValue;
                }

                if (runningNumber >= 3)
                {
                    if (counters[runningNumber - 3] < counters[runningNumber - 2]) result++;
                }

                runningNumber++;
            }

            return result.ToString();
        }

        private static IEnumerable<string> GetData()
        {
            var lines = File.ReadAllLines("Day1\\day1.txt");

            return lines;
        }
    }
}
