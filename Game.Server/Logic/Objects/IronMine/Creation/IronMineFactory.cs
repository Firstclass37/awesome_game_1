using Game.Server.DataBuilding;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects.IronMine.Interaction;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.IronMine.Creation
{
    internal class IronMineFactory : IGameObjectFactory
    {
        public GameObjectAggregator CreateNew(Coordiante root, Coordiante[] area)
        {
            return new GameObjectAggregatorBuilder(BuildingTypes.IronMine)
                .AddArea(root, area)
                .AddInteraction<IronMineInteraction>()
                .Build();
        }
    }
}