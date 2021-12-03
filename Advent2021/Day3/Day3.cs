using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent2021.Days
{
    public class Day3
    {
        public string Part1() 
        {
            var result = string.Empty;
            var data = GetData().ToList(); ;

            var length = data.Count;

            var tracking = new int[data[0].Length];

            foreach(var row in data)
            {
                for(var i = 0; i < row.Length; i++)
                {
                    if (row[i] == '1') tracking[i]++;
                }
            }

            var e = "";
            var g = "";

            for(var i = 0; i < tracking.Length; i++)
            {
                if(tracking[i] < (length / 2))
                {
                    e = e + "1";
                    g = g + "0";
                }
                else
                {
                    e = e + "0";
                    g = g + "1";
                }
            }

            var eNum = Convert.ToInt32(e, 2);
            var gNum = Convert.ToInt32(g, 2);

            return (eNum * gNum).ToString();
        }

        public string Part2() 
        {
            var data = GetData().ToList(); ;

            var tracking = new int[data[0].Length];
            

            //foreach (var row in data)
            //{
            //    for (var i = 0; i < row.Length; i++)
            //    {
            //        if (row[i] == '1') tracking[i]++;
            //    }
            //}

            var o = "";
            var c = "";

            var currentSet = GetData().ToList();

            for(var i = 0; i < tracking.Length; i++)
            {
                tracking = new int[data[0].Length];
                foreach (var row in currentSet)
                {
                    for (var j = 0; j < row.Length; j++)
                    {
                        if (row[j] == '1') tracking[j]++;
                    }
                }

                if (tracking[i]*2 >= (currentSet.Count)) currentSet = currentSet.Where(t => t[i] == '1').ToList();
                else if (tracking[i]*2 < ((decimal)currentSet.Count)) currentSet = currentSet.Where(t => t[i] == '0').ToList();

                if (currentSet.Count() == 1)
                {
                    o = currentSet.First();
                    break;
                }
            }

            currentSet = GetData().ToList();

            for (var i = 0; i < tracking.Length; i++)
            {
                tracking = new int[data[0].Length];
                foreach (var row in currentSet)
                {
                    for (var j = 0; j < row.Length; j++)
                    {
                        if (row[j] == '1') tracking[j]++;
                    }
                }

                if (tracking[i]*2 < (currentSet.Count)) currentSet = currentSet.Where(t => t[i] == '1').ToList();
                else if (tracking[i]*2 >= (currentSet.Count)) currentSet = currentSet.Where(t => t[i] == '0').ToList();

                if (currentSet.Count() == 1)
                {
                    c = currentSet.First();
                    break;
                }
            }

            var oNum = Convert.ToInt32(o, 2);
            var cNum = Convert.ToInt32(c, 2);

            return  (oNum * cNum).ToString();
        }

        private static IEnumerable<string> GetData()
        {
            var lines = File.ReadAllLines("Day3\\day3.txt");

            return lines;
        }
    }
}
