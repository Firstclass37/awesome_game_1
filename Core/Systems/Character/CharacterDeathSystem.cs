using Game.Server.Events.Core;
using Game.Server.Events.List.Character;
using Godot;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Ui;

namespace My_awesome_character.Core.Systems.Character
{
    internal class CharacterDeathSystem : ISystem
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ISceneAccessor _sceneAccessor;

        public CharacterDeathSystem(IEventAggregator eventAggregator, ISceneAccessor sceneAccessor)
        {
            _eventAggregator = eventAggregator;
            _sceneAccessor = sceneAccessor;
        }

        public void OnStart()
        {
            _eventAggregator.GetEvent<GameEvent<CharacterDeathEvent>>().Subscribe(OnDamageTake);
        }

        private void OnDamageTake(CharacterDeathEvent obj)
        {
            var character = _sceneAccessor.GetScene<character>(SceneNames.Character(obj.CharacterId));
            var map = _sceneAccessor.FindFirst<Map>(SceneNames.Map);

            map.RemoveCharacter(character);
        }

        public void Process(double gameTime)
        {
        }
    }
}