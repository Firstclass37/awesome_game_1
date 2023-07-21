using Game.Server.API.Buildings;
using Game.Server.Models.Maps;
using Godot;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game;
using My_awesome_character.Core.Helpers;
using My_awesome_character.Core.Ui;
using System.Linq;
using Coordiante = Game.Server.Models.Maps.Coordiante;

namespace My_awesome_character.Core.Systems.Homes
{
    internal class HomePreviewSystem : ISystem
    {
        private readonly ISceneAccessor _sceneAccessor;
        private readonly IBuildingController _buildingController;

        public HomePreviewSystem(ISceneAccessor sceneAccessor, IBuildingController buildingController)
        {
            _sceneAccessor = sceneAccessor;
            _buildingController = buildingController;
        }

        public void OnStart()
        {
        }

        public void Process(double gameTime)
        {
            if (Input.IsActionJustPressed("Esc"))
            {
                ClearPreview();
            }
            else if (Input.IsActionJustPressed("left-click"))
            {
                var map = _sceneAccessor.FindFirst<Map>(SceneNames.Map);
                var globalMousePos = map.GetGlobalMousePosition();
                if (map.IsMouseExistsNew(globalMousePos, out var cell))
                {
                    ClearPreview();
                    CreateBuildingOn(cell);
                }
            }
            else
            {
                var selected = FindSelectedBuilding();
                if (selected == null)
                    return;

                var map = _sceneAccessor.FindFirst<Map>(SceneNames.Map);
                var globalMousePos = map.GetGlobalMousePosition();
                if (map.IsMouseExistsNew(globalMousePos, out var cell))
                {
                    ClearPreview();
                    ShowPreview(cell, map);
                }
                else
                {
                    ClearPreview();
                }
            }
        }

        private void CreateBuildingOn(Game.Coordiante mapCoordinate)
        {
            var selected = FindSelectedBuilding();
            if (selected == null)
                return;

            var coordinate = new Coordiante(mapCoordinate.X, mapCoordinate.Y);
            _buildingController.Build(selected.BuildingType, coordinate);
        }

        private void ShowPreview(Game.Coordiante mapCoordinate, Map map)
        {
            var selected = FindSelectedBuilding();
            if (selected == null)
                return;

            var canBuild = _buildingController.CanBuild(selected.BuildingType, new Coordiante(mapCoordinate.X, mapCoordinate.Y));
            var selectStyle = canBuild ? 1 : 2;
            var tile = new NewBuildingTileSelector().Select(selected.BuildingType);

            map.SetCellPreview(mapCoordinate, tile, selectStyle);
        }

        private BuildingsPreview FindSelectedBuilding()
        {
            var buidlingCollection = _sceneAccessor.FindFirst<BuildingCollection>(SceneNames.BuildingCollection);
            var selected = buidlingCollection.GetList().FirstOrDefault(b => b.IsSelected);
            return selected;
        }

        private void ClearPreview()
        {
            _sceneAccessor.FindFirst<Map>(SceneNames.Map).ClearLayer(MapLayers.Preview);
        }
    }
}