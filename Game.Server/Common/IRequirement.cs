namespace Game.Server.Common
{
    internal interface IRequirement<T>
    {
        bool Satisfy(T item);
    }
}