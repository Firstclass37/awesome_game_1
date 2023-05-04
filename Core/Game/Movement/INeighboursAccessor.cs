namespace My_awesome_character.Core.Game.Movement
{
    internal interface INeighboursAccessor
    {
        MapCell[] GetNeighboursOf(MapCell mapCell);
    }
}