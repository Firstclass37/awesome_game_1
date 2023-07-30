using Godot;
using My_awesome_character.Core.Game;
using My_awesome_character.Core.Game.Constants;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class TrafficLight : Node2D
{
	private readonly Dictionary<DirectionUI, Polygon2D> _directions = new();
	private readonly Dictionary<DirectionUI, Area2D> _areas = new();

	public override void _Ready()
	{
		_directions.Add(DirectionUI.Left, GetNode<Polygon2D>("TextureRect/Left_polygon"));
		_directions.Add(DirectionUI.Top, GetNode<Polygon2D>("TextureRect/Top_polygon"));
		_directions.Add(DirectionUI.Right, GetNode<Polygon2D>("TextureRect/Right_polygon"));
		_directions.Add(DirectionUI.Bottom, GetNode<Polygon2D>("TextureRect/Bottom_polygon"));

		_areas.Add(DirectionUI.Left, GetNode<Area2D>("TextureRect/Left_Area2D"));
        _areas.Add(DirectionUI.Top, GetNode<Area2D>("TextureRect/Top_Area2D2"));
        _areas.Add(DirectionUI.Right, GetNode<Area2D>("TextureRect/Right_Area2D3"));
        _areas.Add(DirectionUI.Bottom, GetNode<Area2D>("TextureRect/Bottom_Area2D4"));

        foreach (var node in _directions.Values)
			node.Visible = false;

		foreach (var area in _areas)
		{
			var inputHandler = area.Value.GetNode<ColorRect>("ColorRect");
			if (inputHandler == null)
				continue;

            inputHandler.GuiInput += (e) => OnDirectionInputEvent(e, area.Key);
            inputHandler.MouseEntered += () => OnDirectionAreaMouseEnter(area.Key);
            inputHandler.MouseExited += () => OnDirectionAreaMouseExit(area.Key);
        }
	}

    public event Action<DirectionUI> OnRightClick;

    public event Action<DirectionUI> OnLeftClick;

    public override void _Process(double delta)
	{

	}

	public Guid Id { get; set; }

	public CoordianteUI MapPosition { get; set; }

	public DirectionUI[] GetActiveDirections() => _directions.Where(d => d.Value.Visible).Select(d => d.Key).ToArray();

	public bool IsActiveDirection(DirectionUI direction) => _directions.Where(d => d.Value.Visible).Any(d => d.Key == direction);

    public void Activate(DirectionUI direction, CoordianteUI trackingCell)
	{
		_directions[direction].Visible = true;
    }

	public void Deactivate(DirectionUI direction)
	{
		_directions[direction].Visible = false;
	}

	public int GetSize(DirectionUI direction)
	{
		var polygon = _directions[direction];
		var label = polygon.GetChild<Label>(0);
		var values = label.Text.Split('/');
		var size = values[1];
		return int.Parse(size);
	}

	public void SetSize(DirectionUI direction, int size)
	{
		var polygon = _directions[direction];
		var label = polygon.GetChild<Label>(0);
		var values = label.Text.Split('/');
		var currentValue = values[0];

		label.Text = $"{currentValue}/{size}";
	}

	public void SetValue(DirectionUI direction, int value)
	{
		var polygon = _directions[direction];
		var label = polygon.GetChild<Label>(0);
		var values = label.Text.Split('/');
		var currentSize = values[1];

		label.Text = $"{value}/{currentSize}";
	}

	public void Reset(DirectionUI direction)
	{
		SetValue(direction, GetSize(direction));
	}

    private void OnDirectionInputEvent(InputEvent @event, DirectionUI direction)
    {
        if (Input.IsActionJustReleased("left-click"))
            OnLeftClick?.Invoke(direction);
        else if (Input.IsActionJustReleased("right-click"))
            OnRightClick?.Invoke(direction);
    }

    private void OnDirectionAreaMouseEnter(DirectionUI direction)
    {
        var polygon = _directions[direction];
        if (!polygon.Visible)
            return;

        polygon.Modulate = new Color("4cff21");
    }

    private void OnDirectionAreaMouseExit(DirectionUI direction)
    {
        var polygon = _directions[direction];
        if (!polygon.Visible)
            return;

        polygon.Modulate = new Color("ffffff");
    }
}