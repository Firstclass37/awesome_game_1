using Godot;
using System.Collections.Generic;
using System.Linq;

namespace My_awesome_character.Entities
{
    public partial class Home: Node2D
    {
        public int Id { get; set; }

        public double LastFireTime { get; set; }

        public Dictionary<MapCell, Vector2I> CellsToTile = new Dictionary<MapCell, Vector2I>();

        public MapCell[] Cells => CellsToTile.Keys.ToArray();

        public MapCell SpawnCell { get; set; }

        public Vector2I GetTileCoord(MapCell cell) => CellsToTile[cell];
    }
}