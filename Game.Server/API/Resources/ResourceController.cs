﻿using Game.Server.Logic.Resources;

namespace Game.Server.API.Resources
{
    public record ResourceInfo(int Id, string Name, float Amout);

    internal class ResourceController : IResourceController
    {
        private readonly IResourceManager _resourceManager;

        public ResourceController(IResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }

        public IReadOnlyCollection<ResourceInfo> GetList()
        {
            return _resourceManager.GetList().Select(r => new ResourceInfo(r.ResourceType, r.Name, r.Value)).ToArray();
        }
    }
}