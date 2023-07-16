﻿using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Logic.Objects.Home.Creation;
using Game.Server.Models.Constants;
using Game.Server.Models.Resources;

namespace Game.Server.Logic.Objects.Home
{
    internal class Metadata : IGameObjectMetadata
    {
        private readonly IDefaultAreaGetterFactory _areFactory;
        private readonly HomeFactory _homeFactory;
        private readonly OnlyGroundRequirement _onlyGroundRequirement;

        public Metadata(IDefaultAreaGetterFactory areFactory, HomeFactory homeFactory, OnlyGroundRequirement onlyGroundRequirement)
        {
            _areFactory = areFactory;
            _homeFactory = homeFactory;
            _onlyGroundRequirement = onlyGroundRequirement;
        }

        public string ObjectType => BuildingTypes.Home;

        public string Description => "Super home";

        public IAreaGetter AreaGetter => _areFactory.Get2x2();

        public ICreationRequirement CreationRequirement => _onlyGroundRequirement;

        public IGameObjectFactory GameObjectFactory => _homeFactory;

        public Price BasePrice => Price.Create(new ResourceChunk(ResourceType.Money, 100));
    }
}