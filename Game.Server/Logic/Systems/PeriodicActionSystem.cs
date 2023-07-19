using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects._PeriodicAction;
using Game.Server.Models;
using Game.Server.Storage;

namespace Game.Server.Logic.Systems
{
    internal class PeriodicActionSystem : ISystem
    {
        private readonly IStorage _storage;
        private readonly IPeriodicAction[] _periodicActions;
        private readonly IGameObjectAccessor _gameObjectAccessor;

        public PeriodicActionSystem(IStorage storage, IPeriodicAction[] periodicActions, IGameObjectAccessor gameObjectAccessor)
        {
            _storage = storage;
            _periodicActions = periodicActions;
            _gameObjectAccessor = gameObjectAccessor;
        }

        public void Process(double gameTime)
        {
            var actions = _storage.Find<PeriodicAction>(a => true);

            var actionToTrigger = actions.Where(a => NeedTrigger(a, gameTime)).ToArray();

            foreach (var action in actionToTrigger)
            {
                if (action.LastTriggerTimeMs != 0) // если 0 - значит только что созданный, необходимо пропустить
                {
                    var instance = _periodicActions.FirstOrDefault(a => a.GetType().FullName == action.ActionType);
                    if (instance == null)
                        throw new Exception($"periodic action {action.ActionType} was not found");


                    var gameObject = _gameObjectAccessor.Get(action.GameObjectId);
                    instance.Trigger(gameObject);

                    throw new Exception("WORK!");
                }

                action.LastTriggerTimeMs = gameTime;
                _storage.Update(action);
            }
        }


        private bool NeedTrigger(PeriodicAction action, double gameTime) => gameTime - action.LastTriggerTimeMs > action.PeriodMs;
    }
}