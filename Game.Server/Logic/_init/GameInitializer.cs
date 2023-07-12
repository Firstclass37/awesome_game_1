using Game.Server.Logic.Maps.Preset;
using Game.Server.Models.Maps;
using Game.Server.Storage;

namespace Game.Server.Logic._init
{
    internal class GameInitializer
    {
        private readonly IPresetLoader _presetLoader;
        private readonly IStorage _storage;

        public GameInitializer(IPresetLoader presetLoader, IStorage storage)
        {
            _presetLoader = presetLoader;
            _storage = storage;

            PopulateMapGrid();
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

            Console.WriteLine("ALL COMPLETED");
        }
    }
}