using Godot;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game.Events;
using My_awesome_character.Core.Infrastructure.Events;
using My_awesome_character.Core.Ui;
using System;
using System.Linq;

namespace My_awesome_character.Core.Systems.Character
{
    internal class InitCharacterSystem : ISystem
    {
        private readonly ISceneAccessor _sceneAccessor;
        private readonly IEventAggregator _eventAggregator;

        private const int _characterCount = 1;

        public InitCharacterSystem(ISceneAccessor sceneAccessor, IEventAggregator eventAggregator)
        {
            _sceneAccessor = sceneAccessor;
            _eventAggregator = eventAggregator;
        }

        public void OnStart()
        {
            GD.Print("InitCharacterSystem");

            var map = _sceneAccessor.FindFirst<Map>(SceneNames.Map);

            for (int i = 0; i < _characterCount; i++)
            {
                var id = i + 1;
                if (_sceneAccessor.FindFirst<character>(SceneNames.Character(id)) != null)
                    continue;

                var randomPoint = map.GetCells().OrderBy(g => Guid.NewGuid()).First();
                var character = SceneFactory.Create<character>(SceneNames.Character(id), ScenePaths.Character);
                map.AddChild(character, forceReadableName: true);
                character.Id = id;
                character.ZIndex = i + 10;
                character.MapPosition = randomPoint;
                character.GlobalPosition = map.GetLocalPosition(randomPoint);
                character.Scale = new Vector2(0.8f, 0.8f);

                _eventAggregator.GetEvent<GameEvent<CharacterCreatedEvent>>().Publish(new CharacterCreatedEvent { CharacterId = id });
            }
        }

        public void Process(double gameTime)
        {
        }
    }
}