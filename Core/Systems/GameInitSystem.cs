using Godot;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game;
using My_awesome_character.Core.Game.Buildings.Requirements;
using My_awesome_character.Core.Game.Constants;
using My_awesome_character.Core.Game.Events.Homes;
using My_awesome_character.Core.Infrastructure.Events;
using My_awesome_character.Core.Ui;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace My_awesome_character.Core.Systems
{
    internal class GameInitSystem : ISystem
    {
        private readonly ISceneAccessor _sceneAccessor;
        private readonly IEventAggregator _eventAggregator;
        private readonly IBuildRequirementProvider _buildRequirementProvider;

        public GameInitSystem(ISceneAccessor sceneAccessor, IEventAggregator eventAggregator, IBuildRequirementProvider buildRequirementProvider)
        {
            _sceneAccessor = sceneAccessor;
            _eventAggregator = eventAggregator;
            _buildRequirementProvider = buildRequirementProvider;
        }

        public void OnStart()
        {
            
        }

        private bool _isInitialized = false;

        public void Process(double gameTime)
        {
            if (_isInitialized)
                return;

            ExportMapToJson();

            var game = _sceneAccessor.GetScene<Node2D>(SceneNames.Game);
            var map = _sceneAccessor.FindFirst<Map>(SceneNames.Map);
            var randomPoint = map.GetCells().OrderBy(g => Guid.NewGuid()).First();

            var requirement = _buildRequirementProvider.GetRequirementFor(BuildingTypes.HomeType1);
            while (!requirement.CanBuild(map.Get2x2Area(randomPoint)))
                randomPoint = map.GetCells().OrderBy(g => Guid.NewGuid()).First();

            _eventAggregator.GetEvent<GameEvent<HomeCreateRequestEvent>>().Publish(new HomeCreateRequestEvent { BuildingType = BuildingTypes.HomeType1, TargetCell = randomPoint });

            _isInitialized = true;
        }

        private void ExportMapToJson()
        {
            var map = _sceneAccessor.FindFirst<Map>(SceneNames.Map);
            
            var cells = map.GetCells();
            var grid = map.GetCells()
                .Select(c => new 
                {
                    Coordinate = new Coordiante(c.X, c.Y),
                    Neightbors = map.GetDirectedNeighboursOf(c).Select(n => new Neightbor(new Coordiante(n.Key.X, n.Key.Y), n.Value)).ToArray()
                })
                .ToArray();

            var info = new
            {
                Grid = grid
            };

            
            var filePath = Path.Combine(@"C:\Projects\Mine\My_awesome_character.Tests\bin\Debug\net6.0", "map.json");

            File.WriteAllText(filePath, JsonConvert.SerializeObject(grid));
            GD.Print(filePath);
        }

        private class Neightbor
        {
            public Neightbor(Coordiante coordinate, Direction direction)
            {
                Coordinate = coordinate;
                Direction = direction;
            }

            public Coordiante Coordinate { get; set; }

            public Direction Direction { get; set; }
        }
    }
}