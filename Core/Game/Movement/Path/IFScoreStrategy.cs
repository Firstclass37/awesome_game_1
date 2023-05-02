namespace My_awesome_character.Core.Game.Movement.Path
{
    internal interface IFScoreStrategy<T>
    {
        double Get(T start, T end, double gScore);
    }
}