﻿using Game.Server.Logic.Characters.Movement.PathSearching.Base;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Characters.Movement.PathSearching
{
    internal class PathSearcherSettingsFactory : IPathSearcherSettingsFactory
    {
        public PathSearcherSetting<Coordiante> Create(INieighborsSearchStrategy<Coordiante> nieighborsSearchStrategy) =>
            new PathSearcherSetting<Coordiante>
            {
                HScoreStrategy = new HScoreStrategy(),
                GScoreStrategy = new GScoreStrategy(),
                NeighborsSearchStrategy = nieighborsSearchStrategy
            };
    }
}