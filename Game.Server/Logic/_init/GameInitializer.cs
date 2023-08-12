using Game.Server.Logic.Maps.Generation;

namespace Game.Server.Logic._init
{
    internal class GameInitializer : IGameInitializer
    {
        private readonly IMapGenerator _mapGenerator;

        public GameInitializer(IMapGenerator mapGenerator)
        {
            _mapGenerator = mapGenerator;
        }

        public void StartNewGame()
        {
            _mapGenerator.Generate();
        }
    }
}