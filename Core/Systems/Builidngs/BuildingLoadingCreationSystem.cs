using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game.Events.Homes;
using My_awesome_character.Core.Infrastructure.Events;
using My_awesome_character.Core.Ui;
using My_awesome_character.Entities;
using System.Linq;

namespace My_awesome_character.Core.Systems.Builidngs
{
    internal class BuildingLoadingCreationSystem : ISystem
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ISceneAccessor _sceneAccessor;

        public BuildingLoadingCreationSystem(IEventAggregator eventAggregator, ISceneAccessor sceneAccessor)
        {
            _eventAggregator = eventAggregator;
            _sceneAccessor = sceneAccessor;
        }

        public void OnStart()
        {
            _eventAggregator.GetEvent<GameEvent<BuildingLoadingEvent>>().Subscribe(OnLoad);
        }

        public void Process(double gameTime)
        {

        }

        private void OnLoad(BuildingLoadingEvent @event)
        {
            var building = _sceneAccessor.FindAll<Home>(h => h.Id == @event.BuidlingId).First();
            var map = _sceneAccessor.GetScene<Map>(SceneNames.Map);

            var buidlingPosition = map.GetGlobalPositionOf(building.RootCell);

            var loadingBar = SceneFactory.Create<LoadingBar>(SceneNames.LoadingBar(@event.BuidlingId), ScenePaths.LoadingBar);
            loadingBar.Duration = @event.DurationSec;
            loadingBar.GlobalPosition = new Godot.Vector2(buidlingPosition.X, buidlingPosition.Y - 70);
            loadingBar.Scale = new Godot.Vector2(2, 2);

            map.AddChild(loadingBar);

            loadingBar.Start(null);
        }
    }
}