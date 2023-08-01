using Game.Server.Logic.Objects._Requirements;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.Gorund.Requirements
{
    internal class GroundRequirement : ICreationRequirement
    {
        public bool Satisfy(Coordiante coordiante, Dictionary<Coordiante, GameObjectAggregator> area)
        {
            return area.Values.All(a => a == null);
        }
    }
}