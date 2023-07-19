using Game.Server.DataBuilding;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects.Home.Action;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.Home.Creation
{
    internal class HomeFactory : IGameObjectFactory
    {
        public GameObjectAggregator CreateNew(Coordiante root, Coordiante[] area)
        {
           var spawnCell = new Coordiante(root.X, root.Y + 3);
           return new GameObjectAggregatorBuilder(BuildingTypes.Home)
                .AddArea(root, area)
                .AddAttribute(AttributeType.SpawnCell, spawnCell)
                .AddPeriodicAction<CreateCharacterPeriodicAction>(5)
                .Build();
        }
    }
}