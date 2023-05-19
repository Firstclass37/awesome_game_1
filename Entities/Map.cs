using Godot;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game.Movement;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;


public class MapLayers
{
    public const int GroundLayer = 0;
    public const int RoadLayer = 1;
	public const int Resources = 2;
    public const int Buildings = 4;
}

public partial class Map : Node2D, INeighboursAccessor
{
    private readonly Dictionary<int, string> _layersToTags = new Dictionary<int, string>()
	{
		{ MapLayers.GroundLayer, MapCellTags.Ground },
		{ MapLayers.RoadLayer, MapCellTags.Road },
		{ MapLayers.Resources, MapCellTags.Ground },
        { MapLayers.Buildings, MapCellTags.Blocking },
    };

	public event Action<MapCell> OnCellClicked;

    public TileMap TileMap => GetNode<TileMap>("TileMap");

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

        var layers = _layersToTags.Select(l => new { LayerId = l.Key, Cells = TileMap.GetUsedCells(l.Key) }).OrderByDescending(l => l.LayerId).ToArray();
        var cells = new HashSet<MapCell>();
		var neighbours = TileMap.GetSurroundingCells(new Vector2I(mapCell.X, mapCell.Y));

        foreach (var layer in layers)
        {
            foreach (var cell in neighbours)
            {
				var exists = TileMap.GetCellTileData(layer.LayerId, new Vector2I(cell.X, cell.Y)) != null;
				if (!exists)
					continue;

                var tempMapCell = new MapCell(cell.X, cell.Y, _layersToTags[layer.LayerId]);
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

		var layers = _layersToTags.Select(l => new { LayerId = l.Key, Cells = TileMap.GetUsedCells(l.Key) }).OrderByDescending(l => l.LayerId).ToArray();
		var cells = new HashSet<MapCell>();
		foreach(var layer in layers)
		{
			foreach(var cell in layer.Cells)
			{
				var mapCell = new MapCell(cell.X, cell.Y, _layersToTags[layer.LayerId]);
				if (!cells.Contains(mapCell))
					cells.Add(mapCell);
			}
		}
		_allCells = cells.ToArray();
		return _allCells;
	}

	public bool IsMouseExists(Vector2 mousePosition, out MapCell mapCell)
	{
        var localPos = TileMap.ToLocal(mousePosition);
        var tilePos = TileMap.LocalToMap(localPos);
		mapCell = GetCells().FirstOrDefault(c => c.X == tilePos.X && c.Y == tilePos.Y);

		return mapCell != default;
    }


    public override void _Input(InputEvent @event)
    {
        // InputEventMouseMotion - when mouse moved
        if (@event is InputEventMouseButton inputEventMouse)
		{
			var pos = inputEventMouse.GlobalPosition;
			var localPos = TileMap.ToLocal(pos);
			var tilePos = TileMap.LocalToMap(localPos);
			if (inputEventMouse.IsPressed())
			{
                GD.Print($"CLiked at: {tilePos}");
				//todo: переделать на сигналы/ивенты
				if (inputEventMouse.ButtonIndex == MouseButton.Left)
					OnCellClicked?.Invoke(GetCells().First(c => c.X == tilePos.X && c.Y == tilePos.Y));
            }
		}
	}
}