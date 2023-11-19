using Game.Server.Logic.Resources;
using Game.Server.Models.Constants.Attributes;
using Game.Server.Models.GameObjects;

namespace Game.Server.Logic.Objects._Produce
{
    internal class EnoughtResourceRequirement : IProduceRequirement
    {
        private readonly IResourceManager _resourceManager;

        public EnoughtResourceRequirement(IResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }

        public bool Can(GameObjectAggregator gameObjectAggregator)
        {
            var requiredResource = gameObjectAggregator.GetAttributeValue(ManufactureAttributes.RequriedResources);
            return requiredResource.All(r => _resourceManager.GetAmount(r.ResourceId) >= r.Amout);
        }
    }
}