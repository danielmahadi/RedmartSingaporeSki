using System.Collections.Generic;
using System.Linq;

namespace RedmartSingaporeSki.Models
{
    public class Map
    {
        public IList<Step> Steps { get; set; }

        public Map()
        {
            Steps = new List<Step>();
        }

        public void AddStep(Step step)
        {
            var lastStep = Steps.LastOrDefault();

            if (lastStep != null)
            {
                for (var idx = 0; idx < lastStep.Tiles.Count; idx++)
                {
                    lastStep.Tiles[idx].South = step.Tiles[idx];
                    step.Tiles[idx].North = lastStep.Tiles[idx];
                }
            }

            step.Map = this;
            Steps.Add(step);
        }
    }
}