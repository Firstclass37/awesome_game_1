﻿using Game.Server.Logic.Objects._Buidling;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;
using Game.Server.DataBuilding;
using Game.Server.Logic.Objects._Interactions;
using Game.Server.Logic._Extentions;
using Game.Server.Models.Constants.Attributes;
using Game.Server.Logic.Objects._Produce;
using Game.Server.Models.Resources;

namespace Game.Server.Logic.Objects.AluminumMine.Creation
{
    internal class AluminumMineFactory : IGameObjectFactory
    {
        public GameObjectAggregator CreateNew(Coordiante root, Coordiante[] area, int player)
        {
            var gameObject = new GameObjectAggregatorBuilder(BuildingTypes.AluminumMine, player)
                .AddArea(root, area)
                .AsManufactoring(new ManufactoringArgs 
                {
                    PrduceSpeedSeconds = 0,
                    ProduceAction = TypeInfoFactory.Create<IProduceAction, SwapResourcesProduceAction>(),
                    Requirements = new[] { TypeInfoFactory.Create<IProduceRequirement, EnoughtResourceRequirement>() },
                    RequriedResources = new[] { ResourceChunk.Create(ResourceType.Energy, 0.1f) },
                    ResultResources = new[] { ResourceChunk.Create(ResourceType.Aluminum, 1) }
                })
                .AsInteractable<ProduceBuildingInteraction>()
                .Build();
            return gameObject;
        }
    }
}