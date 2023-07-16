using Game.Server.DataAccess;
using Game.Server.Models.GamesObjectList;

namespace Game.Server.Logic.Objects.Characters
{
    internal class CharacterDamageService : ICharacterDamageService
    {
        private readonly IGameObjectAgregatorRepository _gameObjectAgregatorRepository;

        public CharacterDamageService(IGameObjectAgregatorRepository gameObjectAgregatorRepository)
        {
            _gameObjectAgregatorRepository = gameObjectAgregatorRepository;
        }

        public void InstantKill(Character character)
        {
            _gameObjectAgregatorRepository.Remove(character.GameObject);
        }

        public void Damage(Character character, double damage)
        {
            throw new NotImplementedException();
        }
    }
}