using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Advent2021.Days
{
    public class Day2
    {
        public string Part1() 
        {
            var depth = 0;
            var x = 0;

            foreach(var data in GetData())
            {
                switch(data.direction)
                {
                    case "forward":
                        x += data.Distance;
                        break;
                    case "down":
                        depth += data.Distance;
                        break;
                    case "up":
                        depth -= data.Distance;
                        break;
                }
            }

            return (depth * x).ToString();
        }

        public string Part2() 
        {
            var depth = 0;
            var x = 0;
            var aim = 0;

            foreach (var data in GetData())
            {
                switch (data.direction)
                {
                    case "forward":
                        x += data.Distance;
                        depth = depth + (aim * data.Distance);
                        break;
                    case "down":
                        aim += data.Distance;
                        break;
                    case "up":
                        aim -= data.Distance;
                        break;
                }
            }

            return (depth * x).ToString();
        }

        private static IEnumerable<Command> GetData()
        {
            var lines = File.ReadAllLines("Day2\\day2.txt");

            foreach(var line in lines)
            {
                var x = line.Trim().Split(' ');

                yield return new Command
                {
                    direction = x[0],
                    Distance = int.Parse(x[1])
                };
            }
        }

        public class Command
        {
            public string direction;
            public int Distance;
        }
    }
}
