using Game.Server.Logic.Objects._Requirements;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.Gorund.Requirements
{
    internal class GroundRequirement : ICreationRequirement
    {
        public bool Satisfy(Dictionary<Coordiante, GameObjectAggregator> area)
        {
            return area.Values.All(a => a.GameObject.ObjectType == BuildingTypes.Ground);
        }
    }
}