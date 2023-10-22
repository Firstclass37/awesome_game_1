using Game.Server.Models;
using Game.Server.Models.GameObjects;

namespace Game.Server.Storage.Extentions
{
    internal static class StorageExtentions
    {
        public static void Upsert<T>(this IStorage storage, T value) where T: IEntityObject
        {
            if (storage.Exists<GameObjectToAttribute>(value.Id))
                storage.Update(value);
            else
                storage.Add(value);
        }
    }
}