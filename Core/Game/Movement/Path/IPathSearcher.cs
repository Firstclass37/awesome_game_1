namespace My_awesome_character.Core.Game.Movement.Path
{
    internal interface IPathSearcher
    {
        T[] Search<T>(T start, T end, PathSearcherSetting<T> setting);
    }
}