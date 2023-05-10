using Godot;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game.Events;
using My_awesome_character.Core.Infrastructure.Events;
using My_awesome_character.Core.Ui;
using System.Linq;

namespace My_awesome_character.Core.Systems
{
    internal class CharacterCreationSystem : ISystem
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ISceneAccessor _sceneAccessor;

        public CharacterCreationSystem(IEventAggregator eventAggregator, ISceneAccessor sceneAccessor)
        {
            _eventAggregator = eventAggregator;
            _sceneAccessor = sceneAccessor;
        }

        public void OnStart()
        {
            _eventAggregator.GetEvent<GameEvent<CharacterCreationRequestEvent>>().Subscribe(OnCreaterionRequest);
        }

        private void OnCreaterionRequest(CharacterCreationRequestEvent obj)
        {
            var game = _sceneAccessor.GetScene<Node2D>(SceneNames.Game);
            var map = _sceneAccessor.FindFirst<Map>(SceneNames.Map);

            var otherCharacters = _sceneAccessor.FindAll<character>();
            var newCharacterId = otherCharacters.Any() ? otherCharacters.Max(h => h.Id) + 1 : 1;

            var character = SceneFactory.Create<character>(SceneNames.Character(newCharacterId), ScenePaths.Character);
            game.AddChild(character, forceReadableName: true);
            character.Id = newCharacterId;
            character.ZIndex = newCharacterId + 10;
            character.MapPosition = obj.InitPosition;
            character.GlobalPosition = map.GetGlobalPositionOf(obj.InitPosition);
            character.Scale = new Vector2(0.8f, 0.8f);

            Godot.GD.Print($"character create {character.Id}");
        }

        public void Process(double gameTime)
        {
        }
    }
}