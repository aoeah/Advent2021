using Advent2021.Days;
using System;
using System.Collections.Generic;
using System.IO;

namespace Advent2021
{
    class Program
    {
        static void Main(string[] args)
        {
            var day = new Day5();

            Console.WriteLine($"Part1: {day.Part1()}");
            Console.WriteLine($"Part2: {day.Part2()}");
        }
    }
}

