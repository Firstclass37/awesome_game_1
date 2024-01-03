using Game.Server.API.Resources;
using Godot;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Helpers;
using My_awesome_character.Core.Ui;

namespace My_awesome_character.Core.Systems.Resources
{
    internal class InitResourcesInfoSystem : ISystem
    {
        private readonly ISceneAccessor _sceneAccessor;
        private readonly IResourceController _resourceController;

        public InitResourcesInfoSystem(ISceneAccessor sceneAccessor, IResourceController resourceController)
        {
            _sceneAccessor = sceneAccessor;
            _resourceController = resourceController;
        }

        public void OnStart()
        {
            var resourceContainer = _sceneAccessor.FindFirst<Container>("ResourceContainer", isStatic: true);

            var textureSelector = new ResourcePreviewTextureSelector();
            foreach(var resource in _resourceController.GetList())
                resourceContainer.AddChild(Create(resource, textureSelector));
        }

        public void Process(double gameTime)
        {
        }

        private Resource Create(ResourceInfo resourceInfo, ISelector<int, Texture2D> textureSelector)
        {
            var resource = SceneFactory.Create<Resource>(SceneNames.ResourceInfo(resourceInfo.Id), ScenePaths.ResourceInfo);
            resource.ResourceType = resourceInfo.Id;
            resource.Amount = (int)resourceInfo.Amout;
            resource.Description = resourceInfo.Name;
            resource.PreviewTexture = textureSelector.Select(resourceInfo.Id);
            return resource;
        }
    }
}