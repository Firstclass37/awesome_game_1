using Game.Server.Map;
using Game.Server.Models.Constants;
using Game.Server.Models.Maps;
using Godot;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game;
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
    public const int Block = 11;
    public const int BauxiteResource = 4;
    public const int IronOreResource = 12;
    public const int MineralsResource = 13;
    public const int CokeResource = 7;
    public const int SolarBatary = 14;
    public const int WindTurbine = 15;
}

public class MapLayers
{
    public const int GroundLayer = 0;
    public const int RoadLayer = 1;
	public const int Resources = 2;
    public const int Buildings = 3;
    public const int Preview = 4;
}

public partial class Map : Node2D
{
    private readonly Dictionary<int, MapCellType> _layersToTags = new Dictionary<int, MapCellType>()
	{
		{ MapLayers.GroundLayer, MapCellType.Groud },
		{ MapLayers.RoadLayer, MapCellType.Road },
		{ MapLayers.Resources, MapCellType.Resource },
        { MapLayers.Buildings, MapCellType.Building },
    };

	public event Action<CoordianteUI> OnCellClicked;

    public event Action<CoordianteUI> OnCellHovered;

    public TileMap TileMap => GetNode<TileMap>("TileMap");

    public Node2D CharacterContainer => GetNode<Node2D>("CharacterContainer");

    public Node2D ProjectileContainer => GetNode<Node2D>("ProjectileContainer");

    public Node2D TrafficLightsContainer => GetNode<Node2D>("TrafficLightsContainer");


    private Lazy<Dictionary<CoordianteUI, int>> _activeCells;


    public Godot.Vector2 GetLocalPosition(CoordianteUI coordiante)
    {
        return TileMap.MapToLocal(new Vector2I { X = coordiante.X, Y = coordiante.Y });
    }

    public Map()
    {
        _activeCells = new Lazy<Dictionary<CoordianteUI, int>>(GetInitialState);
    }

    public CoordianteUI[] GetCells() => _activeCells.Value.Keys.ToArray();


    public void AddCharacter(character character)
    {
        CharacterContainer.AddChild(character, forceReadableName: true);
    }

    public void RemoveCharacter(character character)
    {
        CharacterContainer.RemoveChild(character);
    }

    public void AddTrafficLight(TrafficLight trafficLight)
    {
        TrafficLightsContainer.AddChild(trafficLight, forceReadableName: true);
    }

    public void AddProjectile(Projectile projectile)
    {
        ProjectileContainer.AddChild(projectile, forceReadableName: true);
    }

    public void RemoveProjectile(Projectile projectile)
    {
        ProjectileContainer.RemoveChild(projectile);
    }

    public void SetCell(Guid objectId, CoordianteUI cell, CoordianteUI[] size, int layer, int sourceId, int alternativeTile = 0)
    {
        TileMap.SetCell(layer, new Vector2I(cell.X, cell.Y), sourceId, new Vector2I(0, 0), alternativeTile);
    }

    public void SetCellPreview(CoordianteUI cell, int sourceId, int alternativeTile = 0)
    {
        TileMap.SetCell(MapLayers.Preview, new Vector2I(cell.X, cell.Y), sourceId, new Vector2I(0, 0), alternativeTile);
    }

    public void ClearPreview(CoordianteUI cell)
    {
        TileMap.SetCell(MapLayers.Preview, new Vector2I(cell.X, cell.Y), -1);
    }

    public void ClearLayer(int layer) 
    {
        foreach(var cell in TileMap.GetUsedCells(layer))
            TileMap.SetCell(MapLayers.Preview, cell, -1);
    }

    public bool IsMouseExistsNew(Godot.Vector2 mousePosition, out CoordianteUI coordiante)
    {
        var localPos = TileMap.ToLocal(mousePosition);
        var tilePos = TileMap.LocalToMap(localPos);
        coordiante = new CoordianteUI(tilePos.X, tilePos.Y);

        return TileMap.GetUsedCells(MapLayers.GroundLayer).Any(c => c == tilePos);
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

	private Dictionary<CoordianteUI, int> GetInitialState()
	{
        var cells = new Dictionary<CoordianteUI, int>();
        var layers = _layersToTags.Select(l => new { LayerId = l.Key, Cells = TileMap.GetUsedCells(l.Key) }).OrderByDescending(l => l.LayerId).ToArray();
        foreach (var layer in layers)
        {
            foreach (var cell in layer.Cells)
            {
                var tile = TileMap.GetCellSourceId(layer.LayerId, cell);
                var mapCell = new CoordianteUI(cell.X, cell.Y);
                if (!cells.ContainsKey(mapCell))
                    cells.Add(mapCell, layer.LayerId);
            }
        }
        return cells;
    }

    public IReadOnlyDictionary<Coordiante, Direction> GetNeightborsOf(Coordiante coordiante)
    {
        var vector = new Vector2I(coordiante.X, coordiante.Y);

        return new Dictionary<Vector2I, Direction>
        {
            { TileMap.GetNeighborCell(vector, TileSet.CellNeighbor.BottomRightSide), Direction.Right },
            { TileMap.GetNeighborCell(vector, TileSet.CellNeighbor.BottomLeftSide), Direction.Bottom },
            { TileMap.GetNeighborCell(vector, TileSet.CellNeighbor.TopRightSide), Direction.Top },
            { TileMap.GetNeighborCell(vector, TileSet.CellNeighbor.TopLeftSide), Direction.Left }
        }
        .ToDictionary(v => new Coordiante(v.Key.X, v.Key.Y), v => v.Value);
    }
}