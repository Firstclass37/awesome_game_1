using Godot;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game.Events.Homes;
using My_awesome_character.Core.Infrastructure.Events;
using My_awesome_character.Core.Ui;
using My_awesome_character.Entities;

namespace My_awesome_character.Core.Systems.Homes
{
    internal class HomeBuildingPreviewConditionSystem : ISystem
    {
        private readonly ISceneAccessor _sceneAccessor;
        private readonly IEventAggregator _eventAggregator;

        public HomeBuildingPreviewConditionSystem(IEventAggregator eventAggregator, ISceneAccessor sceneAccessor)
        {
            _eventAggregator = eventAggregator;
            _sceneAccessor = sceneAccessor;
        }

        public void OnStart()
        {
            _sceneAccessor.FindFirst<Map>(SceneNames.Map).OnCellClicked += HomeBuildingPreviewConditionSystem_OnCellClicked; ;
        }

        private void HomeBuildingPreviewConditionSystem_OnCellClicked(MapCell obj)
        {
            var existingPreview = _sceneAccessor.FindFirst<Home>(SceneNames.Builidng_preview(typeof(Home)));
            if (existingPreview != null) 
            {
                var map = _sceneAccessor.FindFirst<Map>(SceneNames.Map);
                var mouseCoord = map.GetGlobalMousePosition();
                if (map.IsMouseExists(mouseCoord, out var cell))
                {
                    _eventAggregator.GetEvent<GameEvent<HomePreviewCanceledEvent>>().Publish(new HomePreviewCanceledEvent());
                    _eventAggregator.GetEvent<GameEvent<HomeCreateRequestEvent>>().Publish(new HomeCreateRequestEvent { TargetCell = cell });
                }
            }
        }

        public void Process(double gameTime)
        {
            if (Input.IsActionPressed("d_pressed"))
            {
                var map = _sceneAccessor.FindFirst<Map>(SceneNames.Map);
                var mouseCoord = map.GetGlobalMousePosition();
                if (map.IsMouseExists(mouseCoord, out var cell))
                    _eventAggregator.GetEvent<GameEvent<HomePreviewEvent>>().Publish(new HomePreviewEvent { TargetCell = cell });
                else
                    _eventAggregator.GetEvent<GameEvent<HomePreviewCanceledEvent>>().Publish(new HomePreviewCanceledEvent());
            }
            else if (Input.IsActionJustReleased("d_pressed")) 
            {
                _eventAggregator.GetEvent<GameEvent<HomePreviewCanceledEvent>>().Publish(new HomePreviewCanceledEvent());
            }
        }
    }
}