using Game.Server.DataBuilding;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects.ChemicalFactory.Interaction;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.ChemicalFactory.Creation
{
    internal class ChemicalFactoryFactory : IGameObjectFactory
    {
        public GameObjectAggregator CreateNew(Coordiante root, Coordiante[] area, int player)
        {
            return new GameObjectAggregatorBuilder(BuildingTypes.ChemicalFactory, player)
                .AddArea(root, area)
                .AddInteraction<ChemicalFactoryInteraction>()
                .Build();
        }
    }
}