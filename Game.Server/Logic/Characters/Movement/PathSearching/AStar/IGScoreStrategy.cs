namespace Game.Server.Logic.Characters.Movement.PathSearching.Base
{
    internal interface IGScoreStrategy<T>
    {
        double Get(T start, T end);
    }
}