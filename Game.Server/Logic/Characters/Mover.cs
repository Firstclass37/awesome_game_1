using Game.Server.Events.Core;
using Game.Server.Logic.Characters.Movement.PathSearching;
using Game.Server.Logic.Characters.Movement.PathSearching.Base;
using Game.Server.Models.Maps;
using Game.Server.Models.Temp;

namespace Game.Server.Logic.Characters
{
    internal class Mover : IMover
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IPathSearcher _pathSearcher;
        private readonly IPathSearcherSettingsFactory _pathSearcherSettingsFactory;
        private readonly INeighboursAccessor _neighboursAccessor;

        public Mover(IEventAggregator eventAggregator, IPathSearcher pathSearcher, IPathSearcherSettingsFactory pathSearcherSettingsFactory, INeighboursAccessor neighboursAccessor)
        {
            _eventAggregator = eventAggregator;
            _pathSearcher = pathSearcher;
            _pathSearcherSettingsFactory = pathSearcherSettingsFactory;
            _neighboursAccessor = neighboursAccessor;
        }

        public void MoveTo(GameObjectAggregator gameObject, Coordiante coordiante)
        {
            var root = gameObject.Area.First(p => p.IsRoot).Coordiante;
            var path = _pathSearcher.Search(root, coordiante, _pathSearcherSettingsFactory.Create(SelectSelector(root, coordiante)));
            if (!path.Any())
                throw new ArgumentException();
        }

        public void StopMoving(GameObjectAggregator gameObject)
        {

        }

        private INieighborsSearchStrategy<Coordiante> SelectSelector(Coordiante currentPosition, Coordiante targetPosition)
        {
            return new OnlyRoadNeighboursSelector(_neighboursAccessor);

            //if (IsRoad(currentPosition) && IsRoad(targetPosition))
            //    return new OnlyRoadNeighboursSelector(map);
            //else
            //    return new AllNeighboursSelector(map);
        }
    }
}