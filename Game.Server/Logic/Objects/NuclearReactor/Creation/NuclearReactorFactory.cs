using Game.Server.DataBuilding;
using Game.Server.Logic._Extentions;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Interactions;
using Game.Server.Logic.Objects._Produce;
using Game.Server.Models.Constants;
using Game.Server.Models.Constants.Attributes;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;
using Game.Server.Models.Resources;

namespace Game.Server.Logic.Objects.NuclearReactor.Creation
{
    internal class NuclearReactorFactory : IGameObjectFactory
    {
        public GameObjectAggregator CreateNew(Coordiante root, Coordiante[] area, int player)
        {
            return new GameObjectAggregatorBuilder(BuildingTypes.NuclearReactor, player)
                .AddArea(root, area)
                .AsManufactoring(new ManufactoringArgs
                {
                    PrduceSpeedSeconds = 0,
                    ProduceAction = TypeInfoFactory.Create<IProduceAction, SwapResourcesProduceAction>(),
                    Requirements = new[] { TypeInfoFactory.Create<IProduceRequirement, EnoughtResourceRequirement>() },
                    RequriedResources = new[] { ResourceChunk.Create(ResourceType.Uranus, 1), ResourceChunk.Create(ResourceType.Coal, 0.5f), 
                                                ResourceChunk.Create(ResourceType.Water, 0.5f) },
                    ResultResources = new[] { ResourceChunk.Create(ResourceType.Energy, 4) }
                })
                .AsInteractable<ProduceBuildingInteraction>()
                .Build();
        }
    }
}