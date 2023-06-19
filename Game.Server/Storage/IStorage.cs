using Game.Server.Models;

namespace Game.Server.Storage
{
    internal interface IStorage
    {
        void Add<T>(T obj) where T: IEntityObject;

        T Get<T>(int id) where T : IEntityObject;

        bool Exists<T>(Func<T, bool> predicate) where T : IEntityObject;

        IEnumerable<T> Find<T>(Func<T, bool> predicate) where T : IEntityObject;

        void Remove<T>(T obj) where T : IEntityObject;

        void Update<T>(T obj) where T : IEntityObject;
    }
}