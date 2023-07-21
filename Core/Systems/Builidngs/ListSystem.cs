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

            var buildingCollection = _sceneAccessor.FindFirst<BuildingCollection>(SceneNames.BuildingCollection);
            foreach(var building in buildings) 
            {
                var resources = building.Prices.Select(p => Create(building.BuildingType, p.resourceType, (int)p.count)).ToArray();

                var buildingPreviewInfo = SceneFactory.Create<BuildingsPreview>(SceneNames.BuidlingPreviewInfo(building.BuildingType), ScenePaths.BuidlingPreviewInfo);
                buildingPreviewInfo.BuildingTexture = new BuildingPreviewInfoSelector().Select(building.BuildingType);
                buildingPreviewInfo.BuildingType = building.BuildingType;
                buildingPreviewInfo.Description = building.Description;
                buildingPreviewInfo.AddPrices(resources);
                buildingPreviewInfo.OnClick += BuildingPreviewInfo_OnClick;
                buildingPreviewInfo.OnMouseEnter += BuildingPreviewInfo_OnMouseEnter;
                buildingPreviewInfo.OnMouseLeave += BuildingPreviewInfo_OnMouseLeave;

                buildingCollection.AddBuilding(buildingPreviewInfo);
            }
        }

        public void Process(double gameTime)
        {
            if (Input.IsActionJustPressed("Esc"))
                UnselectAll();
        }

        private void BuildingPreviewInfo_OnMouseLeave(BuildingsPreview obj)
        {
            obj.Hovered = false;
        }

        private void BuildingPreviewInfo_OnMouseEnter(BuildingsPreview obj)
        {
            obj.Hovered = true;
        }

        private void BuildingPreviewInfo_OnClick(BuildingsPreview obj)
        {
            UnselectAll();
            obj.IsSelected = !obj.IsSelected;
        }

        private void UnselectAll()
        {
            var buildingCollection = _sceneAccessor.FindFirst<BuildingCollection>(SceneNames.BuildingCollection);
            foreach (var building in buildingCollection.GetList())
                building.IsSelected = false;
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