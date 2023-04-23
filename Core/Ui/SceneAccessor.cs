using Godot;
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
    }
}