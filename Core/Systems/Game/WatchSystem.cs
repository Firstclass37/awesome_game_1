using Game.Server.Events.Core;
using Game.Server.Events.List.Game;
using My_awesome_character.Common.Extentions;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Ui;

namespace My_awesome_character.Core.Systems.Game
{
    internal class WatchSystem : ISystem
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ISceneAccessor _sceneAccessor;

        public WatchSystem(IEventAggregator eventAggregator, ISceneAccessor sceneAccessor)
        {
            _eventAggregator = eventAggregator;
            _sceneAccessor = sceneAccessor;
        }

        public void OnStart()
        {
            _eventAggregator.SubscribeGameEvent<TimeChangedEvent>(OnTimeChanged);
        }

        private void OnTimeChanged(TimeChangedEvent @event)
        {
            var watch = _sceneAccessor.FindFirst<Watch>(SceneNames.Watch);
            watch.Hours = @event.Hours;
        }

        public void Process(double gameTime)
        {
            
        }
    }
}