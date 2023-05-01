using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace My_awesome_character.Core.Game
{
    internal class Storage : IStorage
    {
        private readonly ConcurrentDictionary<string, ConcurrentDictionary<object, bool>> _dataBase = new ConcurrentDictionary<string, ConcurrentDictionary<object, bool>>();

        public void Add<T>(T obj)
        {
            var key = typeof(T).Name;
            if (!_dataBase.ContainsKey(key))
                _dataBase.TryAdd(key, new ConcurrentDictionary<object, bool>());

            _dataBase[key].TryAdd(obj, true);
        }

        public void Update<T>(T obj)
        {
            var key = typeof(T).Name;
            if (!_dataBase.ContainsKey(key))
                return;

            _dataBase[key].TryRemove(new KeyValuePair<object, bool>(obj, true));
            _dataBase[key].TryAdd(obj, true);
        }

        public T FindFirstOrDefault<T>(Func<T, bool> predicate)
        {
            var key = typeof(T).Name;
            if (!_dataBase.ContainsKey(key))
                return default(T);

            return _dataBase[key].Keys.Cast<T>().Where(predicate).FirstOrDefault();
        }

        public bool Exists<T>(Func<T, bool> predicate)
        {
            var key = typeof(T).Name;
            if (!_dataBase.ContainsKey(key))
                return false;

            return _dataBase[key].Keys.Cast<T>().Any(predicate);
        }

        public IEnumerable<T> Find<T>(Func<T, bool> predicate)
        {
            var key = typeof(T).Name;
            if (!_dataBase.ContainsKey(key))
                return Enumerable.Empty<T>();

            return _dataBase[key].Keys.Cast<T>().Where(predicate);
        }

        public void Remove<T>(T obj)
        {
            var key = typeof(T).Name;
            if (!_dataBase.ContainsKey(key))
                return;

            _dataBase[key].TryRemove(new KeyValuePair<object, bool>(obj, true));
        }
    }
}