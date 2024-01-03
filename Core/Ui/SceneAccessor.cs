using Godot;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace My_awesome_character.Core.Ui
{
    internal class SceneAccessor : ISceneAccessor
    {
        private readonly ConcurrentDictionary<string, object> _nodesCache = new();

        public static Node Root { get; set; }

        public T GetNode<T>(string name) where T : class =>
            Root.GetNode(name) as T;

        public T GetScene<T>(string name) where T : class 
        {
            if (Root.Name == name)
                return Root as T;

            return Root.FindChild(name, true, false) as T;
        }

        public T FindFirst<T>(string name, bool isStatic = false) where T : class 
        {
            if (_nodesCache.ContainsKey(name))
                return _nodesCache[name] as T;

            var found = Find(Root, name) as T;
            if (isStatic)
                _nodesCache.TryAdd(name, found);

            return found;
        }

        private Node Find(Node current, string name)
        {
            if (current.Name == name)
                return current;

            return current.GetChildren()
                .Select(c => Find(c, name))
                .FirstOrDefault(c => c != null);
        }

        public IEnumerable<T> FindAll<T>(Predicate<T> predicate) where T : class
        {
            return Find(Root, n => n is T t && predicate(t)).Cast<T>();
        }

        public IEnumerable<T> FindAll<T>() where T : class
        {
            return Find(Root, n => n is T).Cast<T>();
        }

        private IEnumerable<Node> Find(Node current, Predicate<Node> predicate)
        {
            if (predicate(current))
                yield return current;


            foreach (var child in current.GetChildren().SelectMany(c => Find(c, predicate)))
                yield return child;

            yield break;
        }
    }
}