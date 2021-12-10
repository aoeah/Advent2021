using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent2021.Days
{
    public class Day10
    {
        public string Part1()
        {
            var lines = GetData();

            var total = 0;

            var scoring = new Dictionary<char, int>()
            {
                { ')', 3 },
                { ']', 57 },
                { '}', 1197 },
                { '>', 25137 }
            };

            var map = new Dictionary<char, char>()
            {
                { '(', ')' },
                { '[', ']' },
                { '{', '}' },
                { '<', '>' }
            };

            foreach(var line in lines)
            {
                var x = new Stack<char>();

                foreach(var c in line)
                {
                    if (map.ContainsKey(c)) x.Push(c);
                    else
                    {
                        if (c != map[x.Pop()]) {
                            total += scoring[c];
                            break;
                        };
                    }
                }
            }

            return total.ToString();
        }

   
        public string Part2() 
        {
            var lines = GetData();

            var scores = new List<long>();

            var scoring = new Dictionary<char, int>()
            {
                { ')', 1 },
                { ']', 2 },
                { '}', 3 },
                { '>', 4 }
            };

            var map = new Dictionary<char, char>()
            {
                { '(', ')' },
                { '[', ']' },
                { '{', '}' },
                { '<', '>' }
            };

            foreach (var line in lines)
            {
                long score = 0;
                var x = new Stack<char>();
                var broken = false;

                foreach (var c in line)
                {
                    if (map.ContainsKey(c)) x.Push(c);
                    else
                    {
                        if (c != map[x.Pop()])
                        {
                            broken = true;
                            break;
                        };
                    }
                }

                if (!broken)
                {
                    while(x.Count > 0)
                    {
                        score *= 5;
                        score += scoring[map[x.Pop()]];
                    }

                    scores.Add(score);
                }
            }

            scores.Sort();
            return scores[scores.Count / 2].ToString();
        }



        private static IEnumerable<string> GetData()
        {
            var lines = File.ReadAllLines("Day10\\day10.txt"); 

            return lines;
        }
    }
}
