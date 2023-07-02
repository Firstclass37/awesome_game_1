using Game.Server.Logic.Objects.Characters;
using Game.Server.Logic.Objects.Characters.Movement.PathSearching.AStar;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.Characters.Movement.PathSearching
{
    internal class OnlyRoadNeighboursSelector : INieighborsSearchStrategy<Coordiante>
    {
        private readonly INeighboursAccessor _neighboursAccessor;

        public OnlyRoadNeighboursSelector(INeighboursAccessor neighboursAccessor)
        {
            _neighboursAccessor = neighboursAccessor;
        }

        public Coordiante[] Search(Coordiante element)
        {
            return _neighboursAccessor.GetNeighboursOf(element).ToArray();
            //.Where(c => c.CellType == MapCellType.Road || c.Tags.Contains(MapCellTags.Trap))
            //.ToArray();
        }
    }
}