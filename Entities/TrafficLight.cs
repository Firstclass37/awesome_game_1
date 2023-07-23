using Godot;
using My_awesome_character.Core.Game;
using My_awesome_character.Core.Game.Constants;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class TrafficLight : Node2D
{
	private readonly Dictionary<Direction, Polygon2D> _directions = new();
	private readonly Dictionary<Direction, Area2D> _areas = new();
	private readonly Dictionary<Direction, MapCell> _trackingCells = new();
	private readonly HashSet<Guid> _skippedPlayers = new();

	public override void _Ready()
	{
		_directions.Add(Direction.Left, GetNode<Polygon2D>("TextureRect/Left_polygon"));
		_directions.Add(Direction.Top, GetNode<Polygon2D>("TextureRect/Top_polygon"));
		_directions.Add(Direction.Right, GetNode<Polygon2D>("TextureRect/Right_polygon"));
		_directions.Add(Direction.Bottom, GetNode<Polygon2D>("TextureRect/Bottom_polygon"));

		_areas.Add(Direction.Left, GetNode<Area2D>("TextureRect/Left_Area2D"));
        _areas.Add(Direction.Top, GetNode<Area2D>("TextureRect/Top_Area2D2"));
        _areas.Add(Direction.Right, GetNode<Area2D>("TextureRect/Right_Area2D3"));
        _areas.Add(Direction.Bottom, GetNode<Area2D>("TextureRect/Bottom_Area2D4"));

        foreach (var node in _directions.Values)
			node.Visible = false;

		foreach (var area in _areas)
		{
            area.Value.InputEvent += (vp, e, sid) => OnDirectionInputEvent(vp, e, sid, area.Key);
			area.Value.MouseEntered += () => OnDirectionAreaMouseEnter(area.Key);
			area.Value.MouseExited += () => OnDirectionAreaMouseExit(area.Key);
        }
	}

    public event Action<Direction> OnRightClick;

    public event Action<Direction> OnLeftClick;

    public override void _Process(double delta)
	{
	}

	public int Id { get; set; }

	public CoordianteUI MapPosition { get; set; }

	public Direction[] GetActiveDirections() => _directions.Where(d => d.Value.Visible).Select(d => d.Key).ToArray();

	public MapCell GetTrackingCell(Direction direction) => _trackingCells[direction];

    public MapCell[] GetTrackingCells() => _trackingCells.Values.ToArray();

	public Direction GetDirectionFor(MapCell cell) => _trackingCells.First(c => c.Value == cell).Key;

    public void ActivateDirection(Direction direction, MapCell trackingCell)
	{
		_directions[direction].Visible = true;
        _trackingCells.TryAdd(direction, trackingCell);
    }

	public void DeactivateDirection(Direction direction)
	{
		_directions[direction].Visible = false;
	}

	public int GetValue(Direction direction)
	{
		var polygon = _directions[direction];
		var label = polygon.GetChild<Label>(0);
		var values = label.Text.Split('/');
		var currentValue = values[0];
		return int.Parse(currentValue);
	}

	public void DecreaseValue(Direction direction)
	{
		SetValue(direction, GetValue(direction) - 1);
	}

	public int GetSize(Direction direction)
	{
		var polygon = _directions[direction];
		var label = polygon.GetChild<Label>(0);
		var values = label.Text.Split('/');
		var size = values[1];
		return int.Parse(size);
	}

	public void SetSize(Direction direction, int size)
	{
		var polygon = _directions[direction];
		var label = polygon.GetChild<Label>(0);
		var values = label.Text.Split('/');
		var currentValue = values[0];

		label.Text = $"{currentValue}/{size}";
	}

	public void SetValue(Direction direction, int value)
	{
		var polygon = _directions[direction];
		var label = polygon.GetChild<Label>(0);
		var values = label.Text.Split('/');
		var currentSize = values[1];

		label.Text = $"{value}/{currentSize}";
	}

	public void Reset(Direction direction)
	{
		SetValue(direction, GetSize(direction));
	}

	public void SkipCharacter(Guid characterId) => _skippedPlayers.Add(characterId);

	public bool WasSkipped(Guid characterId) => _skippedPlayers.Contains(characterId);

	public void ClearSkip(Guid characterId) => _skippedPlayers.Remove(characterId);


    private void OnDirectionInputEvent(Node viewport, InputEvent @event, long shapeIdx, Direction direction)
    {
		//var mouseEvent = @event as InputEventMouseButton;
        if (Input.IsActionJustReleased("left-click"))
            OnLeftClick?.Invoke(direction);
        else if (Input.IsActionJustReleased("right-click"))
            OnRightClick?.Invoke(direction);
    }

    private void OnDirectionAreaMouseEnter(Direction direction)
    {
        var polygon = _directions[direction];
        if (!polygon.Visible)
            return;

        polygon.Modulate = new Color("4cff21");
    }

    private void OnDirectionAreaMouseExit(Direction direction)
    {
        var polygon = _directions[direction];
        if (!polygon.Visible)
            return;

        polygon.Modulate = new Color("ffffff");
    }
}