namespace Game.Server.Logic.Characters.Movement.PathSearching.Base
{
    internal interface IHScoreStrategy<T>
    {
        double Get(T start, T end);
    }
}