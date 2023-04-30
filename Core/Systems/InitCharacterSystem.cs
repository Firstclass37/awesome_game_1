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

        private const int _characterCount = 10;

        public InitCharacterSystem(ISceneAccessor sceneAccessor)
        {
            _sceneAccessor = sceneAccessor;
        }

        public void Process(double gameTime)
        {
            var characters = _sceneAccessor.FindAll<character>();
            if (characters.Any())
                return;

            var game = _sceneAccessor.GetScene<Node2D>(SceneNames.Game);
            var map = _sceneAccessor.FindFirst<Map>(SceneNames.Map);
            
            for (int i = 0; i < _characterCount; i++)
            {
                var randomPoint = map.GetCells().OrderBy(g => Guid.NewGuid()).First();
                var character = SceneFactory.Create<character>(SceneNames.Character(i + 1), ScenePaths.Character);
                game.AddChild(character, forceReadableName: true);
                character.ZIndex = 100;
                character.MapPosition = randomPoint;
                character.GlobalPosition = map.GetGlobalPositionOf(randomPoint);
                character.Scale = new Vector2(0.8f, 0.8f);
            }
        }
    }
}