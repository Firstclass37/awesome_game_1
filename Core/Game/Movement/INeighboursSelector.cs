namespace My_awesome_character.Core.Game.Movement
{
    internal interface INeighboursSelector
    {
        MapCell[] GetNeighboursOf(MapCell mapCell);
    }
}