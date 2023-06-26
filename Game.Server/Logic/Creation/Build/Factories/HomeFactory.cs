using Game.Server.Events.Core;
using Game.Server.Events.List.Character;
using Game.Server.Logic.Building;
using Game.Server.Logic.Resources;
using Game.Server.Models.Buildings;
using Game.Server.Models.Constants;
using Game.Server.Models.Maps;

namespace My_awesome_character.Core.Game.Buildings.Build.Factories
{
    internal class HomeFactory : IBuildingFactory
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IResourceManager _resourceManager;

        public HomeFactory(IEventAggregator eventAggregator, IResourceManager resourceManager)
        {
            _eventAggregator = eventAggregator;
            _resourceManager = resourceManager;
        }

        public IBuilding Create(Coordiante targetCell, IAreaCalculator areaCalculator, IMap map)
        {
            var rootCell = targetCell;
            var spawnCell = new Coordiante(rootCell.X, rootCell.Y + 3);

            var building = new CommonBuilding();
            building.Id = new Random().Next(1, 1000000000);
            building.Cells = areaCalculator.Get2x2Area(rootCell);
            building.RootCell = rootCell;
            building.BuildingType = BuildingTypes.HomeType1;
            building.PeriodicAction = CreateAction(spawnCell, map);
            building.InteractionAction = CreateInteraction();

            return building;
        }

        private IInteractionAction CreateInteraction()
        {
            return new CommonInteractionAction(c =>
            {
                _eventAggregator.GetEvent<GameEvent<TakeDamageCharacterEvent>>().Publish(new TakeDamageCharacterEvent { CharacterId = c.Id, Damage = 1000 });
                _resourceManager.Increase(ResourceType.Uranus, 2);
            });
        }

        private IPeriodicAction CreateAction(Coordiante spawnCell, IMap map)
        {
            var @event = new CharacterCreationRequestEvent { InitPosition = spawnCell };
            var action = () =>
            {
                var actualCell = map.GetActualCell(new Coordiante(spawnCell.X, spawnCell.Y));
                if (actualCell.CellType == MapCellType.Road || actualCell.Tags.Contains(MapCellTags.Trap))
                    _eventAggregator.GetEvent<GameEvent<CharacterCreationRequestEvent>>().Publish(@event);
            };
            return new CommonPeriodicAction(action, 5, SystemNode.GameTime);
        }
    }
}