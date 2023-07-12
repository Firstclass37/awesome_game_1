using Game.Server.Logic.Objects._Buidling;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.Gorund.Creation
{
    internal class GroundFactory : IGameObjectFactory
    {
        public GameObjectAggregator CreateNew(Coordiante root, Coordiante[] area)
        {
            var rootCell = root;

            var gameObject = new GameObject(GroundTypes.Ground);
            var positions = area.Select(a => new GameObjectPosition(gameObject.Id, root, a.Equals(rootCell))).ToList();

            return new GameObjectAggregator
            {
                GameObject = gameObject,
                Area = positions
            };
        }
    }
}