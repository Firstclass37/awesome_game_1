using Godot;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game.Movement;
using My_awesome_character.Core.Ui;
using System;
using System.Linq;
using System.Threading;

namespace My_awesome_character.Core.Systems
{
    internal class MovementSystem: ISystem
    {
        private readonly IPathBuilder _pathBuilder;
        private readonly ISceneAccessor _sceneAccessor;

        public MovementSystem(IPathBuilder pathBuilder, ISceneAccessor sceneAccessor)
        {
            _pathBuilder = pathBuilder;
            _sceneAccessor = sceneAccessor;
        }

        public void Process(double gameTime)
        {
            var map = _sceneAccessor.FindFirst<Map>(SceneNames.Map);
            var character = _sceneAccessor.FindFirst<character>(SceneNames.Character);
            var game = _sceneAccessor.FindFirst<Node2D>(SceneNames.Game);
            if (character == null || character.IsBusy)
                return;

            var randomPoint = map.GetCells().Where(p => p != character.MapPosition).OrderBy(g => Guid.NewGuid()).First();
            var pathToRandomPoint = _pathBuilder.FindPath(character.MapPosition, randomPoint, SelectSelector(character.MapPosition, randomPoint, map));

            GD.Print($"Character: {character.MapPosition}");
            GD.Print($"New: {randomPoint}");
            GD.Print($"path: {string.Join(" - ", pathToRandomPoint.Select(p => p))}");

            character.MoveTo(pathToRandomPoint, mc => game.ToLocal(map.GetGlobalPositionOf(mc)));
        }

        private INeighboursSelector SelectSelector(MapCell currentPosition, MapCell targetPosition, Map map)
        {
            if (currentPosition.Tags.Contains(MapCellTags.Road) && targetPosition.Tags.Contains(MapCellTags.Road))
                return new OnlyRoadNeighboursSelector(map);
            else
                return new AllNeighboursSelector(map);
        }
    }
}