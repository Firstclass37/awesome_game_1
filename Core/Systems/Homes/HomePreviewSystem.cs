using Godot;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game.Buildings;
using My_awesome_character.Core.Game.Events.Homes;
using My_awesome_character.Core.Helpers;
using My_awesome_character.Core.Infrastructure.Events;
using My_awesome_character.Core.Ui;
using My_awesome_character.Entities;
using System.Collections.Generic;
using System.Linq;

namespace My_awesome_character.Core.Systems.Homes
{
    internal class HomePreviewSystem : ISystem
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ISceneAccessor _sceneAccessor;
        private readonly IBuildRequirementProvider _buildRequirementProvider;

        public HomePreviewSystem(IEventAggregator eventAggregator, ISceneAccessor sceneAccessor, IBuildRequirementProvider buildRequirementProvider)
        {
            _eventAggregator = eventAggregator;
            _sceneAccessor = sceneAccessor;
            _buildRequirementProvider = buildRequirementProvider;
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

            var cell = new MapCell(existingPreview.RootCell.X, existingPreview.RootCell.Y, MapCellType.Building);
            map.ClearPreview(cell);
            game.RemoveChild(existingPreview);
        }

        private void OnCreated(BuildingPreviewEvent homePreviewEvent)
        {
            OnCenceled(new BuildingPreviewCanceledEvent());

            var targetCell = new MapCell(homePreviewEvent.TargetCell.X, homePreviewEvent.TargetCell.Y, MapCellType.Building); 
            var alreadyBuiltHome = _sceneAccessor.FindAll<Home>().Where(h => h.Id != default).Any(h => h.RootCell == targetCell);
            if (alreadyBuiltHome)
                return;

            var map = _sceneAccessor.FindFirst<Map>(SceneNames.Map);

            var size = GetSize(targetCell, map).ToArray();
            var whereWantToBuild = map.GetCells().Where(c => size.Contains(c)).ToArray();
            var otherHomes = _sceneAccessor.FindAll<Home>();
            var canBuildHere = _buildRequirementProvider.GetRequirementFor(homePreviewEvent.BuildingType).CanBuild(whereWantToBuild);
            var selectStyle = otherHomes.Any(h => h.Cells.Intersect(size).Any()) || !canBuildHere ? 2 : 1;
            var tile = new BuildingTileSelector().Select(homePreviewEvent.BuildingType);

            var home = SceneFactory.Create<Home>(SceneNames.Builidng_preview(typeof(Home)), ScenePaths.HomeFactory);
            home.Cells = size;
            home.RootCell = targetCell;

            var game = _sceneAccessor.GetScene<Node2D>(SceneNames.Game);
            game.AddChild(home);
            map.SetCellPreview(home.RootCell, tile, selectStyle);
        }

        private IEnumerable<MapCell> GetSize(MapCell center, Map map)
        {
            var area = map.Get2x2Area(center);
            foreach (var cell in area)
            {
                var c = cell;
                c.CellType = MapCellType.Building;
                yield return c;
            }
        }
    }
}