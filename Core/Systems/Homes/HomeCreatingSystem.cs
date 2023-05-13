using Godot;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.System;
using My_awesome_character.Core.Ui;
using My_awesome_character.Entities;
using System.Linq;

namespace My_awesome_character.Core.Systems.Homes
{
    internal class HomeCreatingSystem : ISystem
    {
        private readonly ISceneAccessor _sceneAccessor;

        public HomeCreatingSystem(ISceneAccessor sceneAccessor)
        {
            _sceneAccessor = sceneAccessor;
        }

        public void OnStart()
        {
            _sceneAccessor.GetScene<Map>(SceneNames.Map).OnCellClicked += HomeCreatingSystem_OnCellClicked;
        }

        public void Process(double gameTime)
        {
        }

        private void HomeCreatingSystem_OnCellClicked(MapCell obj)
        {
            var otherHomes = _sceneAccessor.FindAll<Home>();
            if (otherHomes.Any(h => h.MapPosition == obj))
                return;

            var map = _sceneAccessor.FindFirst<Map>(SceneNames.Map);

            var newHomeId = otherHomes.Any() ? otherHomes.Max(h => h.Id) + 1 : 1;
            var home = SceneFactory.Create<Home>(SceneNames.HomeFactory(newHomeId), ScenePaths.HomeFactory);
            home.Id = newHomeId;
            home.SpawnCell = obj;
            home.LastFireTime = SystemNode.GameTime;
            home.SpawnEverySecond = 5;
            home.MapPosition = obj;

            var game = _sceneAccessor.GetScene<Node2D>(SceneNames.Game);
            game.AddChild(home);
            map.TileMap.SetCell(MapLayers.BuildingWallsLayer, new Vector2I(obj.X, obj.Y), 5, new Vector2I(0, 0));

            GD.Print($"home created on: {obj}");
        }
    }
}