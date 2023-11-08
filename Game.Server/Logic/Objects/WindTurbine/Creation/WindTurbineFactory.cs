using Game.Server.DataBuilding;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects.WindTurbine.Interaction;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.WindTurbine.Creation
{
    internal class WindTurbineFactory : IGameObjectFactory
    {
        public GameObjectAggregator CreateNew(Coordiante root, Coordiante[] area, int player)
        {
            return new GameObjectAggregatorBuilder(BuildingTypes.WindTurbine, player)
                .AddArea(root, area)
                .AddInteraction<WindTurbineInteraction>()
                .Build();
        }
    }
}