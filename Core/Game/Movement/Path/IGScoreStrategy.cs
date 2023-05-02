namespace My_awesome_character.Core.Game.Movement.Path
{
    internal interface IGScoreStrategy<T>
    {
        double Get(T start,T end);
    }
}