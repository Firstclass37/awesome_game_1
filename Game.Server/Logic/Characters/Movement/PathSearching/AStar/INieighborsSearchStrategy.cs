namespace Game.Server.Logic.Characters.Movement.PathSearching.Base
{
    internal interface INieighborsSearchStrategy<T>
    {
        T[] Search(T element);
    }
}