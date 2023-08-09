using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Resources;

namespace Game.Server.Logic.Core
{
    internal class BuildingAvailability : IBuildingAvailability
    {
        private readonly IBuidlingPricing _buidlingPricing;
        private readonly IResourceManager _resourceManager;

        public BuildingAvailability(IBuidlingPricing buidlingPricing, IResourceManager resourceManager)
        {
            _buidlingPricing = buidlingPricing;
            _resourceManager = resourceManager;
        }

        public bool IsAvailable(IGameObjectMetadata objectMetadata)
        {
            var actualPrice = _buidlingPricing.GetActualPriceFor(objectMetadata);
            return actualPrice.Chunks.All(c => _resourceManager.GetAmount(c.ResourceId) >= c.Amout);
        }
    }
}