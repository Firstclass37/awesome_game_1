using Game.Server.Logic.Objects._Buidling;
using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;
using Game.Server.Storage;

namespace Game.Server.Logic.Maps.Generation
{
    internal interface IResourceGenerator
    {
        void Generate();
    }

    internal class ResourceGenerator : IResourceGenerator
    {
        private readonly IPlayerGrid _playerGrid;
        private readonly IGameObjectCreator _gameObjectCreator;
        private readonly IStorage _storage;
        private readonly IAreaCalculator _areaCalculator;

        private const int _eachResourceCount = 4;
        private readonly string[] _resourceToGenerate = new string[] 
        {
            ResourceResourceTypes.Bauxite,
            ResourceResourceTypes.Minerals,
            ResourceResourceTypes.Uranium,
            ResourceResourceTypes.Coke,
            ResourceResourceTypes.IronOre
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
            var players = GetPlayers();
            foreach(var  resource in _resourceToGenerate)
            {
                foreach(var player in players) 
                {
                    var group = SelectGroupsOfFour(resource, player);
                    foreach (var point in group)
                        _gameObjectCreator.Create(resource, point);
                }
            }        
        }

        private IEnumerable<Coordiante> SelectGroupsOfFour(string objectType, int playerId)
        {
            var randomPoints = _playerGrid.GetAvailableFor(playerId)
                .OrderBy(g => Guid.NewGuid())
                .ToArray();

            foreach(var coordiante in randomPoints)
            {
                var area = _areaCalculator.Get2x2Area(coordiante);
                if (area.All(a => randomPoints.Contains(a) && _gameObjectCreator.CanCreate(objectType, a)))
                    return area;
            }

            throw new ArgumentException($"can't found available area for {objectType} (player: {playerId})");
        }

        private int[] GetPlayers() => 
             _storage.Find<PlayerToPosition>(t => true).GroupBy(pp => pp.PlayerNumber).Select(g => g.Key).ToArray();
    }
}