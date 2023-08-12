using Game.Server.API.GameInit;
using Godot;
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
        private IGameController _gameController = Application.Get<IGameController>();


        public override void _EnterTree()
        {
            SceneAccessor.Root = GetParent().GetNode("Game");
        }

        public override void _Ready()
        {
            foreach (var system in _systems)
                system.OnStart();

            _gameController.StartNewGame();
        }

        public override void _Process(double delta)
        {
            GameTime += delta;
            foreach (var system in _systems)
            {
                system.Process(GameTime);
                _gameController.Tick(GameTime);
            }
        }
    }
}