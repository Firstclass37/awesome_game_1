using Godot;
using My_awesome_character.Core.Ioc;
using My_awesome_character.Core.Systems;
using My_awesome_character.Core.Ui;
using System.Linq;

namespace My_awesome_character.Core.System
{
    internal partial class SystemNode: Node
    {
        private double _timeElapsed = 0;

        private ISystem[] _systems = Application.GetAll<ISystem>();

        public override void _EnterTree()
        {
            SceneAccessor.Root = GetParent();
        }

        public override void _Ready()
        {
            GD.Print($"_Ready SYSTEMS COUNT: {_systems?.Count()}");

            foreach (var system in _systems)
                system.OnStart();
        }

        public override void _Process(double delta)
        {
            _timeElapsed += delta;
            foreach (var system in _systems)
                system.Process(_timeElapsed);
        }
    }
}