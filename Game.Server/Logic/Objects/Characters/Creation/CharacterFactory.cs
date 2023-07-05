using Game.Server.Logic.Objects._Buidling;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.Characters.Creation
{
    internal class CharacterFactory : IGameObjectFactory
    {
        public GameObjectAggregator CreateNew(Coordiante root, Coordiante[] area)
        {
            var rootCell = root;
            var spawnCell = new Coordiante(rootCell.X, rootCell.Y + 3);

            var gameObject = new GameObject(BuildingTypes.Character);
            var positions = area.Select(a => new GameObjectPosition(gameObject.Id, root, a.Equals(rootCell))).ToList();

            return new GameObjectAggregator
            {
                GameObject = gameObject,
                Area = positions
            };
        }
    }
}