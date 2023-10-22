using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects._Interactions;
using Game.Server.Logic.Objects.Characters;
using Game.Server.Logic.Objects.TrafficLights.InnerLogic;
using Game.Server.Models.Buildings;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.GamesObjectList;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.TrafficLights.Interaction
{
    internal class TrafficLightInteraction : ICharacterInteraction
    {
        private readonly IPointsman _pointsman;
        private readonly IMover _characterMovement;
        private readonly IMapGrid _map;

        public TrafficLightInteraction(IPointsman pointsman, IMover characterMovement, IMapGrid map)
        {
            _pointsman = pointsman;
            _characterMovement = characterMovement;
            _map = map;
        }

        public void Interact(GameObjectAggregator gameObject, Character character, Coordiante interactionPoint)
        {
            if (gameObject.GameObject.ObjectType != BuildingTypes.TrafficLigh)
                return;

            var characterMovement = _characterMovement.GetCurrentMovement(character);
            if (characterMovement != null && characterMovement.Initiator == gameObject.GameObject.Id)
                return;

            var trafficLight = new TrafficLight(gameObject);
            if (trafficLight.GameObject.RootCell == interactionPoint)
                return;

            _characterMovement.StopMoving(character);

            var characterDirection = _map.GetDirectionOfNeightbor(trafficLight.GameObject.RootCell, character.Position);
            var targetDirection = _pointsman.SelectDirection(trafficLight, characterDirection);
            var directionCoord = trafficLight.GameObject.GetAttributeValue(TrafficLightAttributes.TrafficLightTrackingCells)[targetDirection];

            var wantMoveTo = _map.GetNeightborsOf(directionCoord)
                .Where(d => d.Value == targetDirection)
                .Select(d => d.Key)
                .First();

            _characterMovement.MoveTo(character, wantMoveTo, trafficLight.Id);
        }
    }
}