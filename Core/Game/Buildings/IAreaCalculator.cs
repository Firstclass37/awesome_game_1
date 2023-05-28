namespace My_awesome_character.Core.Game.Buildings
{
    internal interface IAreaCalculator
    {
        MapCell[] Get2x2Area(MapCell root);
    }
}