﻿using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Logic.Objects.SteelPlant.Creation;
using Game.Server.Models.Constants;
using Game.Server.Models.Resources;

namespace Game.Server.Logic.Objects.SteelPlant
{
    internal class Metadata : IGameObjectMetadata
    {
        private readonly SteelPlantFactory _steelPlantFactory;

        public Metadata(SteelPlantFactory steelPlantFactory)
        {
            _steelPlantFactory = steelPlantFactory;
        }

        public string ObjectType => BuildingTypes.SteelPlant;

        public string Description => "Steel plant";

        public AreaSize Size => AreaSize.Area2x2;

        public ICreationRequirement CreationRequirement => new OnlyGroundRequirement();

        public IGameObjectFactory GameObjectFactory => _steelPlantFactory;

        public Price BasePrice => Price.Create(new ResourceChunk(ResourceType.Iron, 20));
    }
}