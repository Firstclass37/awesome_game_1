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
    internal class HomePreviewSystem : ISystem
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ISceneAccessor _sceneAccessor;
        private readonly IBuildRequirementProvider _buildRequirementProvider;
        private readonly IBuildingFactoryProvider _buildingFactoryProvider;

        public HomePreviewSystem(IEventAggregator eventAggregator, ISceneAccessor sceneAccessor, IBuildRequirementProvider buildRequirementProvider, IBuildingFactoryProvider buildingFactoryProvider)
        {
            _eventAggregator = eventAggregator;
            _sceneAccessor = sceneAccessor;
            _buildRequirementProvider = buildRequirementProvider;
            _buildingFactoryProvider = buildingFactoryProvider;
        }

        public void OnStart()
        {
            _eventAggregator.GetEvent<GameEvent<BuildingPreviewEvent>>().Subscribe(OnCreated);
            _eventAggregator.GetEvent<GameEvent<BuildingPreviewCanceledEvent>>().Subscribe(OnCenceled);
        }

        public void Process(double gameTime)
        {
        }

        private void OnCenceled(BuildingPreviewCanceledEvent obj)
        {
            var game = _sceneAccessor.FindFirst<Node2D>(SceneNames.Game);
            var map = _sceneAccessor.FindFirst<Map>(SceneNames.Map);
            var existingPreview = _sceneAccessor.FindFirst<Home>(SceneNames.Builidng_preview(typeof(Home)));
            if (existingPreview == null)
                return;

            map.ClearPreview(existingPreview.RootCell);
            game.RemoveChild(existingPreview);
        }

        private void OnCreated(BuildingPreviewEvent homePreviewEvent)
        {
            OnCenceled(new BuildingPreviewCanceledEvent());

            var map = _sceneAccessor.FindFirst<Map>(SceneNames.Map);
            var building = _buildingFactoryProvider.GetFor(homePreviewEvent.BuildingType).Create(homePreviewEvent.TargetCell, map, map);

            var whereWantToBuild = map.GetCells().Where(c => building.Cells.Contains(c)).ToArray();
            var otherHomes = _sceneAccessor.FindAll<Home>();
            var canBuildHere = _buildRequirementProvider.GetRequirementFor(homePreviewEvent.BuildingType).CanBuild(whereWantToBuild);
            var selectStyle = otherHomes.Any(h => h.Cells.Intersect(building.Cells).Any()) || !canBuildHere ? 2 : 1;
            var tile = new BuildingTileSelector().Select(homePreviewEvent.BuildingType);

            var home = SceneFactory.Create<Home>(SceneNames.Builidng_preview(typeof(Home)), ScenePaths.HomeFactory);
            home.Cells = building.Cells;
            home.RootCell = building.RootCell;

            var game = _sceneAccessor.GetScene<Node2D>(SceneNames.Game);
            game.AddChild(home);
            map.SetCellPreview(home.RootCell, tile, selectStyle);
        }
    }
}