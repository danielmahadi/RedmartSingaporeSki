using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using RedmartSingaporeSki.Models;
using RedmartSingaporeSki.PathFinders;

namespace RedmartSingaporeSki
{
    internal class Program
    {
        private static string SUM_FORMAT = "Length: {0}, Steep: {1}, Path: {2}";

        private static void Main(string[] args)
        {
            var map = ReadMap("map.txt");

            var pathFinder = new FindPathDownward();
            var flattenPath = pathFinder.Find(map);

            Print(flattenPath);

            Console.ReadKey();
        }

        private static void Print(IEnumerable<IList<int>> flattenPath)
        {
            var result = new StringBuilder();

            var maxLength = flattenPath.Max(x => x.Count);

            var sum = new List<Summarize>();

            foreach (var f in flattenPath.Where(x => x.Count == maxLength))
            {
                var steep = f.First() - f.Last();
                var path = string.Join(" > ", f.ToArray());
                var length = f.Count;

                var message = string.Format(SUM_FORMAT,
                    length, steep, path);

                result.AppendLine(message);

                sum.Add(new Summarize
                {
                    Length = length,
                    Steep = steep,
                    Path = path
                });
            }

            var maxSteep = sum.Max(x => x.Steep);
            var answer = sum.First(x => x.Steep == maxSteep);
            result.AppendLine(string.Format("Answer: {0}", answer));

            File.WriteAllText("result.txt", result.ToString());

            Console.WriteLine(result.ToString());
        }

        private static Map ReadMap(string fileName)
        {
            var mapPath = Path.Combine(Environment.CurrentDirectory, fileName);

            var map = new Map();

            var content = File.ReadLines(mapPath);
            var contentInfo = content
                .Skip(1)
                .ToArray();

            for (var idx = 0; idx < contentInfo.Length; idx++)
            {
                var arr = contentInfo[idx]
                    .Split(' ')
                    .Select(int.Parse)
                    .ToArray();

                var step = new Step();

                foreach (var val in arr)
                {
                    step.AddTile(new Tile {Elevation = val});
                }

                map.AddStep(step);
            }

            return map;
        }

        private class Summarize
        {
            public int Length { get; set; }
            public int Steep { get; set; }
            public string Path { get; set; }

            public override string ToString()
            {
                return string.Format(SUM_FORMAT, Length, Steep, Path);
            }
        }
    }
}