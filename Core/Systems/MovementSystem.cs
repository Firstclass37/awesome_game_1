using Godot;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game;
using My_awesome_character.Core.Ui;
using System;
using System.Linq;

namespace My_awesome_character.Core.Systems
{
    internal class MovementSystem: ISystem
    {
        private readonly IPathBuilder _pathBuilder;
        private readonly ISceneAccessor _sceneAccessor;

        private static double _lastTime;

        public MovementSystem(IPathBuilder pathBuilder, ISceneAccessor sceneAccessor)
        {
            _pathBuilder = pathBuilder;
            _sceneAccessor = sceneAccessor;
        }

        public void Process(double gameTime)
        {
            if (_lastTime != 0D && gameTime - _lastTime < 500)
                return;

            _lastTime = gameTime;

            //var map = _sceneAccessor.FindFirst<Map>(SceneNames.Map);
            //var character = _sceneAccessor.FindFirst<character>(SceneNames.Character);

            //var randomPoint = map.GetCells.OrderBy(g => Guid.NewGuid()).First();
            //character.GlobalPosition = map.GetGlobalPositionOf(randomPoint);
            //character.MapPosition = randomPoint;
        }
    }
}