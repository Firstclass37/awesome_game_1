using Game.Server.Logic.Objects._Buidling;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;
using Game.Server.Logic.Objects.AluminumMine.Interaction;
using Game.Server.DataBuilding;

namespace Game.Server.Logic.Objects.AluminumMine.Creation
{
    internal class AluminumMineFactory : IGameObjectFactory
    {
        public GameObjectAggregator CreateNew(Coordiante root, Coordiante[] area)
        {
            var gameObject = new GameObjectAggregatorBuilder(BuildingTypes.AluminumMine)
                .AddArea(root, area)
                .AddInteraction<AluminumMineInteraction>()
                .Build();
            return gameObject;
        }
    }
}