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

namespace Game.Server.Logic.Objects.SiliconQuarry.Creation
{
    internal class SiliconQuarryFactory : IGameObjectFactory
    {
        public GameObjectAggregator CreateNew(Coordiante root, Coordiante[] area, int player)
        {
            return new GameObjectAggregatorBuilder(BuildingTypes.SiliconQuarry, player)
                .AddArea(root, area)
                .AsManufactoring(new ManufactoringArgs
                {
                    PrduceSpeedSeconds = 0,
                    ProduceAction = TypeInfoFactory.Create<IProduceAction, SwapResourcesProduceAction>(),
                    Requirements = new[] { TypeInfoFactory.Create<IProduceRequirement, EnoughtResourceRequirement>() },
                    RequriedResources = new[] { ResourceChunk.Create(ResourceType.Energy, 0.1f) },
                    ResultResources = new[] { ResourceChunk.Create(ResourceType.Silicon, 1) }
                })
                .AsInteractable<ProduceBuildingInteraction>()
                .Build();
        }
    }
}