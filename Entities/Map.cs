using Godot;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game.Movement;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

public partial class Map : Node2D, INeighboursAccessor
{
	private const string _groundLayerName = "ground";
	private const string _roadLayerName = "road";

	private readonly System.Collections.Generic.Dictionary<int, string> _layers = new System.Collections.Generic.Dictionary<int, string>()
	{
		{ 0, _groundLayerName },
		{ 1, _roadLayerName }
	};

	private readonly System.Collections.Generic.Dictionary<string, string> _layersToTags = new System.Collections.Generic.Dictionary<string, string>()
	{
		{ _groundLayerName, MapCellTags.Ground },
		{ _roadLayerName, MapCellTags.Road }
	};

    private TileMap TileMap => GetNode<TileMap>("TileMap");

	public Vector2 GetGlobalPositionOf(MapCell mapCell) 
	{
		var cell = TileMap.MapToLocal(new Vector2I { X = mapCell.X, Y = mapCell.Y });
		return ToGlobal(cell);
	}

    public override void _Ready()
	{
	}

	public override void _Process(double delta)
    {
    }

	private ConcurrentDictionary<MapCell, MapCell[]>  _neighbours = new ConcurrentDictionary<MapCell, MapCell[]>();

    public MapCell[] GetNeighboursOf(MapCell mapCell)
    {
		if (_neighbours.ContainsKey(mapCell))
			return _neighbours[mapCell];

        var layers = _layers.Select(l => new { LayerId = l.Key, LayerName = l.Value, Cells = TileMap.GetUsedCells(l.Key) }).OrderByDescending(l => l.LayerId).ToArray();
        var cells = new HashSet<MapCell>();
		var neighbours = TileMap.GetSurroundingCells(new Vector2I(mapCell.X, mapCell.Y));

        foreach (var layer in layers)
        {
            foreach (var cell in neighbours)
            {
				var exists = TileMap.GetCellTileData(layer.LayerId, new Vector2I(cell.X, cell.Y)) != null;
				if (!exists)
					continue;

                var tempMapCell = new MapCell(cell.X, cell.Y, _layersToTags[layer.LayerName]);
                if (!cells.Contains(tempMapCell))
                    cells.Add(tempMapCell);
            }
        }
		_neighbours.TryAdd(mapCell, cells.ToArray());
        return _neighbours[mapCell];
    }

	private MapCell[] _allCells;

    public MapCell[] GetCells()
	{
		if (_allCells != null)
			return _allCells;

		var layers = _layers.Select(l => new { LayerId = l.Key, LayerName = l.Value, Cells = TileMap.GetUsedCells(l.Key) }).OrderByDescending(l => l.LayerId).ToArray();
		var cells = new HashSet<MapCell>();
		foreach(var layer in layers)
		{
			foreach(var cell in layer.Cells)
			{
				var mapCell = new MapCell(cell.X, cell.Y, _layersToTags[layer.LayerName]);
				if (!cells.Contains(mapCell))
					cells.Add(mapCell);
			}
		}
		_allCells = cells.ToArray();
		return _allCells;
	}
}

public struct MapCell
{
	public MapCell(int x, int y, params string[] tags)
	{
		X = x;
		Y = y;
		Tags = tags;
	}

	public int X { get; set; }

	public int Y { get; set; }

	public string[] Tags { get; set; }

    public static bool operator !=(MapCell a, MapCell b) => !(a == b);

    public static bool operator ==(MapCell a, MapCell b) => a.X == b.X && a.Y == b.Y;

	public override string ToString() => $"\"{X} {Y}\"";

	public override bool Equals([NotNullWhen(true)] object obj) => this == (MapCell)obj;

	public override int GetHashCode() => $"{X} {Y}".GetHashCode();
}