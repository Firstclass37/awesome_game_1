using Game.Server.Logic.Maps.Generation;
using Game.Server.Models.Weather;
using Game.Server.Storage;

namespace Game.Server.Logic._init
{
    internal class GameInitializer : IGameInitializer
    {
        private readonly IMapGenerator _mapGenerator;
        private readonly IResourceGenerator _resourceGenerator;
        private readonly IBuildingGenerator _buildingGenerator;
        private readonly IStorage _storage;

        public GameInitializer(IMapGenerator mapGenerator, IResourceGenerator resourceGenerator, IStorage storage, IBuildingGenerator buildingGenerator)
        {
            _mapGenerator = mapGenerator;
            _resourceGenerator = resourceGenerator;
            _storage = storage;
            _buildingGenerator = buildingGenerator;
        }

        public void StartNewGame()
        {
            var players = _mapGenerator.Generate();
            _resourceGenerator.Generate();
            _buildingGenerator.Generate();

            var game = new Models.Game { Players =  players };
            var gameTime = new GameTime { Hours = 12, TimeOfDay = Models.Constants.TimeOfDay.Day };
            _storage.Add(game);
            _storage.Add(gameTime);
        }
    }
}