using Game.Server.Models;

namespace Game.Server.Storage.Extentions
{
    internal static class StorageExtentions
    {
        public static void Upsert<T>(this IStorage storage, T value) where T: IEntityObject
        {
            if (storage.Exists<T>(value.Id))
                storage.Update(value);
            else
                storage.Add(value);
        }
    }
}