using System;
using System.Collections.Generic;

namespace My_awesome_character.Core.Game
{
    internal interface IStorage
    {
        void Add<T>(T obj);
        bool Exists<T>(Func<T, bool> predicate);
        IEnumerable<T> Find<T>(Func<T, bool> predicate);
        T FindFirstOrDefault<T>(Func<T, bool> predicate);
        void Remove<T>(T obj);
        void Update<T>(T obj);
    }
}