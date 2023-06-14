using Godot;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game.Constants;
using My_awesome_character.Core.Helpers;
using My_awesome_character.Core.Ui;

namespace My_awesome_character.Core.Systems.Resources
{
    internal class InitResourcesInfoSystem : ISystem
    {
        private readonly ISceneAccessor _sceneAccessor;

        public InitResourcesInfoSystem(ISceneAccessor sceneAccessor)
        {
            _sceneAccessor = sceneAccessor;
        }

        public void OnStart()
        {
            var resourceContainer = _sceneAccessor.GetScene<Node2D>(SceneNames.Game).GetNode<HBoxContainer>("ResourceContainer");

            var textureSelector = new ResourcePreviewTextureSelector();

            resourceContainer.AddChild(Create(ResourceType.Money, textureSelector));
            resourceContainer.AddChild(Create(ResourceType.Water, textureSelector));
            resourceContainer.AddChild(Create(ResourceType.Food, textureSelector));
            resourceContainer.AddChild(Create(ResourceType.Electricity, textureSelector));
            resourceContainer.AddChild(Create(ResourceType.Steel, textureSelector));
            resourceContainer.AddChild(Create(ResourceType.Uranus, textureSelector));
            resourceContainer.AddChild(Create(ResourceType.Microchip, textureSelector, 100));
        }

        public void Process(double gameTime)
        {
        }

        private Resource Create(int resourceId, ISelector<int, Texture2D> textureSelector, int amount = 0)
        {
            var resource = SceneFactory.Create<Resource>(SceneNames.ResourceInfo(resourceId), ScenePaths.ResourceInfo);
            resource.ResourceType = resourceId;
            resource.Amount = amount;
            resource.PreviewTexture = textureSelector.Select(resourceId);
            return resource;
        }
    }
}