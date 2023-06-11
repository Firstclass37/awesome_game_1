using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game;
using My_awesome_character.Core.Game.Constants;
using My_awesome_character.Core.Game.Events;
using My_awesome_character.Core.Game.Movement.Path;
using My_awesome_character.Core.Game.Movement.Path_1;
using My_awesome_character.Core.Infrastructure.Events;
using My_awesome_character.Core.Ui;
using System;
using System.Collections.Generic;
using System.Linq;

namespace My_awesome_character.Core.Systems
{
    internal class RandomPathGeneratorSystem: ISystem
    {
        private readonly ISceneAccessor _sceneAccessor;
        private readonly IStorage _storage;
        private readonly IPathSearcherSettingsFactory _pathSearcherSettingsFactory;
        private readonly IPathSearcher _pathSearcher;
        private readonly IEventAggregator _eventAggregator;

        public RandomPathGeneratorSystem(ISceneAccessor sceneAccessor, IStorage storage, IPathSearcherSettingsFactory pathSearcherSettingsFactory,
            IPathSearcher pathSearcher, IEventAggregator eventAggregator)
        {
            _sceneAccessor = sceneAccessor;
            _storage = storage;
            _pathSearcherSettingsFactory = pathSearcherSettingsFactory;
            _pathSearcher = pathSearcher;
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<GameEvent<MovementEndEvent>>().Subscribe(OnCharacterMovementEnd);
        }

        private void OnCharacterMovementEnd(MovementEndEvent obj)
        {
            if (obj.ObjectType != typeof(character)) 
                return;

            GenerateRandomPath(obj.ObjectId);
        }

        private static Dictionary<int, character> Characters;

        public void Process(double gameTime)
        {
            var ready = (int)gameTime % 2 == 0;
            if (!ready)
                return;

            Characters = _sceneAccessor.FindAll<character>().ToDictionary(c => c.Id, c => c);

            var map = _sceneAccessor.FindFirst<Map>(SceneNames.Map);
            var characters = Characters.Values.Where(c => c.IsMoving == false).Take(20).ToArray();
            foreach (var character in characters)
                GenerateRandomPath(character, map);
        }

        private void GenerateRandomPath(int characterId)
        {
            var map = _sceneAccessor.FindFirst<Map>(SceneNames.Map);
            GenerateRandomPath(Characters[characterId], map);
        }

        private void GenerateRandomPath(character character, Map map)
        {
            var randomPoint = map.GetCells()
                .Where(p => p != character.MapPosition && IsRoad(p))
                .OrderBy(g => Guid.NewGuid())
                .First();
            var pathToRandomPoint = _pathSearcher.Search(character.MapPosition, randomPoint, _pathSearcherSettingsFactory.Create(SelectSelector(character.MapPosition, randomPoint, map)));

            _eventAggregator.GetEvent<GameEvent<MovementCharacterPathEvent>>().Publish(new MovementCharacterPathEvent
            {
                CharacterId = character.Id,
                Path = pathToRandomPoint
            });
        }

        private INieighborsSearchStrategy<MapCell> SelectSelector(MapCell currentPosition, MapCell targetPosition, Map map)
        {
            return new OnlyRoadNeighboursSelector(map);

            if (IsRoad(currentPosition) && IsRoad(targetPosition))
                return new OnlyRoadNeighboursSelector(map);
            else
                return new AllNeighboursSelector(map);
        }

        public void OnStart()
        {
        }


        private bool IsRoad(MapCell cell) => cell.CellType == MapCellType.Road || cell.Tags.Contains(MapCellTags.Trap);

    }
}