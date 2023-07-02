namespace Game.Server.Logic.Objects.Characters.Movement.PathSearching.AStar
{
    internal interface IHScoreStrategy<T>
    {
        double Get(T start, T end);
    }
}