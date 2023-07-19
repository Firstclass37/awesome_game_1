using Game.Server.Logic._init;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Server.API.GameInit
{
    internal class GameController : IGameController
    {
        private readonly IGameInitializer _gameInitializer;

        public GameController(IGameInitializer gameInitializer)
        {
            _gameInitializer = gameInitializer;
        }

        public void StartNewGame()
        {
            _gameInitializer.StartNewGame();
        }
    }
}