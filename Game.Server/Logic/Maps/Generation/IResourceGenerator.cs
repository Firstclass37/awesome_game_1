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
        private readonly IMapGrid _mapGrid;
        private readonly IGameObjectAccessor _gameObjectAccessor;

        private readonly ResourceGenerationInfo[] _resourceToGenerate = new ResourceGenerationInfo[] 
        {
            new ResourceGenerationInfo(ResourceResourceTypes.Bauxite, new AreaSize(3, 3)),
            new ResourceGenerationInfo(ResourceResourceTypes.Minerals, new AreaSize(3, 3)),
            new ResourceGenerationInfo(ResourceResourceTypes.Uranium, new AreaSize(3, 3)),
            new ResourceGenerationInfo(ResourceResourceTypes.Coke, new AreaSize(3, 3)),
            new ResourceGenerationInfo(ResourceResourceTypes.IronOre, new AreaSize(3, 3))
        };

        public ResourceGenerator(IGameObjectCreator gameObjectCreator, IPlayerGrid layerGrid, IStorage storage, IAreaCalculator areaCalculator, IMapGrid mapGrid, IGameObjectAccessor gameObjectAccessor)
        {
            _gameObjectCreator = gameObjectCreator;
            _playerGrid = layerGrid;
            _storage = storage;
            _areaCalculator = areaCalculator;
            _mapGrid = mapGrid;
            _gameObjectAccessor = gameObjectAccessor;
        }

        public void Generate()
        {
            var players = GetPlayers();
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
            var randomPoints = _playerGrid.GetAvailableFor(playerId)
                .OrderBy(g => Guid.NewGuid())
                .ToArray();

            foreach(var coordiante in randomPoints)
            {
                var area = GetArea(coordiante, generationInfo.AreaSize);
                if (area.All(a => randomPoints.Contains(a) && CanCreateHere(area, generationInfo.ResourceType)))
                    return area;
            }

            throw new ArgumentException($"can't found available area for {generationInfo.ResourceType} with area {generationInfo.AreaSize} (player: {playerId})");
        }


        private Coordiante[] GetArea(Coordiante root, AreaSize areaSize)
        {
            try
            {
                return _areaCalculator.GetArea(root, areaSize);
            }
            catch
            {
                return GetArea(root, areaSize);
            }
        }

        private bool CanCreateHere(Coordiante[] area, string objectType)
        {
            return area.All(a => _gameObjectCreator.CanCreate(objectType, a)) &&
                area.SelectMany(a => _mapGrid.GetNeightborsOf(a)).All(a => _gameObjectAccessor.Find(a.Key).GameObject.ObjectType == GroundTypes.Ground);
        }

        private int[] GetPlayers() => 
             _storage.Find<PlayerToPosition>(t => true).GroupBy(pp => pp.PlayerNumber).Select(g => g.Key).ToArray();
    }
}