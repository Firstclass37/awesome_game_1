using Game.Server.Logic.Maps.Generation;
using Game.Server.Storage;

namespace Game.Server.Logic._init
{
    internal class GameInitializer : IGameInitializer
    {
        private readonly IMapGenerator _mapGenerator;
        private readonly IResourceGenerator _resourceGenerator;
        private readonly IStorage _storage;

        public GameInitializer(IMapGenerator mapGenerator, IResourceGenerator resourceGenerator, IStorage storage)
        {
            _mapGenerator = mapGenerator;
            _resourceGenerator = resourceGenerator;
            _storage = storage;
        }

        public void StartNewGame()
        {
            var players = _mapGenerator.Generate();
            _resourceGenerator.Generate();

            var game = new Models.Game { InnerGameHour = 12, Players =  players };
            _storage.Add(game);
        }
    }
}