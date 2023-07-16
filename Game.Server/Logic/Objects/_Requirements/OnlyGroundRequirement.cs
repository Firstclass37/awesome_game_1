using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects._Requirements
{
    internal class OnlyGroundRequirement : ICreationRequirement
    {
        public bool Satisfy(Dictionary<Coordiante, GameObjectAggregator> area)
        {
            return area.Values.All(v => v.GameObject.ObjectType == BuildingTypes.Ground);
        }
    }
}