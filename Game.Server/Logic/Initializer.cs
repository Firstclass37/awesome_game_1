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
            
        }
    }
}