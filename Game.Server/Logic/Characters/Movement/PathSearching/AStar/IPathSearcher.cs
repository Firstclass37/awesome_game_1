namespace Game.Server.Logic.Characters.Movement.PathSearching.Base
{
    internal interface IPathSearcher
    {
        T[] Search<T>(T start, T end, PathSearcherSetting<T> setting);
    }
}