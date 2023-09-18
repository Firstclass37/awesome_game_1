namespace Game.Server.Common.Extentions
{
    internal static class IEnumerableExtentions
    {
        public static IEnumerable<T> Mix<T>(this IEnumerable<T> collection) => collection.OrderBy(e => Guid.NewGuid());
    }
}