namespace Game.Server.Logic.Objects.Characters.Movement.PathSearching.AStar
{
    internal interface IPathSearcher
    {
        T[] Search<T>(T start, T end, PathSearcherSetting<T> setting);
    }
}