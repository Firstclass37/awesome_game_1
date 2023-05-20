using Godot;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game.Events.Homes;
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

        public HomePreviewSystem(IEventAggregator eventAggregator, ISceneAccessor sceneAccessor)
        {
            _eventAggregator = eventAggregator;
            _sceneAccessor = sceneAccessor;
        }

        public void OnStart()
        {
            _eventAggregator.GetEvent<GameEvent<HomePreviewEvent>>().Subscribe(OnCreated);
            _eventAggregator.GetEvent<GameEvent<HomePreviewCanceledEvent>>().Subscribe(OnCenceled);
        }

        public void Process(double gameTime)
        {
        }

        private void OnCenceled(HomePreviewCanceledEvent obj)
        {
            var game = _sceneAccessor.FindFirst<Node2D>(SceneNames.Game);
            var map = _sceneAccessor.FindFirst<Map>(SceneNames.Map);
            var existingPreview = _sceneAccessor.FindFirst<Home>(SceneNames.Builidng_preview(typeof(Home)));
            if (existingPreview == null)
                return;

            map.TileMap.SetCell(MapLayers.Buildings, new Vector2I(existingPreview.RootCell.X, existingPreview.RootCell.Y), -1);
            game.RemoveChild(existingPreview);
        }

        private void OnCreated(HomePreviewEvent homePreviewEvent)
        {
            OnCenceled(new HomePreviewCanceledEvent());

            var targetCell = homePreviewEvent.TargetCell;
            var alreadyBuiltHome = _sceneAccessor.FindAll<Home>().Where(h => h.Id != default).Any(h => h.RootCell == targetCell);
            if (alreadyBuiltHome)
                return;

            var size = GetSize(targetCell);
            var otherHomes = _sceneAccessor.FindAll<Home>();
            var selectStyle = otherHomes.Any(h => h.Cells.Intersect(size).Any()) ? 4 : 1;

            var map = _sceneAccessor.FindFirst<Map>(SceneNames.Map);
            var home = SceneFactory.Create<Home>(SceneNames.Builidng_preview(typeof(Home)), ScenePaths.HomeFactory);
            home.SpawnCell = new MapCell(targetCell.X, targetCell.Y + 3, MapCellType.Groud);
            home.Cells = GetSize(targetCell);
            home.RootCell = targetCell;

            var game = _sceneAccessor.GetScene<Node2D>(SceneNames.Game);
            game.AddChild(home);
            map.TileMap.SetCell(MapLayers.Buildings, new Vector2I(targetCell.X, targetCell.Y), 5, new Vector2I(0, 0), selectStyle);
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