using Godot;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game.Buildings.Requirements;
using My_awesome_character.Core.Game.Events.Homes;
using My_awesome_character.Core.Infrastructure.Events;
using My_awesome_character.Core.Ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            var game = _sceneAccessor.GetScene<Node2D>(SceneNames.Game);
            var map = _sceneAccessor.FindFirst<Map>(SceneNames.Map);
            var randomPoint = map.GetCells().OrderBy(g => Guid.NewGuid()).First();

            var requirement = _buildRequirementProvider.GetRequirementFor(BuildingTypes.HomeType1);
            while (!requirement.CanBuild(map.Get2x2Area(randomPoint)))
                randomPoint = map.GetCells().OrderBy(g => Guid.NewGuid()).First();

            _eventAggregator.GetEvent<GameEvent<HomeCreateRequestEvent>>().Publish(new HomeCreateRequestEvent { BuildingType = BuildingTypes.HomeType1, TargetCell = randomPoint });

            _isInitialized = true;
        }
    }
}
