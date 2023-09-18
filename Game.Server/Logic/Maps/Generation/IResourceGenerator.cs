using Game.Server.Common.Extentions;
using Game.Server.Logic.Maps.Extentions;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;
using Game.Server.Storage;
using System;

namespace Game.Server.Logic.Maps.Generation
{
    internal interface IResourceGenerator
    {
        void Generate();
    }

    internal record ResourceGenerationInfo(string ResourceType, AreaSize AreaSize);

    internal class ResourceGenerator : IResourceGenerator
    {
        private readonly IPlayerGrid _playerGrid;
        private readonly IGameObjectCreator _gameObjectCreator;
        private readonly IStorage _storage;
        private readonly IAreaCalculator _areaCalculator;


        private readonly int _resourceAmount = 7;
        private readonly ResourceGenerationInfo[] _resourceToGenerate = new ResourceGenerationInfo[] 
        {
            new ResourceGenerationInfo(ResourceResourceTypes.Bauxite, AreaSize.Square(4)),
            new ResourceGenerationInfo(ResourceResourceTypes.Minerals, AreaSize.Square(4)),
            new ResourceGenerationInfo(ResourceResourceTypes.Uranium, AreaSize.Square(4)),
            new ResourceGenerationInfo(ResourceResourceTypes.Coke, AreaSize.Square(4)),
            new ResourceGenerationInfo(ResourceResourceTypes.IronOre, AreaSize.Square(4))
        };

        public ResourceGenerator(IGameObjectCreator gameObjectCreator, IPlayerGrid layerGrid, IStorage storage, IAreaCalculator areaCalculator)
        {
            _gameObjectCreator = gameObjectCreator;
            _playerGrid = layerGrid;
            _storage = storage;
            _areaCalculator = areaCalculator;
        }

        public void Generate()
        {
            var players = _storage.Find<PlayerToPosition>(t => true).GroupBy(pp => pp.PlayerNumber).Select(g => g.Key).ToArray();
            foreach(var resource in _resourceToGenerate)
            {
                foreach(var player in players) 
                {
                    var group = FindArea(resource, player);
                    foreach (var point in group)
                        _gameObjectCreator.Create(resource.ResourceType, point);
                }
            }
        }

        private IEnumerable<Coordiante> FindArea(ResourceGenerationInfo generationInfo, int playerId)
        {
            var playerCells = _playerGrid.GetAvailableFor(playerId).Mix();

            foreach(var coordiante in playerCells)
            {
                if (!_areaCalculator.TryGetArea(coordiante, generationInfo.AreaSize, out var area))
                    continue;

                if (!area.All(a => playerCells.Contains(a)))
                    continue;

                var availableCells = area
                    .Mix()
                    .Where(c => _gameObjectCreator.CanCreate(generationInfo.ResourceType, c))
                    .ToArray();
                if (availableCells.Length >= _resourceAmount)
                    return availableCells.Take(_resourceAmount).ToArray();
            }

            throw new ArgumentException($"can't found available area for {generationInfo.ResourceType} with area {generationInfo.AreaSize} (player: {playerId})");
        }
    }
}