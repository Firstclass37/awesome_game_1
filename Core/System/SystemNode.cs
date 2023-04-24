using Godot;
using My_awesome_character.Core.Ioc;
using My_awesome_character.Core.Systems;
using My_awesome_character.Core.Ui;
using System;

namespace My_awesome_character.Core.System
{
    internal partial class SystemNode: Node
    {
        private DateTime _startDate;

        public override void _EnterTree()
        {
            _startDate = DateTime.Now;
            SceneAccessor.Root = GetParent();
        }

        public override void _Process(double delta)
        {
            var gameTime = (DateTime.Now - _startDate).TotalMilliseconds;
            foreach (var system in Application.GetAll<ISystem>())
                system.Process(gameTime);
        }
    }
}