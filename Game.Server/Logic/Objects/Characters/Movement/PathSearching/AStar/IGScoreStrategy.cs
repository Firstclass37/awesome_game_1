namespace Game.Server.Logic.Objects.Characters.Movement.PathSearching.AStar
{
    internal interface IGScoreStrategy<T>
    {
        double Get(T start, T end);
    }
}