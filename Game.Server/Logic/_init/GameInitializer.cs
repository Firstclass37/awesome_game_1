using Game.Server.Logic.Maps.Preset;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Models.Constants;
using Game.Server.Models.Maps;
using Game.Server.Storage;

namespace Game.Server.Logic._init
{
    internal class GameInitializer
    {
        private readonly IGameObjectCreator _gameObjectCreator;
        private readonly IPresetLoader _presetLoader;
        private readonly IStorage _storage;

        public GameInitializer(IPresetLoader presetLoader, IStorage storage, IGameObjectCreator gameObjectCreator)
        {
            _presetLoader = presetLoader;
            _storage = storage;
            _gameObjectCreator = gameObjectCreator;

            PopulateMapGrid();
            PopulateGround();
            PopulateHome();
        }

        private void PopulateHome()
        {
            var coordinate = new Coordiante(7, 21);
            var home = _gameObjectCreator.Create(BuildingTypes.Home, coordinate, null);
            if (home == null)
                throw new Exception($"cant create home at [{coordinate.X} {coordinate.Y}]");
        }

        private void PopulateGround()
        {
            var cells = _storage.Find<IsometricMapCell>(c => true);
            foreach(var cell in cells)
                _gameObjectCreator.Create(GroundTypes.Ground, cell.Coordiante, null);
        }

        private void PopulateMapGrid()
        {
            var map = _presetLoader.Load();
            foreach (var cell in map)
                _storage.Add(new IsometricMapCell(cell.Coordinate));

            foreach (var cell in map)
            {
                var dbValue = _storage.Find<IsometricMapCell>(p => p.Coordiante.Equals(cell.Coordinate)).First();
                var neighbors = cell.Neightbors.ToDictionary(n => _storage.Find<IsometricMapCell>(p => p.Coordiante.Equals(n.Coordinate)).First(), n => n.Direction);

                var newCell = new IsometricMapCell(dbValue.Coordiante, neighbors) { Id = dbValue.Id };
                _storage.Update(newCell);
            }
        }
    }
}