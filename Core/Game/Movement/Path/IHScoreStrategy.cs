namespace My_awesome_character.Core.Game.Movement.Path
{
    internal interface IHScoreStrategy<T>
    {
        double Get(T start, T end);
    }
}