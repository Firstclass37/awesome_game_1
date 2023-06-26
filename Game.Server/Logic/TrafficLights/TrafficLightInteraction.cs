using Game.Server.Logic.Characters;
using Game.Server.Models.Buildings;
using Game.Server.Models.Characters;

namespace Game.Server.Logic.TrafficLights
{
    internal class TrafficLightInteraction : ITrafficLightInteraction
    {
        private readonly IPointsman _pointsman;
        private readonly IMover _characterMovement;

        public TrafficLightInteraction(IPointsman pointsman, IMover characterMovement)
        {
            _pointsman = pointsman;
            _characterMovement = characterMovement;
        }

        public void Interact(TrafficLight trafficLight, Character character, INeighboursAccessor neighboursAccessor)
        {
            if (trafficLight.AlreadySkipped.Remove(character.Id))
                return;

            _characterMovement.StopMoving(character);

            var characterDirection = trafficLight.Tracking.First(t => t.Value.Equals(character.Position)).Key;

            var direction = _pointsman.SelectDirection(trafficLight, characterDirection);
            var directionCoord = trafficLight.Tracking[direction];

            var wantMoveTo = neighboursAccessor.GetDirectedNeighboursOf(directionCoord)
                .Where(d => d.Value == direction)
                .Select(d => d.Key)
                .First();

            _characterMovement.MoveTo(character, wantMoveTo);
            trafficLight.AlreadySkipped.Add(character.Id);
        }
    }
}