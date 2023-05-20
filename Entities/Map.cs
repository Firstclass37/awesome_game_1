﻿using Godot;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game.Movement;
using System;
using System.Collections.Generic;
using System.Linq;


public class MapLayers
{
    public const int GroundLayer = 0;
    public const int RoadLayer = 1;
	public const int Resources = 2;
    public const int Buildings = 3;
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


	private Lazy<Dictionary<MapCell, int>> _activeCells;


	public Vector2 GetGlobalPositionOf(MapCell mapCell) 
	{
		var cell = TileMap.MapToLocal(new Vector2I { X = mapCell.X, Y = mapCell.Y });
		return ToGlobal(cell);
	}

    public Map()
    {
        _activeCells = new Lazy<Dictionary<MapCell, int>>(GetInitialState);
    }

    public override void _Ready()
	{
    }

	public override void _Process(double delta)
    {
    }

    public MapCell[] GetCells() => _activeCells.Value.Keys.ToArray();

    public MapCell[] GetNeighboursOf(MapCell mapCell)
    {
        var cells = new HashSet<MapCell>();
		var neighbours = TileMap.GetSurroundingCells(new Vector2I(mapCell.X, mapCell.Y));
        var layers = _activeCells.Value.Select(g => g.Value).OrderByDescending(g => g).ToArray();

        foreach (var layer in layers)
        {
            foreach (var cell in neighbours)
            {
				var exists = TileMap.GetCellTileData(layer, new Vector2I(cell.X, cell.Y)) != null;
				if (!exists)
					continue;

                var tempMapCell = new MapCell(cell.X, cell.Y, _layersToTags[layer]);
                if (!cells.Contains(tempMapCell))
                    cells.Add(tempMapCell);
            }
        }
        return cells.ToArray();
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

	private Dictionary<MapCell, int> GetInitialState()
	{
        var cells = new Dictionary<MapCell, int>();
        var layers = _layersToTags.Select(l => new { LayerId = l.Key, Cells = TileMap.GetUsedCells(l.Key) }).OrderByDescending(l => l.LayerId).ToArray();
        foreach (var layer in layers)
        {
            foreach (var cell in layer.Cells)
            {
                var mapCell = new MapCell(cell.X, cell.Y, _layersToTags[layer.LayerId]);
                if (!cells.ContainsKey(mapCell))
                    cells.Add(mapCell, layer.LayerId);
            }
        }
        return cells;
    }
}