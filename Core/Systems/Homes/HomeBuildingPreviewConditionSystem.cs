using Godot;
using Godot.Collections;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game.Events.Homes;
using My_awesome_character.Core.Infrastructure.Events;
using My_awesome_character.Core.Ui;
using My_awesome_character.Entities;
using System.Linq;

namespace My_awesome_character.Core.Systems.Homes
{
    internal class HomeBuildingPreviewConditionSystem : ISystem
    {
        private readonly ISceneAccessor _sceneAccessor;
        private readonly IEventAggregator _eventAggregator;

        private readonly Dictionary<string, BuildingTypes> _hotKeys = new Dictionary<string, BuildingTypes>()
        {
            { "h_pressed" , BuildingTypes.HomeType1 },
            { "m_pressed", BuildingTypes.MineUranus },
            { "p_pressed", BuildingTypes.PowerStation },
            { "r_pressed", BuildingTypes.Road }
        };

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
                    _eventAggregator.GetEvent<GameEvent<BuildingPreviewCanceledEvent>>().Publish(new BuildingPreviewCanceledEvent());

                    var pressed = _hotKeys.Keys.FirstOrDefault(k => Input.IsActionPressed(k));
                    if (pressed != null)
                        _eventAggregator.GetEvent<GameEvent<HomeCreateRequestEvent>>().Publish(new HomeCreateRequestEvent { BuildingType = _hotKeys[pressed], TargetCell = cell });
                }
            }
        }

        public void Process(double gameTime)
        {
            var pressed = _hotKeys.Keys.FirstOrDefault(k => Input.IsActionPressed(k));
            var anyReleased = _hotKeys.Keys.Any(k => Input.IsActionJustReleased(k));
            if (!string.IsNullOrWhiteSpace(pressed))
            {
                var buildingType = _hotKeys[pressed];
                var map = _sceneAccessor.FindFirst<Map>(SceneNames.Map);
                var mouseCoord = map.GetGlobalMousePosition();
                if (map.IsMouseExists(mouseCoord, out var cell))
                    _eventAggregator.GetEvent<GameEvent<BuildingPreviewEvent>>().Publish(new BuildingPreviewEvent { BuildingType = buildingType, TargetCell = cell });
                else
                    _eventAggregator.GetEvent<GameEvent<BuildingPreviewCanceledEvent>>().Publish(new BuildingPreviewCanceledEvent());
            }
            else if (anyReleased) 
            {
                _eventAggregator.GetEvent<GameEvent<BuildingPreviewCanceledEvent>>().Publish(new BuildingPreviewCanceledEvent());
            }
        }
    }
}