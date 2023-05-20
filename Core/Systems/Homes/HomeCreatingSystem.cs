using Godot;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game;
using My_awesome_character.Core.Game.Buildings;
using My_awesome_character.Core.Game.Events;
using My_awesome_character.Core.Game.Events.Homes;
using My_awesome_character.Core.Helpers;
using My_awesome_character.Core.Infrastructure.Events;
using My_awesome_character.Core.System;
using My_awesome_character.Core.Ui;
using My_awesome_character.Entities;
using System.Linq;

namespace My_awesome_character.Core.Systems.Homes
{
    internal class HomeCreatingSystem : ISystem
    {
        private readonly ISceneAccessor _sceneAccessor;
        private readonly IEventAggregator _eventAggregator;
        private readonly IBuildRequirementProvider _buildRequirementProvider;

        public HomeCreatingSystem(ISceneAccessor sceneAccessor, IEventAggregator eventAggregator, IBuildRequirementProvider buildRequirementProvider)
        {
            _sceneAccessor = sceneAccessor;
            _eventAggregator = eventAggregator;
            _buildRequirementProvider = buildRequirementProvider;
        }

        public void OnStart()
        {
            _eventAggregator.GetEvent<GameEvent<HomeCreateRequestEvent>>().Subscribe(OnRequested);
        }

        public void Process(double gameTime)
        {
        }

        private void OnRequested(HomeCreateRequestEvent obj)
        {
            var targetCell = obj.TargetCell;
            var size = GetSize(targetCell);
            var otherHomes = _sceneAccessor.FindAll<Home>();
            if (otherHomes.Any(h => h.Cells.Intersect(size).Any()))
                return;

            var map = _sceneAccessor.FindFirst<Map>(SceneNames.Map);
            var whereWantToBuild = map.GetCells().Where(c => size.Contains(c)).ToArray();
            var canBuildHere = _buildRequirementProvider.GetRequirementFor(obj.BuildingType).CanBuild(whereWantToBuild);
            if (!canBuildHere)
                return;

            var rootCell = new MapCell(targetCell.X, targetCell.Y, MapCellType.Building);
            var newHomeId = otherHomes.Any() ? otherHomes.Max(h => h.Id) + 1 : 1;
            var home = SceneFactory.Create<Home>(SceneNames.HomeFactory(newHomeId), ScenePaths.HomeFactory);
            var spawnCell = new MapCell(rootCell.X, rootCell.Y + 3, MapCellType.Groud);

            home.Id = newHomeId;
            home.PeriodicAction = new CommonPeriodicAction(() => _eventAggregator.GetEvent<GameEvent<CharacterCreationRequestEvent>>().Publish(new CharacterCreationRequestEvent { InitPosition = spawnCell }), 5, SystemNode.GameTime);
            home.Cells = size;
            home.RootCell = rootCell;
            home.BuildingType = obj.BuildingType;

            var tile = new BuildingTileSelector().Select(obj.BuildingType);
            var game = _sceneAccessor.GetScene<Node2D>(SceneNames.Game);
            game.AddChild(home);
            map.SetCell(home.RootCell, home.Cells, tile);
        }

        private MapCell[] GetSize(MapCell center)
        {
            var first = new MapCell(center.X, center.Y + 1, MapCellType.Building);
            var second = new MapCell(center.X - 1, center.Y + 1, MapCellType.Building);
            var third = new MapCell(center.X, center.Y + 2, MapCellType.Building);
            var spawn = new MapCell(center.X, center.Y + 3, MapCellType.Groud);
            return new MapCell[] { center, first, second, third, spawn };
        }
    }
}