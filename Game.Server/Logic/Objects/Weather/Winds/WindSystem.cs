using Game.Server.Common;
using Game.Server.Common.Extentions;
using Game.Server.Events.Core;
using Game.Server.Events.Core.Extentions;
using Game.Server.Events.List.Weather;
using Game.Server.Logic.Maps;
using Game.Server.Logic.Systems;
using Game.Server.Models.Constants;
using Game.Server.Models.Maps;
using Game.Server.Models.Weather;
using Game.Server.Storage;

namespace Game.Server.Logic.Objects.Weather.Winds
{
    internal class WindSystem : ISystem
    {
        private readonly IStorage _storage;
        private readonly IEventAggregator _eventAggregator;
        private readonly IMapGrid _mapGrid;
        private readonly IAreaCalculator _areaCalculator;
        private readonly IMatrixGrid _matrixGrid;

        private readonly double _windDurationSeconds = 60;

        public WindSystem(IStorage storage, IEventAggregator eventAggregator, IMapGrid mapGrid,
            IAreaCalculator areaCalculator, IMatrixGrid matrixGrid)
        {
            _storage = storage;
            _eventAggregator = eventAggregator;
            _mapGrid = mapGrid;
            _areaCalculator = areaCalculator;
            _matrixGrid = matrixGrid;
        }

        public void Process(double gameTimeSeconds)
        {
            var wind = _storage.Find<Wind>(w => true).FirstOrDefault();
            if (wind == null)
            {
                StartNew(gameTimeSeconds);
            }
            else if (wind.EndAt < gameTimeSeconds)
            {
                End(wind);
            }
        }

        private void StartNew(double startDate)
        {
            if (GameDiceGroup.CreateCube(2).Roll() > 6)
            {
                var areaSize = GenerateAreaSize();

                var wind = new Wind
                {
                    Area = FindArea(areaSize),
                    Direction = GenerateWindDirection(),
                    StartedAt = startDate,
                    EndAt = startDate + _windDurationSeconds
                };
                _storage.Add(wind);
                _eventAggregator.PublishGameEvent(new WindStartEvent { Area = wind.Area, Direction = wind.Direction, Id = wind.Id });
            }
        }

        private Coordiante[] FindArea(AreaSize areaSize)
        {
            foreach (var point in _mapGrid.GetGrid().Mix())
            {
                try
                {
                    return _areaCalculator.GetArea(point, areaSize);
                }
                catch
                {

                }
            }
            throw new ArgumentException($"can not find area {areaSize}");
        }

        private AreaSize GenerateAreaSize()
        {
            var diceGroup = GameDiceGroup.CreateCube(3);
            var width = diceGroup.Roll();
            var height = diceGroup.Roll();

            return new AreaSize(Math.Min(width, _matrixGrid.Width), Math.Min(height, _matrixGrid.Height));
        }

        //TODO: autofac fail on this code, No constructors on type '<PrivateImplementationDetails>' can be found with the constructor finder 'Autofac.Core.Activators.Reflection.DefaultConstructorFinder'.
        //private readonly Direction[] _directions = new Direction[]
        //{
        //    Direction.Top,
        //    Direction.Bottom,
        //    Direction.Left,
        //    Direction.Right
        //};

        private Direction GenerateWindDirection()
        {
            return Direction.Top;
            //return _directions
            //    .Mix()
            //    .First();
        }

        private void End(Wind wind)
        {
            _storage.Remove(wind);
            _eventAggregator.PublishGameEvent(new WindEndEvent { Id = wind.Id });
        }
    }
}