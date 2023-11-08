using Game.Server.Common.Extentions;
using Game.Server.Logic.Maps.Extentions;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Models.Constants;
using Game.Server.Models.Maps;
using Game.Server.Storage;

namespace Game.Server.Logic.Maps.Generation
{
    internal interface IResourceGenerator
    {
        void Generate();
    }

    internal record ResourceGenerationInfo(string ResourceType, AreaSize AreaSize, int ResourceCount);

    internal class ResourceGenerator : IResourceGenerator
    {
        private readonly IPlayerGrid _playerGrid;
        private readonly IGameObjectCreator _gameObjectCreator;
        private readonly IAreaCalculator _areaCalculator;

        private readonly ResourceGenerationInfo[] _resourceToGenerate = new ResourceGenerationInfo[] 
        {
            new ResourceGenerationInfo(ResourceResourceTypes.Bauxite, AreaSize.Square(4), 7),
            new ResourceGenerationInfo(ResourceResourceTypes.Minerals, AreaSize.Square(4), 7),
            new ResourceGenerationInfo(ResourceResourceTypes.Uranium, AreaSize.Square(4), 7),
            new ResourceGenerationInfo(ResourceResourceTypes.Coke, AreaSize.Square(4), 7),
            new ResourceGenerationInfo(ResourceResourceTypes.IronOre, AreaSize.Square(4), 7)
        };

        public ResourceGenerator(IGameObjectCreator gameObjectCreator, IPlayerGrid layerGrid, IStorage storage, IAreaCalculator areaCalculator)
        {
            _gameObjectCreator = gameObjectCreator;
            _playerGrid = layerGrid;
            _areaCalculator = areaCalculator;
        }

        public void Generate()
        {
            foreach(var resource in _resourceToGenerate)
            {
                foreach(var player in _playerGrid.Players) 
                {
                    var group = FindArea(resource, player);
                    foreach (var point in group)
                        _gameObjectCreator.Create(new CreationParams(resource.ResourceType, point, player));
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
                    .Where(c => _gameObjectCreator.CanCreate(new CreationParams(generationInfo.ResourceType, c, playerId)))
                    .ToArray();
                if (availableCells.Length >= generationInfo.ResourceCount)
                    return availableCells.Take(generationInfo.ResourceCount).ToArray();
            }

            throw new ArgumentException($"can't found available area for {generationInfo.ResourceType} with area {generationInfo.AreaSize} (player: {playerId})");
        }
    }
}