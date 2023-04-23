namespace My_awesome_character.Core.Game
{
    internal interface IPathBuilder
    {
        Coordiante[] FindPath(int[,] map, Coordiante start, Coordiante end);
    }
}