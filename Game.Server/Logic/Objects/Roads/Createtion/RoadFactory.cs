using Game.Server.Logic.Objects._Buidling;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.Roads.Createtion
{
    internal class RoadFactory : IGameObjectFactory
    {
        public GameObjectAggregator CreateNew(Coordiante root, Coordiante[] area, int player)
        {
            var gameObject = new GameObject(BuildingTypes.Road);
            var positions = area.Select(a => new GameObjectPosition(gameObject.Id, root, a.Equals(root))).ToList();

            return new GameObjectAggregator
            {
                GameObject = gameObject,
                Area = positions
            };
        }
    }
}