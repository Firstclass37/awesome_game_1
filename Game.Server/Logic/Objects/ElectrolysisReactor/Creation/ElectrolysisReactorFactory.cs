using Game.Server.DataBuilding;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects.ElectrolysisReactor.Interactions;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.ElectrolysisReactor.Creation
{
    internal class ElectrolysisReactorFactory : IGameObjectFactory
    {
        public GameObjectAggregator CreateNew(Coordiante root, Coordiante[] area) => new GameObjectAggregatorBuilder(BuildingTypes.ElectrolysisReactor)
                .AddArea(root, area)
                .AddInteraction<ElectrolysisReactorInteraction>()
                .Build();
    }
}