using My_awesome_character.Core.Game.Events.Character;
using My_awesome_character.Core.Infrastructure.Events;

namespace My_awesome_character.Core.Game.Movement
{
    internal class CharacterMovement : ICharacterMovement
    {
        private readonly IEventAggregator _eventAggregator;

        public CharacterMovement(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void MoveTo(character character, CoordianteUI coordiante)
        {
            _eventAggregator.GetEvent<GameEvent<MoveToRequestEvent>>()
                .Publish(new MoveToRequestEvent { CharacterId = character.Id, TargetCell = new MapCell(coordiante.X, coordiante.Y, Constatns.MapCellType.Road) });
        }

        public void StopMoving(character character)
        {
            _eventAggregator.GetEvent<GameEvent<StopMovingRequest>>()
                .Publish(new StopMovingRequest { CharacterId = character.Id });
        }
    }
}