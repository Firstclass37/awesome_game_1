using Game.Server.DataBuilding;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects.NuclearReactor.Interaction;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.NuclearReactor.Creation
{
    internal class NuclearReactorFactory : IGameObjectFactory
    {
        public GameObjectAggregator CreateNew(Coordiante root, Coordiante[] area)
        {
            return new GameObjectAggregatorBuilder(BuildingTypes.NuclearReactor)
                .AddArea(root, area)
                .AddInteraction<NuclearReactorInteraction>()
                .Build();
        }
    }
}