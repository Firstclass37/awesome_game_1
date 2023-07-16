using Game.Server.Logic.Objects._Requirements;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.UranusMine.Requirement
{
    internal class UranusMineRequirement : ICreationRequirement
    {
        public bool Satisfy(Dictionary<Coordiante, GameObjectAggregator> area)
        {
            var gameObjectsHere = area.Values.ToArray();

            var resources = gameObjectsHere
                .Where(o => o.GameObject.ObjectType == ResourceResourceTypes.Uranium)
                .ToArray();

            if (!resources.Any())
                return false;

            var grounds = gameObjectsHere
                .Except(resources)
                .Where(o => o.GameObject.ObjectType == BuildingTypes.Ground)
                .Count();

            return (resources.Count() + grounds) == gameObjectsHere.Count();
        }
    }
}