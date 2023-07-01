using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects.PowerStations.Interaction;
using Game.Server.Models;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.PowerStations.Creation
{
    internal class PowerStationFactory : IGameObjectFactory
    {
        public GameObjectAggregator CreateNew(Coordiante root, Coordiante[] area)
        {
            var gameObject = new GameObject(BuildingTypes.PowerStation);
            var positions = area.Select(a => new GameObjectPosition(gameObject.Id, root, a.Equals(root))).ToArray();
            var interactions = new List<GameObjectInteraction>()
            {
                new GameObjectInteraction(gameObject.Id, typeof(PowerStatitionInteraction).FullName)
            };

            return new GameObjectAggregator
            {
                GameObject = gameObject,
                Area = positions,
                Interactions = interactions,
            };
        }
    }
}