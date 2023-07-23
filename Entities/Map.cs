using Godot;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game;
using My_awesome_character.Core.Game.Buildings;
using My_awesome_character.Core.Game.Constants;
using My_awesome_character.Core.Game.Movement;
using System;
using System.Collections.Generic;
using System.Linq;


public static class Tiles
{
    public const int Ground = 1;
    public const int RoadAshpalt = 9;
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
    public const int Preview = 4;
}

public partial class Map : Node2D, INeighboursAccessor, IAreaCalculator, IMap
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

    public event Action<MapCell> OnCellHovered;

    public TileMap TileMap => GetNode<TileMap>("TileMap");


	private Lazy<Dictionary<MapCell, int>> _activeCells;


	public Godot.Vector2 GetGlobalPositionOf(MapCell mapCell) 
	{
		var cell = TileMap.MapToLocal(new Vector2I { X = mapCell.X, Y = mapCell.Y });
		return ToGlobal(cell);
	}

    public Godot.Vector2 GetLocalPosition(MapCell mapCell)
    {
        return TileMap.MapToLocal(new Vector2I { X = mapCell.X, Y = mapCell.Y });
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

        foreach(var c in size.Where(s => s != cell))
        {
            _activeCells.Value.Remove(c);
            _activeCells.Value.Add(c, _layersToTags.First(g => g.Value == c.CellType).Key);
        }
    }

    public void SetCell(Guid objectId, Coordiante cell, Coordiante[] size, int layer, int sourceId, int alternativeTile = 0)
    {
        TileMap.SetCell(layer, new Vector2I(cell.X, cell.Y), sourceId, new Vector2I(0, 0), alternativeTile);
    }

    public void SetCellPreview(Coordiante cell, int sourceId, int alternativeTile = 0)
    {
        TileMap.SetCell(MapLayers.Preview, new Vector2I(cell.X, cell.Y), sourceId, new Vector2I(0, 0), alternativeTile);
    }

    public void ClearPreview(MapCell cell)
    {
        TileMap.SetCell(MapLayers.Preview, new Vector2I(cell.X, cell.Y), -1);
    }

    public void ClearLayer(int layer) 
    {
        foreach(var cell in TileMap.GetUsedCells(layer))
            TileMap.SetCell(MapLayers.Preview, cell, -1);
    }

    public MapCell[] GetNeighboursOf(MapCell mapCell)
    {
		var neighbours = TileMap.GetSurroundingCells(new Vector2I(mapCell.X, mapCell.Y));
        return _activeCells.Value.Keys.Where(c => neighbours.Contains(new Vector2I(c.X, c.Y))).ToArray();
    }

    public Dictionary<MapCell, Direction> GetDirectedNeighboursOf(MapCell mapCell)
    {
        var vector = new Vector2I(mapCell.X, mapCell.Y);

        return new Dictionary<Vector2I, Direction>
        {
            { TileMap.GetNeighborCell(vector, TileSet.CellNeighbor.BottomRightSide), Direction.Right },
            { TileMap.GetNeighborCell(vector, TileSet.CellNeighbor.BottomLeftSide), Direction.Bottom },
            { TileMap.GetNeighborCell(vector, TileSet.CellNeighbor.TopRightSide), Direction.Top },
            { TileMap.GetNeighborCell(vector, TileSet.CellNeighbor.TopLeftSide), Direction.Left }
        }
        .Where(v => _activeCells.Value.Keys.Any(c => c.X == v.Key.X && c.Y == v.Key.Y))
        .ToDictionary(v => _activeCells.Value.Keys.First(c => c.X == v.Key.X && c.Y == v.Key.Y), v => v.Value);
    }


    public bool IsMouseExists(Godot.Vector2 mousePosition, out MapCell mapCell)
	{
        var localPos = TileMap.ToLocal(mousePosition);
        var tilePos = TileMap.LocalToMap(localPos);
		mapCell = GetCells().FirstOrDefault(c => c.X == tilePos.X && c.Y == tilePos.Y);

		return mapCell != default;
    }

    public bool IsMouseExistsNew(Godot.Vector2 mousePosition, out Coordiante coordiante)
    {
        var localPos = TileMap.ToLocal(mousePosition);
        var tilePos = TileMap.LocalToMap(localPos);
        coordiante = new Coordiante(tilePos.X, tilePos.Y);

        return tilePos != Vector2.Zero;
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

    //todo: переделать на сигналы/ивенты
    public override void _Input(InputEvent @event)
    {
        // InputEventMouseMotion - when mouse moved

        var mouseEvent = @event as InputEventMouse;
        if (mouseEvent == null)
            return;

        var pos = GetGlobalMousePosition();
        var localPos = TileMap.ToLocal(pos);
        var tilePos = TileMap.LocalToMap(localPos);
        if (Input.IsActionPressed("left-click") || Input.IsActionPressed("right-click"))
        {
            GD.Print($"Clicked on {tilePos.X} {tilePos.Y}");
            OnCellClicked?.Invoke(GetCells().First(c => c.X == tilePos.X && c.Y == tilePos.Y));
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

    public MapCell GetActualCell(Coordiante coordiante)
    {
        return _activeCells.Value.First(k => k.Key.X == coordiante.X && k.Key.Y == coordiante.Y).Key;
    }
}