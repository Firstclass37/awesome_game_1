using Game.Server.Events.Core;
using Game.Server.Events.Core.Extentions;
using Game.Server.Events.List.Game;
using Game.Server.Logic.Systems;
using Game.Server.Storage;

namespace Game.Server.Logic.Objects.Weather.Sun
{
    internal class WatchSystem : ISystem
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IStorage _storage;

        private readonly int _k = 10;

        public WatchSystem(IEventAggregator eventAggregator, IStorage storage)
        {
            _eventAggregator = eventAggregator;
            _storage = storage;
        }

        public void Process(double gameTimeSeconds)
        {
            var game = _storage.Find<Models.Game>(g => true).First();
            if (game.GameTimeSeconds == 0.0)
            {
                game.GameTimeSeconds = gameTimeSeconds;

                _storage.Update(game);
                _eventAggregator.PublishGameEvent(new TimeChangedEvent { Hours = game.InnerGameHour });
                return;
            }

            var roundedTime = (int)gameTimeSeconds;
            var timePassed = (int)(gameTimeSeconds - game.GameTimeSeconds);

            if (timePassed / _k > 0)
            {
                var newHours = game.InnerGameHour + 1;

                game.GameTimeSeconds = gameTimeSeconds;
                game.InnerGameHour = newHours == 25 ? 0 : newHours;
                _storage.Update(game);
                _eventAggregator.PublishGameEvent(new TimeChangedEvent { Hours = game.InnerGameHour });
            }
        }
    }
}