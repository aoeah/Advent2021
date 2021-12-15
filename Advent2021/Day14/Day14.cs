using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent2021.Days
{
    public class Day14
    {
        public string Part1()
        {
            //var map = new Dictionary<string, string>();
            //var finalValue = GetStarter();


            //foreach (var insertion in GetInsertions())
            //{
            //    map.Add(insertion.Pattern, insertion.Value);
            //}

            //for (var j = 0; j < 10; j++)
            //{
            //    var result = new StringBuilder();

            //    var starter = finalValue;

            //    for (var i = 0; i < starter.Length - 1; i++)
            //    {
            //        result.Append(starter[i]);

            //        if (!map.ContainsKey($"{starter[i]}{starter[i + 1]}")) result.Append(map[$"{starter[i]}{starter[i + 1]}"]);
            //    }

            //    result.Append(starter[starter.Length - 1]);

            //    finalValue = result.ToString();
            //}

            //var x = finalValue.GroupBy(n => n);
            //var largest = x.Max(t => t.Count());
            //var smallest = x.Min(t => t.Count());

            //return (largest - smallest).ToString();
            return string.Empty;
        }


        public string Part2() 
        {
            return string.Empty;
        }

        private static IEnumerable<string> GetData()
        {
            var lines = File.ReadAllLines("Day14\\day14.txt");

            return lines;
        }

        private static string GetStarter()
        {
            return GetData().First();
        }

        private static IEnumerable<Insertion> GetInsertions()
        {
            return GetData().Skip(2).Select(t =>
            {
                var vals = t.Trim().Split(" -> ");

                return new Insertion
                {
                    Pattern = vals[0],
                    Value = vals[1]
                };
            });
        }

        public class Insertion
        {
            public string Pattern { get; set; }
            public string Value { get; set; }
        }
    }
}
