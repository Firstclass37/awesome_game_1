using Game.Server.DataBuilding;
using Game.Server.Logic._Extentions;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects.Home.Action;
using Game.Server.Models.Constants;
using Game.Server.Models.Constants.Attributes;
using Game.Server.Models.GameObjects;
using Game.Server.Models.GamesObjectList;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.Home.Creation
{
    internal class HomeFactory : IGameObjectFactory
    {
        public GameObjectAggregator CreateNew(Coordiante root, Coordiante[] area, int player)
        {
           var spawnCell = new Coordiante(root.X, root.Y + 3);
           return new GameObjectAggregatorBuilder(BuildingTypes.Home, player)
                .AddArea(root, area)
                .AddAttribute(HomeAttributes.SpawnCell, spawnCell)
                .AsManufactoring(new ManufactoringArgs
                {
                    PrduceSpeedSeconds = 5,
                    BaseOnQueue = false,
                    ProduceAction = TypeInfoFactory.Create<IProduceAction, CreateCharacterPeriodicAction>(),
                })
                .Build();
        }
    }
}