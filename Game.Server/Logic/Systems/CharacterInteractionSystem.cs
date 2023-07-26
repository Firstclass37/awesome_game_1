using Game.Server.Events.Core;
using Game.Server.Events.List.Character;
using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects._Core;
using Game.Server.Models.GameObjects;
using Game.Server.Models.GamesObjectList;
using Game.Server.Storage;

namespace Game.Server.Logic.Systems
{
    internal class CharacterInteractionSystem : ISystem
    {
        private readonly IInteractionsCollection _interactionsCollection;
        private readonly IGameObjectAccessor _gameObjectAccessor;
        private readonly IStorage _storage;

        public CharacterInteractionSystem(IEventAggregator eventAggregator, IGameObjectAccessor gameObjectAccessor, IStorage storage, IInteractionsCollection interactionsCollection)
        {
            _gameObjectAccessor = gameObjectAccessor;

            eventAggregator.GetEvent<GameEvent<CharacterPositionChanged>>().Subscribe(OnPositionChanged);
            _storage = storage;
            _interactionsCollection = interactionsCollection;
        }

        public void Process(double gameTime)
        {

        }

        private void OnPositionChanged(CharacterPositionChanged changed)
        {
            var type = _storage.Get<GameObject>(changed.CharacterId).ObjectType;
            var interactWith = _gameObjectAccessor.FindAll(changed.Position).FirstOrDefault(p => p.GameObject.ObjectType != type);
            if (interactWith == null || !interactWith.Interactions.Any())
                return;

            var singleInteraction = interactWith.Interactions.First();
            var interaction = _interactionsCollection.Get(singleInteraction.InteractionType);

            var character = _gameObjectAccessor.Get(changed.CharacterId);
            interaction.Interact(interactWith, new Character(character), changed.Position);
        }
    }
}