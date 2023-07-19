using Game.Server.Logger;
using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects._PeriodicAction;
using Game.Server.Models;
using Game.Server.Storage;
using System;

namespace Game.Server.Logic.Systems
{
    internal class PeriodicActionSystem : ISystem
    {
        private readonly IStorage _storage;
        private readonly IPeriodicAction[] _periodicActions;
        private readonly IGameObjectAccessor _gameObjectAccessor;
        private readonly ILogger _logger;

        public PeriodicActionSystem(IStorage storage, IPeriodicAction[] periodicActions, IGameObjectAccessor gameObjectAccessor, ILogger logger)
        {
            _storage = storage;
            _periodicActions = periodicActions;
            _gameObjectAccessor = gameObjectAccessor;
            _logger = logger;
        }

        public void Process(double gameTime)
        {
            var actions = _storage.Find<PeriodicAction>(a => true);
            var actionToTrigger = actions.Where(a => NeedTrigger(a, gameTime)).ToArray();

            foreach (var action in actionToTrigger)
            {
                if (action.LastTriggerTimeSeconds != 0) // если 0 - значит только что созданный, необходимо пропустить
                {
                    var instance = _periodicActions.FirstOrDefault(a => a.GetType().FullName == action.ActionType);
                    if (instance == null)
                        throw new Exception($"periodic action {action.ActionType} was not found");


                    var gameObject = _gameObjectAccessor.Get(action.GameObjectId);
                    instance.Trigger(gameObject);
                }

                action.LastTriggerTimeSeconds = gameTime;
                _storage.Update(action);
            }
        }


        private bool NeedTrigger(PeriodicAction action, double gameTime) => gameTime - action.LastTriggerTimeSeconds > action.PeriodSeconds;
    }
}