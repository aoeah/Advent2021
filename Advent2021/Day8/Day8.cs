using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent2021.Days
{
    public class Day8
    {
        public string Part1()
        {
            var data = GetData();

            var total = 0;

            foreach (var line in data)
            {
                var segments = line.Trim().Split('|')[1].Trim().Split(' ');
                total += segments.Count(t => t.Length == 2 || t.Length == 4 || t.Length == 3 || t.Length == 7);
            }

            return total.ToString();
        }

   
        public string Part2() 
        {
            var data = GetData();

            var total = 0;

            var result = 0;

            foreach (var line in data)
            {
                var segments = line.Trim().Split('|')[0].Trim().Split(' ');

                var mapping = new Dictionary<string, int>();

                var one = string.Concat(segments.FirstOrDefault(t => t.Length == 2).OrderBy(t => t));
                var three = string.Concat(segments.Where(t => t.Length == 5 && FindCommon(t, one).Count() == 2).FirstOrDefault().OrderBy(t => t));
                var six = string.Concat(segments.Where(t => t.Length == 6 && FindCommon(t, one).Count() == 1).FirstOrDefault().OrderBy(t => t));
                var two = string.Concat(segments.Where(t => t.Length == 5 && FindCommon(t, one).Count() == 1 && FindCommon(one, six).First() != FindCommon(t, one).First()).FirstOrDefault().OrderBy(t => t));
                var five = string.Concat(segments.Where(t => t.Length == 5 && FindCommon(t, one).Count() == 1 && FindCommon(one, six).First() == FindCommon(t, one).First()).FirstOrDefault().OrderBy(t => t));
                var nine = string.Concat(segments.Where(t => t.Length == 6 && FindCommon(t, one).Count() == 2 && FindCommon(t, five).Count() == 5).FirstOrDefault().OrderBy(t => t));
                var zero = string.Concat(segments.Where(t => t.Length == 6 && FindCommon(t, one).Count() == 2 && FindCommon(t, five).Count() == 4).FirstOrDefault().OrderBy(t => t));


                mapping.Add(three ?? "3", 3);
                mapping.Add(six ?? "6", 6);
                mapping.Add(two ?? "5", 2);
                mapping.Add(one ?? "1", 1);
                mapping.Add(string.Concat(segments.FirstOrDefault(t => t.Length == 4).OrderBy(t => t)) ?? "4", 4);
                mapping.Add(string.Concat(segments.FirstOrDefault(t => t.Length == 3).OrderBy(t => t)) ?? "7", 7);
                mapping.Add(string.Concat(segments.FirstOrDefault(t => t.Length == 7).OrderBy(t => t)) ?? "8", 8);
                mapping.Add(five ?? "5", 5);
                mapping.Add(nine ?? "9", 9);
                mapping.Add(zero ?? "0", 0);

                var right = line.Trim().Split('|')[1].Trim().Split(' ');
                var tempVal = string.Concat(right.Select(t => mapping[string.Concat(t.OrderBy(n => n))]));

                result += Convert.ToInt32(tempVal);
            }

            return result.ToString();
        }

        public IEnumerable<char> FindCommon(string a, string b)
        {
            return a.Where(t => b.Contains(t));
        }

        private static IEnumerable<string> GetData()
        {
            var lines = File.ReadAllLines("Day8\\day8.txt");

            

            return lines;
        }
    }
}
