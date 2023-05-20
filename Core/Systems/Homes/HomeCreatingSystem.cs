using Godot;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game.Events.Homes;
using My_awesome_character.Core.Infrastructure.Events;
using My_awesome_character.Core.System;
using My_awesome_character.Core.Ui;
using My_awesome_character.Entities;
using System;
using System.Linq;

namespace My_awesome_character.Core.Systems.Homes
{
    internal class HomeCreatingSystem : ISystem
    {
        private readonly ISceneAccessor _sceneAccessor;
        private readonly IEventAggregator _eventAggregator;

        public HomeCreatingSystem(ISceneAccessor sceneAccessor, IEventAggregator eventAggregator)
        {
            _sceneAccessor = sceneAccessor;
            _eventAggregator = eventAggregator;
        }

        public void OnStart()
        {
            _sceneAccessor.FindFirst<Map>(SceneNames.Map).OnCellClicked += HomeCreatingSystem_OnCellClicked;
            _eventAggregator.GetEvent<GameEvent<HomeCreateRequestEvent>>().Subscribe(OnRequested);
        }

        private void OnRequested(HomeCreateRequestEvent obj)
        {
            HomeCreatingSystem_OnCellClicked(obj.TargetCell);
        }

        public void Process(double gameTime)
        {
        }

        private void HomeCreatingSystem_OnCellClicked(MapCell obj)
        {
            var size = GetSize(obj);
            var otherHomes = _sceneAccessor.FindAll<Home>();
            if (otherHomes.Any(h => h.Cells.Intersect(size).Any()))
                return;

            var map = _sceneAccessor.FindFirst<Map>(SceneNames.Map);
            var newHomeId = otherHomes.Any() ? otherHomes.Max(h => h.Id) + 1 : 1;
            var home = SceneFactory.Create<Home>(SceneNames.HomeFactory(newHomeId), ScenePaths.HomeFactory);
            home.Id = newHomeId;
            home.SpawnCell = new MapCell(obj.X, obj.Y + 3, MapCellType.Groud);
            home.LastFireTime = SystemNode.GameTime;
            home.SpawnEverySecond = 5;
            home.Cells = size;
            home.RootCell = obj;

            var game = _sceneAccessor.GetScene<Node2D>(SceneNames.Game);
            game.AddChild(home);
            map.TileMap.SetCell(MapLayers.Buildings, new Vector2I(obj.X, obj.Y), 5, new Vector2I(0, 0));

            GD.Print($"home created on: {obj}");
        }

        private MapCell[] GetSize(MapCell center)
        {
            var first = new MapCell(center.X, center.Y + 1, MapCellType.Building);
            var second = new MapCell(center.X - 1, center.Y + 1, MapCellType.Building);
            var third = new MapCell(center.X, center.Y + 2, MapCellType.Building);
            var spawn = new MapCell(center.X, center.Y + 3, MapCellType.Building);
            return new MapCell[] { center, first, second, third, spawn };
        }
    }
}