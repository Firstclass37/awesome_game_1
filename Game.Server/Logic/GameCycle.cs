using Game.Server.Logic.Systems;

namespace Game.Server.Logic
{
    internal class GameCycle : IGameCycle
    {
        private readonly ISystem[] _systems;

        public GameCycle(ISystem[] systems)
        {
            _systems = systems;
        }

        public void Tick(double gameTimeMs)
        {
            foreach (var system in _systems)
                system.Process(gameTimeMs);
        }
    }
}