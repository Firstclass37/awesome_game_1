namespace My_awesome_character.Core.Game.Movement
{
    internal class AllNeighboursSelector : INeighboursSelector
    {
        private readonly INeighboursAccessor _neighboursAccessor;

        public AllNeighboursSelector(INeighboursAccessor neighboursAccessor)
        {
            _neighboursAccessor = neighboursAccessor;
        }

        public MapCell[] GetNeighboursOf(MapCell mapCell)
        {
            return _neighboursAccessor.GetNeighboursOf(mapCell);
        }
    }
}