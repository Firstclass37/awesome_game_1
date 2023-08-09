using Game.Server.Models.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Server.Logic.Objects._Requirements
{
    internal class GameState
    {

    }

    internal interface IAvailabilityRequirement
    {
        bool Satisfy(GameState gameState, GameObjectAggregator gameObjectAggregator);
    }

    internal class AgeRequirement : IAvailabilityRequirement
    {
        public bool Satisfy(GameState gameState, GameObjectAggregator gameObjectAggregator)
        {
            throw new NotImplementedException();
        }
    }

    internal class PriceRequirement : IAvailabilityRequirement
    {
        public bool Satisfy(GameState gameState, GameObjectAggregator gameObjectAggregator)
        {
            throw new NotImplementedException();
        }
    }

    internal class WeatherRequirement : IAvailabilityRequirement
    {
        public bool Satisfy(GameState gameState, GameObjectAggregator gameObjectAggregator)
        {
            throw new NotImplementedException();
        }
    }

    internal class TimeRequirement : IAvailabilityRequirement
    {
        public bool Satisfy(GameState gameState, GameObjectAggregator gameObjectAggregator)
        {
            throw new NotImplementedException();
        }
    }

    internal class OtherBuildingRequirement : IAvailabilityRequirement
    {
        public bool Satisfy(GameState gameState, GameObjectAggregator gameObjectAggregator)
        {
            throw new NotImplementedException();
        }
    }
}