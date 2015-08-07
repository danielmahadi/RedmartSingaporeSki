using System.Collections.Generic;
using System.Linq;

namespace RedmartSingaporeSki.Models
{
    public class Step
    {
        public IList<Tile> Tiles { get; set; }
        public Map Map { get; set; }

        public Step()
        {
            Tiles = new List<Tile>();
        }

        public void AddTile(Tile tile)
        {
            var lastTile = Tiles.LastOrDefault();

            if (lastTile != null)
            {
                lastTile.East = tile;
                tile.West = lastTile;
            }

            tile.Step = this;
            Tiles.Add(tile);
        }
    }
}