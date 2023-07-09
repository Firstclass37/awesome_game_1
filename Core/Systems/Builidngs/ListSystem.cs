using Game.Server.API.Buildings;
using Godot;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Helpers;
using My_awesome_character.Core.Ui;
using System.Linq;

namespace My_awesome_character.Core.Systems.Builidngs
{
    internal class ListSystem : ISystem
    {
        private readonly IBuildingController _buildingController;
        private readonly ISceneAccessor _sceneAccessor;

        public ListSystem(IBuildingController buildingController, ISceneAccessor sceneAccessor)
        {
            _buildingController = buildingController;
            _sceneAccessor = sceneAccessor;
        }

        public void OnStart()
        {
            var buildings = _buildingController.GetBuildableList();

            var container = _sceneAccessor.GetScene<Node2D>(SceneNames.Game).GetNode<Container>("BuildingsContainer");
            foreach(var building in buildings) 
            {
                var resources = building.Prices.Select(p => Create(building.BuildingType, p.resourceType, p.count)).ToArray();

                var buildingPreviewInfo = SceneFactory.Create<BuildingsPreview>(SceneNames.BuidlingPreviewInfo(building.BuildingType), ScenePaths.BuidlingPreviewInfo);
                buildingPreviewInfo.BuildingTexture = new BuildingPreviewInfoSelector().Select(building.BuildingType);
                buildingPreviewInfo.Description = building.Description;
                buildingPreviewInfo.AddPrices(resources);

                container.AddChild(buildingPreviewInfo);
            }
        }

        public void Process(double gameTime)
        {
        }

        private Resource Create(string objecyType, int resourceId, int amount)
        {
            var resource = SceneFactory.Create<Resource>(SceneNames.ResourceCost(objecyType, resourceId), ScenePaths.ResourceInfo);
            resource.ResourceType = resourceId;
            resource.Amount = amount;
            resource.PreviewTexture = new ResourcePreviewTextureSelector().Select(resourceId);
            return resource;
        }
    }
}