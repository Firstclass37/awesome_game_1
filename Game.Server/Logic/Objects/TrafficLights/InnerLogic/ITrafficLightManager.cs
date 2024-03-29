﻿using Game.Server.Models.Buildings;
using Game.Server.Models.Constants;

namespace Game.Server.Logic.Objects.TrafficLights.InnerLogic
{
    internal interface ITrafficLightManager
    {
        void ActivateDirection(TrafficLight trafficLight, Direction direction);
        void DecreaseSize(TrafficLight trafficLight, Direction direction, int decrement = 1);

        void IncreaseSize(TrafficLight trafficLight, Direction direction, int increment = 1);

        void UpdateValue(TrafficLight trafficLight, Direction direction, int value);
    }
}