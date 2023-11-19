using Game.Server.Events.Core;
using Game.Server.Events.List.Homes;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Conveters;
using My_awesome_character.Core.Ui;

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
            _eventAggregator.GetEvent<GameEvent<ProduceStartEvent>>().Subscribe(OnLoad);
        }

        public void Process(double gameTime)
        {

        }

        private void OnLoad(ProduceStartEvent @event)
        {
            var map = _sceneAccessor.GetScene<Map>(SceneNames.Map);

            var buidlingPosition = map.GetLocalPosition(@event.Root.ToUi());

            var loadingBar = SceneFactory.Create<LoadingBar>(SceneNames.LoadingBar(@event.BuildingId), ScenePaths.LoadingBar);
            loadingBar.Duration = @event.Speed;
            loadingBar.Position = new Godot.Vector2(buidlingPosition.X, buidlingPosition.Y - 100);
            loadingBar.Scale = new Godot.Vector2(2, 2);
            loadingBar.ZIndex = 140;
            loadingBar.WhenEnd = (b) => WhenEnd(b);

            map.AddChild(loadingBar);
            loadingBar.Start();
        }

        private void WhenEnd(LoadingBar bar)
        {
            bar.GetParent().RemoveChild(bar);
            bar.Dispose();
        }
    }
}