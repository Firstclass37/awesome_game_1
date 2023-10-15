using Game.Server.Events.Core;
using Game.Server.Events.List.Weather;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Conveters;
using My_awesome_character.Core.Ui;

namespace My_awesome_character.Core.Systems.Weather
{
    internal class WindSystem : ISystem
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ISceneAccessor _sceneAccessor;

        public WindSystem(IEventAggregator eventAggregator, ISceneAccessor sceneAccessor)
        {
            _eventAggregator = eventAggregator;
            _sceneAccessor = sceneAccessor;
        }

        public void OnStart()
        {
            _eventAggregator.GetEvent<GameEvent<WindStartEvent>>().Subscribe(OnStart);
            _eventAggregator.GetEvent<GameEvent<WindEndEvent>>().Subscribe(OnEnd);
        }

        public void Process(double gameTime)
        {

        }

        private void OnEnd(WindEndEvent @event)
        {
            var wind = _sceneAccessor.FindAll<Winddirection>();
            var map = _sceneAccessor.GetScene<Map>(SceneNames.Map);

            foreach(var windDir in wind)
                map.RemoveChild(windDir);
        }

        private void OnStart(WindStartEvent @event)
        {
            var map = _sceneAccessor.GetScene<Map>(SceneNames.Map);

            foreach(var windPoint in @event.Area)
            {
                var point = windPoint.ToUi();
                var windScene = SceneFactory.Create<Winddirection>(SceneNames.Wind(point), ScenePaths.WindDirection);
                windScene.Position = map.GetLocalPosition(point);
                windScene.ZIndex = 100;
                windScene.Scale = new Godot.Vector2(0.8f, 0.8f);
                windScene.Rotate(@event.Direction.ToUi());

                map.AddChild(windScene, true);
            }
        }
    }
}