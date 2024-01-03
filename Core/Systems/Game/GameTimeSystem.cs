using Game.Server.Events.Core;
using Game.Server.Events.List.Game;
using Game.Server.Models.Constants;
using Godot;
using My_awesome_character.Common.Extentions;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Ui;
using System;

namespace My_awesome_character.Core.Systems.Game
{
    internal class GameTimeSystem : ISystem
    {
        private readonly ISceneAccessor _sceneAccessor;

        public GameTimeSystem(IEventAggregator eventAggregator, ISceneAccessor sceneAccessor)
        {
            _sceneAccessor = sceneAccessor;

            eventAggregator.SubscribeGameEvent<TimeChangedEvent>(OnTimeChanged);
        }

        private void OnTimeChanged(TimeChangedEvent @event)
        {
            var map = _sceneAccessor.FindFirst<Map>(SceneNames.Map, isStatic: true);
            map.Modulate = CalculateModulation(@event.TimeOfDay);
        }

        private Color CalculateModulation(TimeOfDay timeOfDay)
        {
            if (timeOfDay == TimeOfDay.Day)
                return new Color("ffffff", 255);

            if (timeOfDay == TimeOfDay.Dawn || timeOfDay == TimeOfDay.Sunset)
                return new Color("6e6e6e", 255);

            if (timeOfDay == TimeOfDay.Night)
                return new Color("282828", 255);

            throw new ArgumentException($"unknown timeOfDay value {timeOfDay}");
        }


        public void OnStart()
        {
        }

        public void Process(double gameTime)
        {
        }
    }
}