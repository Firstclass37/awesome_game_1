using Godot;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game.Events.Character;
using My_awesome_character.Core.Infrastructure.Events;
using My_awesome_character.Core.Ui;

namespace My_awesome_character.Core.Systems.Character
{
    internal class CharacterDamageSystem : ISystem
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ISceneAccessor _sceneAccessor;

        public CharacterDamageSystem(IEventAggregator eventAggregator, ISceneAccessor sceneAccessor)
        {
            _eventAggregator = eventAggregator;
            _sceneAccessor = sceneAccessor;
        }

        public void OnStart()
        {
            _eventAggregator.GetEvent<GameEvent<TakeDamageCharacterEvent>>().Subscribe(OnDamageTake);
        }

        private void OnDamageTake(TakeDamageCharacterEvent obj)
        {
            var character = _sceneAccessor.GetScene<character>(SceneNames.Character(obj.CharacterId));
            var game = _sceneAccessor.GetScene<Node2D>(SceneNames.Game);

            game.RemoveChild(character);
        }

        public void Process(double gameTime)
        {
        }
    }
}