using My_awesome_character.Core.Game.Models;
using My_awesome_character.Core.Game.Movement;
using System;
using System.Linq;

namespace My_awesome_character.Core.Game.TrafficLights
{
    internal class TrafficLightInteraction : ITrafficLightInteraction
    {
        private readonly IPointsman _pointsman;
        private readonly ICharacterMovement _characterMovement;

        public TrafficLightInteraction(IPointsman pointsman, ICharacterMovement characterMovement)
        {
            _pointsman = pointsman;
            _characterMovement = characterMovement;
        }

        public void Interact(TrafficLightModel trafficLight, character character, INeighboursAccessor neighboursAccessor)
        {
            if (trafficLight.AlreadySkipped.Remove(character.Id))
                return;

            _characterMovement.StopMoving(character);

            var characterCoordinates = new Coordiante(character.MapPosition.X, character.MapPosition.Y);
            var characterDirection = trafficLight.Tracking.First(t => t.Value.Equals(characterCoordinates)).Key;

            var direction = _pointsman.SelectDirection(trafficLight, characterDirection);
            var directionCoord = trafficLight.Tracking[direction];

            var wantMoveTo = neighboursAccessor.GetDirectedNeighboursOf(new MapCell(directionCoord.X, directionCoord.Y, Constatns.MapCellType.Road))
                .Where(d => d.Value == direction)
                .Select(d => d.Key)
                .FirstOrDefault();

            if (wantMoveTo == default)
                throw new ArgumentOutOfRangeException();

            _characterMovement.MoveTo(character, new Coordiante(wantMoveTo.X, wantMoveTo.Y));
            trafficLight.AlreadySkipped.Add(character.Id);
        }
    }
}