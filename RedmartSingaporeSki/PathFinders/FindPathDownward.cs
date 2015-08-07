using System.Collections.Generic;
using RedmartSingaporeSki.Models;

namespace RedmartSingaporeSki.PathFinders
{
    public class FindPathDownward
    {
        public IEnumerable<IList<int>> Find(Map map)
        {
            var flattenPath = new List<IList<int>>();

            var allTiles = new List<Tile>();

            foreach (var step in map.Steps)
            {
                allTiles.AddRange(step.Tiles);
            }

            foreach (var tile in allTiles)
            {
                TraverseDownward(flattenPath, tile, new List<int>());
            }

            return flattenPath;
        }

        private static void TraverseDownward(
            ICollection<IList<int>> flattenPath,
            Tile currentTile,
            IEnumerable<int> previousPath)
        {
            var newPath = new List<int>(previousPath) {currentTile.Elevation};
            flattenPath.Add(newPath);

            var otherTiles = currentTile.GetDownwardTiles();

            foreach (var otherTile in otherTiles)
            {
                TraverseDownward(flattenPath, otherTile, newPath);
            }
        }
    }
}