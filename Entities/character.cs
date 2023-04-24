using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Linq;

public partial class character : Node2D
{
	private readonly System.Collections.Generic.Dictionary<string, Key[]> _keyToDirectionMap = new System.Collections.Generic.Dictionary<string, Key[]> 
	{
		{ "back-left", new Key[] { Key.Left, Key.Up } },
		{ "back-right", new Key[] { Key.Right, Key.Up } },
		{ "front-left", new Key[] { Key.Left, Key.Down } },
		{ "front-right", new Key[] { Key.Right, Key.Down } },
        { "left", new Key[] { Key.Left } },
        { "right", new Key[] { Key.Right } },
        { "back", new Key[] { Key.Up } },
        { "front", new Key[] { Key.Down } },
    };

	private AnimationPlayer _currentAnimation;
	private string _currentDirection;


	public MapCell MapPosition { get; set; }

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		Move();
	}

	public override void _UnhandledKeyInput(InputEvent @event)
	{
		var keyEvent = @event as InputEventKey;
		if (keyEvent == null)
			return;

		var newDirection = _keyToDirectionMap
			.Where(kv => kv.Value.All(IsKeyPressed))
			.Select(k => k.Key)
			.FirstOrDefault();

		if (newDirection != null && newDirection != _currentDirection)
            ActivateDirection(newDirection);
    }

    private void ActivateDirection(string name)
	{
		foreach(Node2D child in GetChildren().Where(c => c is Node2D))
		{
            child.Visible = child.Name == name;
			if (child.Visible)
			{
                _currentAnimation?.Stop();
                _currentAnimation = child.FindChild("AnimationPlayer") as AnimationPlayer;
				_currentDirection = name;

				GD.Print($"animation found for dir: {name}, animation exists: {_currentAnimation != null}");
            }
        }
    }

	private void Move()
	{
		if (_currentAnimation == null)
			return;

		var allKeyPressed = _keyToDirectionMap[_currentDirection].All(IsKeyPressed);

		if (!allKeyPressed && _currentAnimation.IsPlaying())
			_currentAnimation?.Stop();
		if (allKeyPressed && !_currentAnimation.IsPlaying())
            _currentAnimation?.Play("move");
	}

	private bool IsKeyPressed(Key key) => Input.IsActionPressed("ui_" + key.ToString().ToLower());
}