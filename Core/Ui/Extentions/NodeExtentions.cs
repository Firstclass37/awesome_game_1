using Godot;
using System.Linq;

namespace My_awesome_character.Core.Ui.Extentions
{
    internal static class NodeExtentions
    {
        public static T GetNamedNode<T>(this Node node, string name) where T : class 
        {
            return node.GetNode(name) as T;
        }
    }
}