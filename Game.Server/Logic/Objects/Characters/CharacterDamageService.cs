using Game.Server.DataAccess;
using Game.Server.Events.Core;
using Game.Server.Events.List.Character;
using Game.Server.Models.Constants.Attributes;
using Game.Server.Models.GamesObjectList;

namespace Game.Server.Logic.Objects.Characters
{
    internal class CharacterDamageService : ICharacterDamageService
    {
        private readonly IGameObjectAgregatorRepository _gameObjectAgregatorRepository;
        private readonly IEventAggregator _eventAggregator;
        private readonly IMover _mover;

        public CharacterDamageService(IGameObjectAgregatorRepository gameObjectAgregatorRepository, IEventAggregator eventAggregator, IMover mover)
        {
            _gameObjectAgregatorRepository = gameObjectAgregatorRepository;
            _eventAggregator = eventAggregator;
            _mover = mover;
        }

        public void InstantKill(Character character)
        {
            _mover.StopMoving(character.GameObject);
            _gameObjectAgregatorRepository.Remove(character.GameObject);
            _eventAggregator.GetEvent<GameEvent<CharacterDeathEvent>>()
                .Publish(new CharacterDeathEvent { CharacterId = character.GameObject.GameObject.Id });
        }

        public void Damage(Character character, double damage)
        {
            _mover.StopMoving(character.GameObject);
            var resultHealth = character.GameObject.GetAttributeValue(HealthAttributes.Health) - damage;
            if (resultHealth <= 0)
            {
                InstantKill(character);
            }
            else
            {
                character.GameObject.SetAttributeValue(HealthAttributes.Health, resultHealth);
                _gameObjectAgregatorRepository.Update(character.GameObject);
            }
        }
    }
}