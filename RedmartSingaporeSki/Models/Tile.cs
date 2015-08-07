using System.Collections.Generic;

namespace RedmartSingaporeSki.Models
{
    public class Tile
    {
        public Tile North { get; set; }
        public Tile South { get; set; }
        public Tile East { get; set; }
        public Tile West { get; set; }

        public int Elevation { get; set; }

        public Step Step { get; set; }
        
        public Tile[] GetDownwardTiles()
        {
            var result = new List<Tile>();

            AddLowerValueTile(result, South);
            AddLowerValueTile(result, East);
            AddLowerValueTile(result, West);
            AddLowerValueTile(result, North);

            return result.ToArray();
        }

        private void AddLowerValueTile(IList<Tile> result, Tile otherTile)
        {
            if (otherTile != null && otherTile.Elevation < Elevation)
            {
                result.Add(otherTile);
            }
        }
    }
}
