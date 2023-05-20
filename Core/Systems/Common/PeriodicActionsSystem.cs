using My_awesome_character.Core.Systems._Core;
using My_awesome_character.Core.Ui;

namespace My_awesome_character.Core.Systems.Common
{
    internal class PeriodicActionsSystem : ISystem
    {
        private readonly ISceneAccessor _sceneAccessor;

        public PeriodicActionsSystem(ISceneAccessor sceneAccessor)
        {
            _sceneAccessor = sceneAccessor;
        }

        public void OnStart()
        {
        }

        public void Process(double gameTime)
        {
            var owners = _sceneAccessor.FindAll<IPeriodicActionOwner>();
            foreach (var owner in owners)
            {
                var action = owner.PeriodicAction;
                if (action == null)
                    continue;

                var needFire = gameTime - action.LastTriggerTimeSeconds > action.PeriodSeconds;
                if (needFire)
                {
                    action.LastTriggerTimeSeconds = gameTime;
                    action.Trigger();
                }
            }
        }
    }
}