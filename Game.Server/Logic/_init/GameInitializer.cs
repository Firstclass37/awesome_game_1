using Game.Server.Logic.Maps.Generation;

namespace Game.Server.Logic._init
{
    internal class GameInitializer : IGameInitializer
    {
        private readonly IMapGenerator _mapGenerator;
        private readonly IResourceGenerator _resourceGenerator;

        public GameInitializer(IMapGenerator mapGenerator, IResourceGenerator resourceGenerator)
        {
            _mapGenerator = mapGenerator;
            _resourceGenerator = resourceGenerator;
        }

        public void StartNewGame()
        {
            _mapGenerator.Generate();
            _resourceGenerator.Generate();
        }
    }
}