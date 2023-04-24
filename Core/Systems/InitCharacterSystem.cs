using Godot;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Ui;
using System;
using System.Linq;

namespace My_awesome_character.Core.Systems
{
    internal class InitCharacterSystem : ISystem
    {
        private readonly ISceneAccessor _sceneAccessor;

        public InitCharacterSystem(ISceneAccessor sceneAccessor)
        {
            _sceneAccessor = sceneAccessor;
        }

        public void Process(double gameTime)
        {
            var character = _sceneAccessor.FindFirst<character>(SceneNames.Character);
            if (character != null)
                return;

            var game = _sceneAccessor.GetScene<Node2D>(SceneNames.Game);
            var map = _sceneAccessor.FindFirst<Map>(SceneNames.Map);
            var randomPoint = map.GetCells.OrderBy(g => Guid.NewGuid()).First();

            character = SceneFactory.Create<character>(SceneNames.Character, ScenePaths.Character);
            game.AddChild(character, forceReadableName: true);
            character.ZIndex = 100;
            character.MapPosition = randomPoint;
            character.GlobalPosition = map.GetGlobalPositionOf(randomPoint);
        }
    }
}
