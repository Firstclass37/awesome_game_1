using Godot;
using My_awesome_character.Core.Game;
using My_awesome_character.Core.Game.Constants;
using System.Collections.Generic;
using System.Linq;

public partial class TrafficLight : Node2D
{
	private readonly Dictionary<Direction, Polygon2D> _directions = new();

	public override void _Ready()
	{
		_directions.Add(Direction.Left, GetNode<Polygon2D>("TextureRect/Left_polygon"));
		_directions.Add(Direction.Top, GetNode<Polygon2D>("TextureRect/Top_polygon"));
		_directions.Add(Direction.Right, GetNode<Polygon2D>("TextureRect/Right_polygon"));
		_directions.Add(Direction.Bottom, GetNode<Polygon2D>("TextureRect/Bottom_polygon"));

		foreach (var node in _directions.Values)
			node.Visible = false;
	}

	public override void _Process(double delta)
	{
	}

	public Coordiante MapPosition { get; set; }

	public Direction CurrentDirection { get; private set; }

	public Direction[] GetActiveDirections() => _directions.Where(d => d.Value.Visible).Select(d => d.Key).ToArray();

	public void NextDirection()
	{
		var array = _directions.Keys.ToList();
		var currentIndex = array.IndexOf(CurrentDirection);
		CurrentDirection = currentIndex == array.Count - 1 ? array[0] : array[currentIndex + 1];
	}

	public void ActivateDirection(Direction direction)
	{
		_directions[direction].Visible = true;
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

		label.Text = $"{currentValue} / {size}";
	}

	public void SetValue(Direction direction, int value)
	{
		var polygon = _directions[direction];
		var label = polygon.GetChild<Label>(0);
		var values = label.Text.Split('/');
		var currentSize = values[1];

		label.Text = $"{value} / {currentSize}";
	}
}
