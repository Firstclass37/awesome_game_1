using Godot;
using My_awesome_character.Core.Ioc;
using My_awesome_character.Core.Systems;
using My_awesome_character.Core.Ui;
using System;

namespace My_awesome_character.Core.System
{
    internal partial class SystemNode: Node
    {
        public override void _EnterTree()
        {
            SceneAccessor.Root = GetParent();
        }

        public override void _Process(double delta)
        {
            foreach (var system in Application.GetAll<ISystem>())
                system.Process(0);
        }
    }
}