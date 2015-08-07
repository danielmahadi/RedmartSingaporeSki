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
        private static void Main(string[] args)
        {
            var map = ReadMap("map.txt");

            var pathFinder = new FindPathDownward();
            var flattenPath = pathFinder.Find(map);

            var availableLongestPaths = CompareAndGetLongest(flattenPath);
            var answer = GetSteepest(availableLongestPaths);

            var result = GenerateReport(availableLongestPaths, answer);

            File.WriteAllText("result.txt", result.ToString());
            Console.WriteLine(result.ToString());

            Console.ReadKey();
        }

        private static StringBuilder GenerateReport(IList<Summarize> availableLongestPaths, Summarize answer)
        {
            var result = new StringBuilder();
            result.AppendLine("Available Path:");
            result.AppendLine("===============");
            foreach (var d in availableLongestPaths)
            {
                result.AppendLine(d.ToString());
            }
            result.AppendLine("===============");
            result.AppendLine(string.Format("Answer: {0}", answer));
            return result;
        }

        private static Summarize GetSteepest(IList<Summarize> availableLongestPaths)
        {
            var maxSteep = availableLongestPaths.Max(x => x.Steep);
            var answer = availableLongestPaths.First(x => x.Steep == maxSteep);
            return answer;
        }

        private static IList<Summarize> CompareAndGetLongest(IEnumerable<IList<int>> flattenPath)
        {
            var maxLength = flattenPath.Max(x => x.Count);

            return (from f in flattenPath.Where(x => x.Count == maxLength)
                let steep = (int) (f.First() - f.Last())
                let path = string.Join(" > ", f.ToArray())
                let length = f.Count
                select new Summarize
                {
                    Length = length, Steep = steep, Path = path
                }).ToList();
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
                const string SUM_FORMAT = "Length: {0}, Steep: {1}, Path: {2}";
                return string.Format(SUM_FORMAT, Length, Steep, Path);
            }
        }
    }
}