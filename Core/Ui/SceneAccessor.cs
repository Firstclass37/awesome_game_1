using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace My_awesome_character.Core.Ui
{
    internal class SceneAccessor : ISceneAccessor
    {
        public static Node Root { get; set; }

        public T GetScene<T>(string name) where T : class =>
            Root.FindChild(name, true, false) as T;

        public T FindFirst<T>(string name) where T : class => 
            Find(Root, name) as T;

        private Node Find(Node current, string name)
        {
            if (current.Name == name)
                return current;

            return current.GetChildren()
                .Select(c => Find(c, name))
                .FirstOrDefault(c => c != null);
        }

        public IEnumerable<T> FindAll<T>(Predicate<Node> predicate) where T : class
        {
            return Find(Root, predicate).Cast<T>();
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