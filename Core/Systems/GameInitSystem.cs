using Godot;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game;
using My_awesome_character.Core.Game.Constants;
using My_awesome_character.Core.Ui;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace My_awesome_character.Core.Systems
{
    internal class GameInitSystem : ISystem
    {
        private readonly ISceneAccessor _sceneAccessor;

        public GameInitSystem(ISceneAccessor sceneAccessor)
        {
            _sceneAccessor = sceneAccessor;
        }

        public void OnStart()
        {
            
        }

        private bool _isInitialized = false;

        public void Process(double gameTime)
        {
            if (_isInitialized)
                return;

            ExportMapToJson();
            _isInitialized = true;
        }

        private void ExportMapToJson()
        {
            var map = _sceneAccessor.FindFirst<Map>(SceneNames.Map);
            
            var cells = map.GetCells();
            var grid = map.GetCells()
                .Select(c => new 
                {
                    Coordinate = new CoordianteUI(c.X, c.Y),
                    Neightbors = map.GetDirectedNeighboursOf(c).Select(n => new Neightbor(new CoordianteUI(n.Key.X, n.Key.Y), n.Value)).ToArray()
                })
                .ToArray();

            var info = new
            {
                Grid = grid
            };

            
            var filePath = Path.Combine(@"C:\Projects\Mine\My_awesome_character.Tests\bin\Debug\net6.0", "map.json");

            File.WriteAllText(filePath, JsonConvert.SerializeObject(grid));
            GD.Print(filePath);
        }

        private class Neightbor
        {
            public Neightbor(CoordianteUI coordinate, Direction direction)
            {
                Coordinate = coordinate;
                Direction = direction;
            }

            public CoordianteUI Coordinate { get; set; }

            public Direction Direction { get; set; }
        }
    }
}