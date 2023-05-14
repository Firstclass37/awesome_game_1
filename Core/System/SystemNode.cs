using Godot;
using My_awesome_character.Core.Game.Events;
using My_awesome_character.Core.Infrastructure.Events;
using My_awesome_character.Core.Ioc;
using My_awesome_character.Core.Systems;
using My_awesome_character.Core.Ui;
using System.Linq;

namespace My_awesome_character.Core.System
{
    internal partial class SystemNode: Node
    {
        public static double GameTime = 0;

        private ISystem[] _systems = Application.GetAll<ISystem>();
        private IEventAggregator _eventAggregator = Application.Get<IEventAggregator>();

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
            GameTime += delta;
            foreach (var system in _systems)
                system.Process(GameTime);
        }
    }
}