using My_awesome_character.Core.Game;
using My_awesome_character.Core.Game.Constants;
using My_awesome_character.Core.Infrastructure.Events;
using My_awesome_character.Core.Ui;
using System.Collections.Generic;

namespace My_awesome_character.Core.Systems.TrafficLights
{
    internal class TrafficLightCreationSystem : ISystem
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ISceneAccessor _sceneAccessor;

        public TrafficLightCreationSystem(IEventAggregator eventAggregator, ISceneAccessor sceneAccessor)
        {
            _eventAggregator = eventAggregator;
            _sceneAccessor = sceneAccessor;
        }

        public void OnStart()
        {
            //_eventAggregator.GetEvent<GameEvent<HomeCreateRequestEvent>>().Subscribe(OnBuildingCreated);
        }

        public void Process(double gameTime)
        {
        }

        //private void OnBuildingCreated(HomeCreateRequestEvent @event)
        //{
            //var map = _sceneAccessor.GetScene<Map>(SceneNames.Map);
            //var targetCell = @event.TargetCell;

            //var actualCell = map.GetActualCell(new Game.CoordianteUI(targetCell.X, targetCell.Y));
            //var candidates = map.GetNeighboursOf(targetCell).Union(new[] { actualCell })
            //    .Where(n => n.CellType == MapCellType.Road)
            //    .ToArray();

            //foreach(var candidate in candidates)
            //{
            //    var neighboursRoads = map
            //            .GetDirectedNeighboursOf(candidate)
            //            .Where(n => n.Key.CellType == MapCellType.Road)
            //            .ToDictionary(n => n.Key, n => n.Value);
                
            //    if (neighboursRoads.Any() && neighboursRoads.Count > 2)
            //    {
            //        var exists = _sceneAccessor
            //            .FindAll<TrafficLight>(t => t.MapPosition.Equals(new Game.CoordianteUI(candidate.X, candidate.Y)))
            //            .FirstOrDefault();

            //        if (exists != null)
            //        {
            //            var activeDirections = exists.GetActiveDirections();
            //            var newDirections = neighboursRoads
            //                .Where(n => !activeDirections.Contains(n.Value))
            //                .ToDictionary(n => n.Key, n => n.Value);
            //            AddDirections(exists, newDirections);
            //        }
            //        else
            //        {
            //            var trafficLight = Create(candidate, map);
            //            AddDirections(trafficLight, neighboursRoads);

            //            _eventAggregator.GetEvent<GameEvent<TrafficLightsCreatedEvent>>().Publish(new TrafficLightsCreatedEvent { Id = trafficLight.Id });
            //        }
            //    }
            //}
        //}

        private TrafficLight Create(CoordianteUI cell, Map map)
        {
            //var id = new Random().Next(0, 100000);
            ////todo: это должно быть в HomeCreatingSystem
            //var trafficLight = SceneFactory.Create<TrafficLight>(SceneNames.TrafficLight(id), ScenePaths.TrafficLight);
            //map.AddChild(trafficLight, forceReadableName: true);

            //trafficLight.Id = id;
            //trafficLight.MapPosition = cell;
            //trafficLight.Scale = new Vector2(0.2f, 0.2f);
            //trafficLight.Position = map.GetLocalPosition(cell);
            //return trafficLight;
            return default;
        }

        private void AddDirections(TrafficLight trafficLight, Dictionary<CoordianteUI, Direction> neightboars)
        {
            //foreach (var neightboar in neightboars)
            //{
            //    trafficLight.SetSize(neightboar.Value, 1);
            //    trafficLight.ActivateDirection(neightboar.Value, neightboar.Key);
            //}
        }
    }
}