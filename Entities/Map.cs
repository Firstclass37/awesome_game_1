using Godot;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game.Movement;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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

	public event Action<MapCell> OnCellClicked;

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


    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton inputEventMouse)
		{
			var pos = inputEventMouse.GlobalPosition;
			var localPos = TileMap.ToLocal(pos);
			var tilePos = TileMap.LocalToMap(localPos);
			if (inputEventMouse.IsPressed())
			{
                GD.Print($"CLiked at: {tilePos}");
				//todo: переделать на сигналы/ивенты
				OnCellClicked?.Invoke(GetCells().First(c => c.X == tilePos.X && c.Y == tilePos.Y));
            }
		}
	}
}