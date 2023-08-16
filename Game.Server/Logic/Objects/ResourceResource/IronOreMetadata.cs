﻿using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Logic.Objects.ResourceResource.Creation;
using Game.Server.Models.Constants;
using Game.Server.Models.Resources;

namespace Game.Server.Logic.Objects.ResourceResource
{
    internal class IronOreMetadata : IGameObjectMetadata
    {
        private readonly IArea1x1Getter _area1X1Getter;

        public IronOreMetadata(IArea1x1Getter area1X1Getter)
        {
            _area1X1Getter = area1X1Getter;
        }

        public string ObjectType => ResourceResourceTypes.IronOre;

        public string Description => "IronOre";

        public IAreaGetter AreaGetter => _area1X1Getter;

        public ICreationRequirement CreationRequirement => new OnlyGroundRequirement();

        public IGameObjectFactory GameObjectFactory => new ResourceResourceFactory(ObjectType);

        public Price BasePrice => Price.Free;
    }
}