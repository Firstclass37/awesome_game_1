﻿using Godot;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game.Constants;
using My_awesome_character.Core.Game.Movement;
using System;
using System.Collections.Generic;
using System.Linq;


public static class Tiles
{
    public const int Road = 1;
    public const int HomeType1 = 5;
    public const int MineUranus = 8;
    public const int PowerStation = 10;
    public const int ResourceUranus = 6;
}

public class MapLayers
{
    public const int GroundLayer = 0;
    public const int RoadLayer = 1;
	public const int Resources = 2;
    public const int Buildings = 3;
}

public partial class Map : Node2D, INeighboursAccessor
{
    private readonly Dictionary<int, MapCellType> _layersToTags = new Dictionary<int, MapCellType>()
	{
		{ MapLayers.GroundLayer, MapCellType.Groud },
		{ MapLayers.RoadLayer, MapCellType.Road },
		{ MapLayers.Resources, MapCellType.Resource },
        { MapLayers.Buildings, MapCellType.Building },
    };

    private readonly Dictionary<int, string> _tileToTags = new Dictionary<int, string>
    {
        { Tiles.ResourceUranus,  MapCellTags.Resource_Uranus }
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

    public void SetCell(MapCell cell, MapCell[] size, int sourceId, int alternativeTile = 0)
    {
        var layer = _layersToTags.First(g => g.Value == cell.CellType).Key;
        TileMap.SetCell(layer, new Vector2I(cell.X, cell.Y), sourceId, new Vector2I(0, 0), alternativeTile);
        _activeCells.Value.Remove(cell);
        _activeCells.Value.Add(cell, layer);

        foreach(var c in size)
        {
            _activeCells.Value.Remove(c);
            _activeCells.Value.Add(c, _layersToTags.First(g => g.Value == c.CellType).Key);
        }
    }

    public void SetCellPreview(MapCell cell, int sourceId, int alternativeTile = 0)
    {
        var layer = _layersToTags.First(g => g.Value == cell.CellType).Key;
        TileMap.SetCell(layer, new Vector2I(cell.X, cell.Y), sourceId, new Vector2I(0, 0), alternativeTile);
    }

    public void ClearPreview(MapCell cell)
    {
        var layer = _layersToTags.First(g => g.Value == cell.CellType).Key;
        TileMap.SetCell(layer, new Vector2I(cell.X, cell.Y), -1);
    }

    public MapCell[] GetNeighboursOf(MapCell mapCell)
    {
		var neighbours = TileMap.GetSurroundingCells(new Vector2I(mapCell.X, mapCell.Y));
        return _activeCells.Value.Keys.Where(c => neighbours.Contains(new Vector2I(c.X, c.Y))).ToArray();
    }

	public bool IsMouseExists(Vector2 mousePosition, out MapCell mapCell)
	{
        var localPos = TileMap.ToLocal(mousePosition);
        var tilePos = TileMap.LocalToMap(localPos);
		mapCell = GetCells().FirstOrDefault(c => c.X == tilePos.X && c.Y == tilePos.Y);

		return mapCell != default;
    }

    public MapCell[] Get2x2Area(MapCell root)
    {
        var vector = new Vector2I(root.X, root.Y);

        var bottomRightSide = TileMap.GetNeighborCell(vector, TileSet.CellNeighbor.BottomRightSide);
        var bottomLeftSide = TileMap.GetNeighborCell(vector, TileSet.CellNeighbor.BottomLeftSide);
        var bottomCorner = TileMap.GetNeighborCell(vector, TileSet.CellNeighbor.BottomCorner);

        return new[] { vector, bottomRightSide, bottomLeftSide, bottomCorner }
            .Select(v => _activeCells.Value.Keys.First(c => c.X == v.X && c.Y == v.Y))
            .ToArray();
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
				//todo: переделать на сигналы/ивенты
				if (inputEventMouse.ButtonIndex == MouseButton.Left)
                {
                    OnCellClicked?.Invoke(GetCells().First(c => c.X == tilePos.X && c.Y == tilePos.Y));
                }

                var right = TileMap.GetNeighborCell(new Vector2I(tilePos.X, tilePos.Y), TileSet.CellNeighbor.RightSide);
                var rightCorner = TileMap.GetNeighborCell(new Vector2I(tilePos.X, tilePos.Y), TileSet.CellNeighbor.RightCorner);
                var bottomRightSide = TileMap.GetNeighborCell(new Vector2I(tilePos.X, tilePos.Y), TileSet.CellNeighbor.BottomRightSide);
                var bottomLeftSide = TileMap.GetNeighborCell(new Vector2I(tilePos.X, tilePos.Y), TileSet.CellNeighbor.BottomLeftSide);
                var bottomCorner = TileMap.GetNeighborCell(new Vector2I(tilePos.X, tilePos.Y), TileSet.CellNeighbor.BottomCorner);
                GD.Print($"CLiked at: {tilePos}, rightCorner: {rightCorner}  bottomRightSide {bottomRightSide} bottomLeftSite: {bottomLeftSide} bottomCorner {bottomCorner}");
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
                var tile = TileMap.GetCellSourceId(layer.LayerId, cell);
                var tags = _tileToTags.ContainsKey(tile) ? new string[] { _tileToTags[tile] } : Array.Empty<string>();
                var mapCell = new MapCell(cell.X, cell.Y, _layersToTags[layer.LayerId], tags);
                if (!cells.ContainsKey(mapCell))
                    cells.Add(mapCell, layer.LayerId);
            }
        }
        return cells;
    }
}