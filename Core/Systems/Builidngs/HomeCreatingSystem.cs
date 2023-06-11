using Godot;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game.Buildings.Build;
using My_awesome_character.Core.Game.Buildings.Requirements;
using My_awesome_character.Core.Game.Events.Homes;
using My_awesome_character.Core.Helpers;
using My_awesome_character.Core.Infrastructure.Events;
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
        private readonly IBuildingFactoryProvider _buildingFactoryProvider;

        public HomeCreatingSystem(ISceneAccessor sceneAccessor, IEventAggregator eventAggregator, IBuildRequirementProvider buildRequirementProvider, IBuildingFactoryProvider buildingFactoryProvider)
        {
            _sceneAccessor = sceneAccessor;
            _eventAggregator = eventAggregator;
            _buildRequirementProvider = buildRequirementProvider;
            _buildingFactoryProvider = buildingFactoryProvider;
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
            var map = _sceneAccessor.FindFirst<Map>(SceneNames.Map);
            var targetCell = obj.TargetCell;

            var building = _buildingFactoryProvider.GetFor(obj.BuildingType).Create(targetCell, map, map);
            var otherHomes = _sceneAccessor.FindAll<Home>();
            if (otherHomes.Any(h => h.Cells.Intersect(building.Cells).Any()))
                return;
            
            var whereWantToBuild = map.GetCells().Where(c => building.Cells.Contains(c)).ToArray();
            var canBuildHere = _buildRequirementProvider.GetRequirementFor(obj.BuildingType).CanBuild(whereWantToBuild);
            if (!canBuildHere)
                return;

            var home = SceneFactory.Create<Home>(SceneNames.HomeFactory(building.Id), ScenePaths.HomeFactory);
            home.Id = building.Id;
            home.Cells = building.Cells;
            home.RootCell = building.RootCell;
            home.BuildingType = obj.BuildingType;
            home.PeriodicAction = building.PeriodicAction;
            home.InteractionAction = building.InteractionAction;

            var tile = new BuildingTileSelector().Select(obj.BuildingType);
            var game = _sceneAccessor.GetScene<Node2D>(SceneNames.Game);
            game.AddChild(home);
            map.SetCell(home.RootCell, home.Cells, tile);

            Godot.GD.Print($"Home created at: {string.Join(";", home.Cells)}");
        }   
    }
}