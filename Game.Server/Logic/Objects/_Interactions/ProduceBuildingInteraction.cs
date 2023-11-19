using Game.Server.DataAccess;
using Game.Server.Logic.Objects.Characters;
using Game.Server.Models.Constants.Attributes;
using Game.Server.Models.GameObjects;
using Game.Server.Models.GamesObjectList;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects._Interactions
{
    internal class ProduceBuildingInteraction: CharacterInteraction
    {
        private readonly ICharacterDamageService _characterDamageService;
        private readonly IGameObjectAgregatorRepository _gameObjectAgregatorRepository;

        public ProduceBuildingInteraction(ICharacterDamageService characterDamageService, IGameObjectAgregatorRepository gameObjectAgregatorRepository)
        {
            _characterDamageService = characterDamageService;
            _gameObjectAgregatorRepository = gameObjectAgregatorRepository;
        }

        protected override void OnInteract(GameObjectAggregator gameObject, Character character, Coordiante interactionPoint)
        {
            _characterDamageService.InstantKill(character);
            gameObject.ModifyAttribute(ManufactureAttributes.ProduceQueueSize, q => q + 1);

            _gameObjectAgregatorRepository.Update(gameObject);
        }
    }
}