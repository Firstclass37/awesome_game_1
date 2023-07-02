namespace Game.Server.Logic.Objects.Characters.Movement.PathSearching.AStar
{
    internal interface INieighborsSearchStrategy<T>
    {
        T[] Search(T element);
    }
}