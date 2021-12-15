using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent2021.Days
{
    public class Day12
    {
        public string Part1()
        {
            var paths = new Dictionary<string, List<string>>() { { "end", new List<string>() } };
            var visited = new HashSet<string>() { "start" };

            foreach (var path in GetData())
            {
                if (paths.ContainsKey(path.Start)) 
                { 
                    paths[path.Start].Add(path.End);
                }
                else paths.Add(path.Start, new List<string> { path.End });

                if (paths.ContainsKey(path.End)) paths[path.End].Add(path.Start);
                else paths.Add(path.End, new List<string>() { path.Start });
            }

            var result = Traverse("start", paths, visited);

            return result.ToString();
        }

        public int Traverse(string node, Dictionary<string, List<string>> paths, HashSet<string> visited)
        {
            if (node == "end") return 1;

            if (!node.Any(t => t >= 'A' && t <= 'Z')) visited.Add(node);

            var total = 0;
            foreach(var path in paths[node])
            {
                if(!visited.Contains(path))
                {
                    total += Traverse(path, paths, new HashSet<string>(visited));
                }
            }

            return total;
        }

        public int TraverseV2(string node, Dictionary<string, List<string>> paths, HashSet<string> visited, bool twice)
        {
            if (node == "end") return 1;

            if (visited.Contains(node) && node != "start") twice = true;
            if (!node.Any(t => t >= 'A' && t <= 'Z') && !visited.Contains(node)) visited.Add(node);
            

            var total = 0;
            foreach (var path in paths[node])
            {
                if (!visited.Contains(path) || (visited.Contains(path) && !twice && path != "start"))
                {
                    total += TraverseV2(path, paths, new HashSet<string>(visited), twice);
                }
            }

            return total;
        }

        public string Part2() 
        {
            var paths = new Dictionary<string, List<string>>() { { "end", new List<string>() } };
            var visited = new HashSet<string>() { "start" };

            foreach (var path in GetData())
            {
                if (paths.ContainsKey(path.Start))
                {
                    paths[path.Start].Add(path.End);
                }
                else paths.Add(path.Start, new List<string> { path.End });

                if (paths.ContainsKey(path.End)) paths[path.End].Add(path.Start);
                else paths.Add(path.End, new List<string>() { path.Start });
            }

            var result = TraverseV2("start", paths, visited, false);

            return result.ToString();
        }



        private static IEnumerable<Path> GetData()
        {
            var lines = File.ReadAllLines("Day12\\day12.txt").Select(t => new Path(t));

            return lines;
        }

        public class Path
        {
            public string Start { get; private set; }
            public string End { get; private set; }

            public Path(string path)
            {
                var segments = path.Split('-');
                Start = segments[0];
                End = segments[1];
            }
        }
    }
}
