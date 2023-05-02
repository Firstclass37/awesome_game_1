namespace My_awesome_character.Core.Game.Movement.Path
{
    internal interface INieighborsSearchStrategy<T>
    {
        T[] Search(T element);
    }
}