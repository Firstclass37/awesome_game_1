namespace My_awesome_character.Core.Game.Movement
{
    internal interface IPathBuilder
    {
        MapCell[] FindPath(MapCell start, MapCell end, INeighboursSelector neighboursSelector);
    }
}