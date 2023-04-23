using Godot;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game;
using My_awesome_character.Core.Ui;

namespace My_awesome_character.Core.Systems
{
    internal class MovementSystem: ISystem
    {
        private readonly IPathBuilder _pathBuilder;
        private readonly ISceneAccessor _sceneAccessor;

        public MovementSystem(IPathBuilder pathBuilder, ISceneAccessor sceneAccessor)
        {
            _pathBuilder = pathBuilder;
            _sceneAccessor = sceneAccessor;
        }

        public void Process()
        {
            var map = _sceneAccessor.FindFirst<Map>(SceneNames.Map);
            var player = _sceneAccessor.FindFirst<character>(SceneNames.Character);
        }
    }
}