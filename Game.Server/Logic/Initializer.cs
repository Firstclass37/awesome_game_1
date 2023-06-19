using Game.Server.Logic.Resources;
using Game.Server.Models.Constants;

namespace Game.Server.Logic
{
    internal class Initializer
    {
        private readonly IResourceManager _resourceManager;

        public Initializer(IResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }

        public void Init()
        {
            _resourceManager.AddResource(ResourceType.Money, 0);
            _resourceManager.AddResource(ResourceType.Water, 0);
            _resourceManager.AddResource(ResourceType.Food, 0);
            _resourceManager.AddResource(ResourceType.Electricity, 0);
            _resourceManager.AddResource(ResourceType.Steel, 0);
            _resourceManager.AddResource(ResourceType.Uranus, 0);
            _resourceManager.AddResource(ResourceType.Microchip, 100);
        }
    }
}