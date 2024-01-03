using Game.Server.Events.Core;
using Game.Server.Events.Core.Extentions;
using Game.Server.Events.List.Game;
using Game.Server.Logic.Systems;
using Game.Server.Models.Constants;
using Game.Server.Models.Weather;
using Game.Server.Storage;

namespace Game.Server.Logic.Objects.Weather.Sun
{
    internal class WatchSystem : ISystem
    {
        private readonly IStorage _storage;
        private readonly IEventAggregator _eventAggregator;

        private readonly int _secondsAmount = 10;

        public WatchSystem(IEventAggregator eventAggregator, IStorage storage)
        {
            _eventAggregator = eventAggregator;
            _storage = storage;
        }

        public void Process(double gameTimeSeconds)
        {
            var game = _storage.First<Models.Game>();
            var gameTime = _storage.First<GameTime>();
            if (game.GameTimeSeconds == 0.0)
            {
                game.GameTimeSeconds = gameTimeSeconds;

                _storage.Update(game);
                _eventAggregator.PublishGameEvent(new TimeChangedEvent { Hours = gameTime.Hours });
                return;
            }

            var timePassed = (int)(gameTimeSeconds - game.GameTimeSeconds);
            if (timePassed / _secondsAmount > 0)
            {
                var newHours = gameTime.Hours + 1;
                var actualHours = newHours == 25 ? 0 : newHours;

                var newGameTime = gameTime with { Hours = actualHours, TimeOfDay = Detect(newHours) };

                _storage.Update(game with { GameTimeSeconds = gameTimeSeconds });
                _storage.Update(newGameTime);

                _eventAggregator.PublishGameEvent(new TimeChangedEvent { Hours = newGameTime.Hours, TimeOfDay = newGameTime.TimeOfDay });
            }
        }

        private TimeOfDay Detect(int hour)
        {
            if (hour >= 0 && hour < 7 || hour >= 22)
                return TimeOfDay.Night;

            if (hour >= 7 && hour <= 9)
                return TimeOfDay.Dawn;

            if (hour >= 19 && hour < 22)
                return TimeOfDay.Sunset;

            return TimeOfDay.Day;
        }
    }
}