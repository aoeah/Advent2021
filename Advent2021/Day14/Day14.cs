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
            var map = new Dictionary<string, string>();
            var finalValue = GetStarter();


            foreach (var insertion in GetInsertions())
            {
                map.Add(insertion.Pattern, insertion.Value);
            }

            for (var j = 0; j < 10; j++)
            {
                var result = new StringBuilder();

                var starter = finalValue;

                for (var i = 0; i < starter.Length - 1; i++)
                {
                    result.Append(starter[i]);

                    if (map.ContainsKey($"{starter[i]}{starter[i + 1]}")) result.Append(map[$"{starter[i]}{starter[i + 1]}"]);
                }

                result.Append(starter[starter.Length - 1]);

                finalValue = result.ToString();
            }

            var x = finalValue.GroupBy(n => n);
            var largest = x.Max(t => t.Count());
            var smallest = x.Min(t => t.Count());

            return (largest - smallest).ToString();
        }


        public string Part2() 
        {
            var map = new Dictionary<string, string>();
            var finalValue = GetStarter();
            var charTracking = new long[26];

            foreach (var insertion in GetInsertions())
            {
                map.Add(insertion.Pattern, insertion.Value);
            }

            var tracking = new Dictionary<string, long>();
            for(var i = 0; i < finalValue.Length - 1; i++)
            {
                charTracking[finalValue[i] - 'A']++;

                if (!tracking.ContainsKey(GetKey(finalValue[i], finalValue[i + 1]))) tracking.Add(GetKey(finalValue[i], finalValue[i + 1]), 1);
                else tracking[GetKey(finalValue[i], finalValue[i + 1])]++;
            }


            charTracking[finalValue.Last() - 'A']++;
            Dictionary<string, long> newTracking = null;

            for (var j = 0; j < 40; j++)
            {
                newTracking = new Dictionary<string, long>();
                foreach(var kv in map)
                {
                    if(tracking.ContainsKey(kv.Key))
                    {
                        var num = tracking[kv.Key];

                        if (!newTracking.ContainsKey(GetKey(kv.Key[0], kv.Value[0]))) newTracking.Add(GetKey(kv.Key[0], kv.Value[0]), num);
                        else newTracking[GetKey(kv.Key[0], kv.Value[0])] += num;

                        if (!newTracking.ContainsKey(GetKey(kv.Value[0], kv.Key[1]))) newTracking.Add(GetKey(kv.Value[0], kv.Key[1]), num);
                        else newTracking[GetKey(kv.Value[0], kv.Key[1])] += num;

                        charTracking[kv.Value[0] - 'A'] += num;
                    }
                }

                tracking = newTracking;
            }

            var largest = charTracking.Max();
            var smallest = charTracking.Where(t => t > 0).Min();


            return (largest - smallest).ToString();
        }

        private string GetKey(char a, char b)
        {
            return $"{a}{b}";
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
