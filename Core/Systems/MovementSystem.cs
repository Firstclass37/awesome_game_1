﻿using Godot;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game;
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

        private static double _lastTime;

        public MovementSystem(IPathBuilder pathBuilder, ISceneAccessor sceneAccessor)
        {
            _pathBuilder = pathBuilder;
            _sceneAccessor = sceneAccessor;
        }

        public void Process(double gameTime)
        {
            var map = _sceneAccessor.FindFirst<Map>(SceneNames.Map);
            var character = _sceneAccessor.FindFirst<character>(SceneNames.Character);
            if (character == null)
                return;

            var randomPoint = map.GetCells.Where(p => p != character.MapPosition).OrderBy(g => Guid.NewGuid()).First();
            var pathToRandomPoint = _pathBuilder.FindPath(character.MapPosition, randomPoint, map);

            GD.Print($"Character: {character.MapPosition}");
            GD.Print($"New: {randomPoint}");
            GD.Print($"path: {string.Join(" - ", pathToRandomPoint.Select(p => p))}");


            foreach(var cell in pathToRandomPoint) 
            {
                character.GlobalPosition = map.GetGlobalPositionOf(cell);
                character.MapPosition = randomPoint;
            }
            Thread.Sleep(5000);
        }
    }
}