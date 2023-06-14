using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game.Events.Homes;
using My_awesome_character.Core.Game.Events.Resource;
using My_awesome_character.Core.Infrastructure.Events;
using My_awesome_character.Core.Infrastructure.Events.Extentions;
using My_awesome_character.Core.Ui;
using My_awesome_character.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace My_awesome_character.Core.Systems.Builidngs
{
    internal class BuildingLoadingCreationSystem : ISystem
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ISceneAccessor _sceneAccessor;

        private readonly static Dictionary<int, Queue<BuildingDelayedActionEvent>> _eventsQueue = new Dictionary<int, Queue<BuildingDelayedActionEvent>>();

        public BuildingLoadingCreationSystem(IEventAggregator eventAggregator, ISceneAccessor sceneAccessor)
        {
            _eventAggregator = eventAggregator;
            _sceneAccessor = sceneAccessor;
        }

        public void OnStart()
        {
            _eventAggregator.GetEvent<GameEvent<BuildingDelayedActionEvent>>().Subscribe(OnLoad);
        }

        public void Process(double gameTime)
        {

        }

        private void OnLoad(BuildingDelayedActionEvent @event)
        {
            if (!_eventsQueue.ContainsKey(@event.BuidlingId))
                _eventsQueue.Add(@event.BuidlingId, new Queue<BuildingDelayedActionEvent>());

            var building = _sceneAccessor.FindAll<Home>(h => h.Id == @event.BuidlingId).First();
            var map = _sceneAccessor.GetScene<Map>(SceneNames.Map);
            var exisingLoading = _sceneAccessor.FindFirst<LoadingBar>(SceneNames.LoadingBar(@event.BuidlingId));
            if (exisingLoading != null)
            {
                _eventsQueue[@event.BuidlingId].Enqueue(@event);
                return;
            }

            var buidlingPosition = map.GetGlobalPositionOf(building.RootCell);

            var loadingBar = SceneFactory.Create<LoadingBar>(SceneNames.LoadingBar(@event.BuidlingId), ScenePaths.LoadingBar);
            loadingBar.Duration = @event.DelaySec;
            loadingBar.GlobalPosition = new Godot.Vector2(buidlingPosition.X, buidlingPosition.Y - 100);
            loadingBar.Scale = new Godot.Vector2(2, 2);
            loadingBar.WhenEnd = (b) => WhenEnd(@event.BuidlingId, @event.Event, b);

            map.AddChild(loadingBar);
            loadingBar.Start();
        }

        private void WhenEnd(int buildingId, Action @event, LoadingBar bar)
        {
            bar.GetParent().RemoveChild(bar);
            bar.Dispose();

            @event?.Invoke();
            
            if (_eventsQueue.Any())
            {
                var newEvent = _eventsQueue[buildingId].Dequeue();
                OnLoad(newEvent);
            }
        }
    }
}