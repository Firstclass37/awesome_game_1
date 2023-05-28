using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game;
using My_awesome_character.Core.Game.Events.Character;
using My_awesome_character.Core.Infrastructure.Events;
using My_awesome_character.Core.Ui;
using My_awesome_character.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_awesome_character.Core.Systems.Common
{
    internal class CharacterInteractionSytem : ISystem
    {
        private readonly ISceneAccessor _sceneAccessor;
        private readonly IEventAggregator _eventAggregator;

        public CharacterInteractionSytem(ISceneAccessor sceneAccessor, IEventAggregator eventAggregator)
        {
            _sceneAccessor = sceneAccessor;
            _eventAggregator = eventAggregator;
        }

        public void OnStart()
        {
            _eventAggregator.GetEvent<GameEvent<CharacterPositionChangedEvent>>().Subscribe(OnPositionChanged);
        }

        private void OnPositionChanged(CharacterPositionChangedEvent obj)
        {
            var interactionWith = _sceneAccessor
                .FindAll<Home>(h => h is IInteractable && h.Cells.Contains(obj.NewPosition))
                .Cast<IInteractable>()
                .FirstOrDefault();
            if (interactionWith != null && interactionWith.InteractionAction != null)
            {
                var character = _sceneAccessor.GetScene<character>(SceneNames.Character(obj.CharacterId));
                interactionWith.InteractionAction.Interacte(character);
            }
        }

        public void Process(double gameTime)
        {
        }
    }
}
