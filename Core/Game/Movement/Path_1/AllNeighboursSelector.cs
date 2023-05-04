using My_awesome_character.Core.Game.Movement.Path;

namespace My_awesome_character.Core.Game.Movement.Path_1
{
    internal class AllNeighboursSelector : INieighborsSearchStrategy<MapCell>
    {
        private readonly INeighboursAccessor _neighboursAccessor;

        public AllNeighboursSelector(INeighboursAccessor neighboursAccessor)
        {
            _neighboursAccessor = neighboursAccessor;
        }

        public MapCell[] Search(MapCell element)
        {
            return _neighboursAccessor.GetNeighboursOf(element);
        }
    }
}