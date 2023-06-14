using Godot;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game.Constants;
using My_awesome_character.Core.Game.Resources;
using My_awesome_character.Core.Helpers;
using My_awesome_character.Core.Ui;

namespace My_awesome_character.Core.Systems.Resources
{
    internal class InitResourcesInfoSystem : ISystem
    {
        private readonly ISceneAccessor _sceneAccessor;
        private readonly IResourceManager _resourceManager;

        public InitResourcesInfoSystem(ISceneAccessor sceneAccessor, IResourceManager resourceManager)
        {
            _sceneAccessor = sceneAccessor;
            _resourceManager = resourceManager;
        }

        public void OnStart()
        {
            var resourceContainer = _sceneAccessor.GetScene<Node2D>(SceneNames.Game).GetNode<HBoxContainer>("ResourceContainer");

            var textureSelector = new ResourcePreviewTextureSelector();
            foreach(var resource in _resourceManager.GetList())
                resourceContainer.AddChild(Create(resource, textureSelector, _resourceManager.GetAmount(resource)));
        }

        public void Process(double gameTime)
        {
        }

        private Resource Create(int resourceId, ISelector<int, Texture2D> textureSelector, int amount)
        {
            var resource = SceneFactory.Create<Resource>(SceneNames.ResourceInfo(resourceId), ScenePaths.ResourceInfo);
            resource.ResourceType = resourceId;
            resource.Amount = amount;
            resource.PreviewTexture = textureSelector.Select(resourceId);
            return resource;
        }
    }
}