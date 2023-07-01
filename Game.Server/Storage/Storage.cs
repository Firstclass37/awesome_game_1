using Game.Server.Models;
using System.Collections.Concurrent;

namespace Game.Server.Storage
{
    internal class Storage : IStorage
    {
        private static readonly ConcurrentDictionary<string, ConcurrentDictionary<Guid, object>> _dataBase = new();

        public void Add<T>(T obj) where T : IEntityObject
        {
            var key = GetKey<T>();
            _dataBase[key].TryAdd(obj.Id, obj);
        }

        public T Get<T>(Guid id) where T : IEntityObject
        {
            var key = GetKey<T>();
            return _dataBase[key].ContainsKey(id) ? (T)_dataBase[key][id] : default;
        }

        public void Update<T>(T obj) where T : IEntityObject
        {
            var key = GetKey<T>();
            if (_dataBase[key].Remove(obj.Id, out var _))
                _dataBase[key].TryAdd(obj.Id, obj);
        }

        public bool Exists<T>(Func<T, bool> predicate) where T : IEntityObject
        {
            var key = GetKey<T>();
            return _dataBase[key].Keys.Cast<T>().Any(predicate);
        }

        public IEnumerable<T> Find<T>(Func<T, bool> predicate) where T : IEntityObject
        {
            var key = GetKey<T>();
            return _dataBase[key].Keys.Cast<T>().Where(predicate);
        }

        public void Remove<T>(T obj) where T : IEntityObject
        {
            var key = GetKey<T>();
            _dataBase[key].Remove(obj.Id, out var _);
        }

        private string GetKey<T>() where T : IEntityObject
        {
            var key = typeof(T).Name;
            if (!_dataBase.ContainsKey(key))
                _dataBase.TryAdd(key, new());

            return key;
        }
    }
}