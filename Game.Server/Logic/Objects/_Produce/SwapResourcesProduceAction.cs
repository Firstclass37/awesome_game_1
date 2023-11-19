using Game.Server.Logic.Resources;
using Game.Server.Models.Constants.Attributes;
using Game.Server.Models.GameObjects;

namespace Game.Server.Logic.Objects._Produce
{
    internal class SwapResourcesProduceAction : IProduceAction
    {
        private readonly IResourceManager _resourceManager;

        public SwapResourcesProduceAction(IResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }

        public bool Produce(GameObjectAggregator gameObject)
        {
            var requiredResources = gameObject.GetAttributeValue(ManufactureAttributes.RequriedResources);
            var resultResources = gameObject.GetAttributeValue(ManufactureAttributes.ResultResources);

            if (resultResources == null || !resultResources.Any())
                throw new ArgumentException($"can't swap resource object {gameObject.GameObject.Id} becouse target resources is empty");

            if (requiredResources.All(r => _resourceManager.GetAmount(r.ResourceId) > r.Amout))
            {
                foreach (var resource in requiredResources)
                    _resourceManager.TrySpend(resource.ResourceId, resource.Amout);

                foreach (var resource in resultResources)
                    _resourceManager.Increase(resource.ResourceId, resource.Amout);

                return true;
            }
            return false;
        }
    }
}