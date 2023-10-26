using Game.Server.Common.Extentions;
using Game.Server.Logic.Maps.Extentions;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Models.Constants;
using Game.Server.Models.GamesObjectList;

namespace Game.Server.Logic.Maps.Generation
{
    internal class BuildingGenerator : IBuildingGenerator
    {
        private readonly IPlayerGrid _playerGrid;
        private readonly IAreaCalculator _areaCalculator;
        private readonly IGameObjectAccessor _gameObjectAccessor;
        private readonly IGameObjectCreator _gameObjectCreator;
        private readonly IGameObjectMetadataCollection _gameObjectMetadataCollection;

        public BuildingGenerator(IPlayerGrid playerGrid, IAreaCalculator areaCalculator, 
            IGameObjectAccessor gameObjectAccessor, IGameObjectCreator gameObjectCreator,
            IGameObjectMetadataCollection gameObjectMetadataCollection)
        {
            _playerGrid = playerGrid;
            _areaCalculator = areaCalculator;
            _gameObjectAccessor = gameObjectAccessor;
            _gameObjectCreator = gameObjectCreator;
            _gameObjectMetadataCollection = gameObjectMetadataCollection;
        }

        public void Generate()
        {
            foreach(var player in _playerGrid.Players)
            {
                GenerateHome(player, 3);
            }
        }

        private void GenerateHome(int player, int count)
        {
            var home = _gameObjectMetadataCollection.Get(BuildingTypes.Home);
            var homesLeft = count;
            foreach(var cell in _playerGrid.GetAvailableFor(player).Mix())
            {
                if (_areaCalculator.TryGetArea(cell, home.Size, out var area) == false)
                    continue;

                if (area.All(a => _playerGrid.GetPlayerOf(a) == player) == false)
                    continue;

                var creationParams = new CreationParams(home.ObjectType, cell, player);
                if (_gameObjectCreator.CanCreate(creationParams) == false)
                    continue;

                var preview = home.GameObjectFactory.CreateNew(cell, area);
                var spawn = preview.GetAttributeValue(HomeAttributes.SpawnCell);

                if (_gameObjectAccessor.Find(spawn)?.GameObject.ObjectType == BuildingTypes.Road)
                {
                    _gameObjectCreator.Create(creationParams);
                    homesLeft--;
                    if (homesLeft == 0)
                        return;
                }
            }
        }
    }
}