namespace My_awesome_character.Core.Game
{
    internal interface IPathBuilder
    {
        MapCell[] FindPath(MapCell start, MapCell end, INeighboursAccessor neighboursAccessor);
    }
}