using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects._Interactions;
using Game.Server.Logic.Objects.Characters;
using Game.Server.Logic.Objects.TrafficLights.InnerLogic;
using Game.Server.Models.Buildings;
using Game.Server.Models.Constants;
using Game.Server.Models.Constants.Attributes;
using Game.Server.Models.GameObjects;
using Game.Server.Models.GamesObjectList;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.TrafficLights.Interaction
{
    internal class TrafficLightInteraction : CharacterInteraction
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

        protected override void OnInteract(GameObjectAggregator gameObject, Character character, Coordiante interactionPoint)
        {
            if (gameObject.GameObject.ObjectType != BuildingTypes.TrafficLigh)
                return;

            var movementIniciator = character.GameObject.GetAttributeValue(MovementAttributes.Iniciator);
            if (movementIniciator.HasValue && movementIniciator == gameObject.GameObject.Id)
                return;

            var trafficLight = new TrafficLight(gameObject);
            if (trafficLight.GameObject.RootCell == interactionPoint)
                return;

            _characterMovement.StopMoving(character.GameObject);

            var characterDirection = _map.GetDirectionOfNeightbor(trafficLight.GameObject.RootCell, character.GameObject.RootCell);
            var targetDirection = _pointsman.SelectDirection(trafficLight, characterDirection);
            var directionCoord = trafficLight.GameObject.GetAttributeValue(TrafficLightAttributes.TrafficLightTrackingCells)[targetDirection];

            var wantMoveTo = _map.GetNeightborsOf(directionCoord)
                .Where(d => d.Value == targetDirection)
                .Select(d => d.Key)
                .First();

            _characterMovement.MoveTo(character.GameObject, wantMoveTo, trafficLight.Id);
        }
    }
}