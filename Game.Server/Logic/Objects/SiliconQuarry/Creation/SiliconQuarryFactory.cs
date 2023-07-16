using Game.Server.DataBuilding;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects.SiliconQuarry.Interaction;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.SiliconQuarry.Creation
{
    internal class SiliconQuarryFactory : IGameObjectFactory
    {
        public GameObjectAggregator CreateNew(Coordiante root, Coordiante[] area)
        {
            return new GameObjectAggregatorBuilder(BuildingTypes.SiliconQuarry)
                .AddArea(root, area)
                .AddInteraction<SiliconQuarryInteraction>()
                .Build();
        }
    }
}