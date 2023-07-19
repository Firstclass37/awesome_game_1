using Game.Server.Models;
using Game.Server.Models.GameObjects;
using Game.Server.Storage;

namespace Game.Server.Logic.Systems
{
    internal class MovementSystem : ISystem
    {
        private readonly IStorage _storage;
        private readonly int _speed = 1;

        public MovementSystem(IStorage storage)
        {
            _storage = storage;
        }

        public void Process(double gameTime)
        {
            var movements = _storage.Find<Movement>(m => m.Active);
            foreach (var movement in movements) 
            {
                if ((gameTime -  movement.LastMovementTime) > _speed)
                {
                    var currentPosition = _storage.Find<GameObjectPosition>(p => p.EntityId == movement.GameObjectId).ToArray();
                    if (currentPosition.Length > 1)
                        throw new Exception($"movement system dosn't support huge object movement yet");

                    var currentSinglePosition = currentPosition.First();
                    var nextIndex = Array.IndexOf(movement.Path, currentSinglePosition.Coordiante) + 1;
                    var nextPoint = movement.Path[nextIndex];

                    _storage.Update(currentSinglePosition with { Coordiante = nextPoint });
                    _storage.Update(movement with { LastMovementTime = gameTime, Active = nextIndex == movement.Path.Length - 1 });
                }
            }
        }
    }
}