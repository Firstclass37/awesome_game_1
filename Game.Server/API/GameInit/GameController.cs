using Game.Server.Logic;
using Game.Server.Logic._init;

namespace Game.Server.API.GameInit
{
    internal class GameController : IGameController
    {
        private readonly IGameInitializer _gameInitializer;
        private readonly IGameCycle _gameCycle;

        public GameController(IGameInitializer gameInitializer, IGameCycle gameCycle)
        {
            _gameInitializer = gameInitializer;
            _gameCycle = gameCycle;
        }

        public void StartNewGame()
        {
            _gameInitializer.StartNewGame();
        }

        public void Tick(double gameTime)
        {
            _gameCycle.Tick(gameTime);
        }
    }
}