using Godot;

namespace My_awesome_character.Core.Ui.Extentions
{
    internal static class NodeExtentions
    {
        public static T GetNamedNode<T>(this Node node, string name) where T : class 
        {
            return node.GetNode(name) as T;
        }

        public static T GetNamedNodeOrNull<T>(this Node node, string name) where T : class
        {
            return node.FindChild(name, false) as T;
        }
    }
}