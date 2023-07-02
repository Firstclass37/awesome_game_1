using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects.Home.Action;
using Game.Server.Models;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.Home.Creation
{
    internal class HomeFactory : IGameObjectFactory
    {
        public GameObjectAggregator CreateNew(Coordiante root, Coordiante[] area)
        {
            var rootCell = root;
            var spawnCell = new Coordiante(rootCell.X, rootCell.Y + 3);

            var gameObject = new GameObject(BuildingTypes.HomeType1);
            var positions = area.Select(a => new GameObjectPosition(gameObject.Id, root, a.Equals(rootCell))).ToArray();
            var attributes = new List<GameObjectToAttribute>
            {
                new GameObjectToAttribute(gameObject.Id, AttributeType.SpawnCell, spawnCell)
            };
            var periodicActions = new List<PeriodicAction>() 
            {
                new PeriodicAction(gameObject.Id, typeof(CreateCharacterPeriodicAction).FullName, 5, -1)
            };

            return new GameObjectAggregator
            {
                GameObject = gameObject,
                Area = positions,
                PeriodicActions = periodicActions,
            };
        }
    }
}