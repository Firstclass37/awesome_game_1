using Godot;
using System;
using System.Linq;

public partial class character : Node2D
{
	private Tween _movingTween;

	public int Id { get; set; }

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

	public bool IsBusy => _movingTween != null && _movingTween.IsRunning();

	public bool IsMoving { get; set; }

    public MapCell MapPosition { get; set; }

	public override void _Ready()
	{
		ActivateDirection("front");
    }

	public override void _Process(double delta)
	{
		MoveAnimation();
	}

	public void MoveTo(MapCell[] path, Func<MapCell, Vector2> positionProvider, Action<MapCell> onPositionChanged, Action onEnd)
	{
		if (_movingTween != null)
			_movingTween.Kill();

        _movingTween = CreateTween();
        for (int i = 0; i < path.Length; i++)
		{
			var current = i == 0 ? MapPosition : path[i - 1];
			var to = path[i];

			var currentPosition = positionProvider(current);
            var targetPosition = positionProvider(to);
            _movingTween.TweenCallback(Callable.From(() => ActivateDirection(SelectDirection(targetPosition - currentPosition, _currentDirection))));
            _movingTween.TweenProperty(this, "position", targetPosition, 1F);
            _movingTween.TweenCallback(Callable.From(() => SetNewPosition(to)));
			if (onPositionChanged != null)
				_movingTween.TweenCallback(Callable.From( () => onPositionChanged(to)));
        }
        _movingTween.TweenCallback(Callable.From(onEnd));

        _movingTween.Play();
	}

	private void SetNewPosition(MapCell newCell)
	{
		MapPosition = newCell;
	}

	private string SelectDirection(Vector2 vector, string currentDirection)
	{
		if (vector == Vector2.Zero)
			return currentDirection;

		if (vector.X == 0 && vector.Y > 0)
			return "front";

		if (vector.X == 0 && vector.Y < 0)
			return "back";

		if (vector.X > 0 && vector.Y == 0)
			return "right";

        if (vector.X < 0 && vector.Y == 0)
            return "left";

		if (vector.X > 0 && vector.Y > 0)
			return "front-right";

        if (vector.X < 0 && vector.Y > 0)
            return "front-left";

        if (vector.X > 0 && vector.Y < 0)
            return "back-right";

        if (vector.X < 0 && vector.Y < 0)
            return "back-left";

		throw new Exception($"cant detect direction for vector {vector}");
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
		if (string.IsNullOrEmpty(name)) 
			throw new ArgumentOutOfRangeException("can't activate empty direction");

		foreach(Node2D child in GetChildren().Where(c => c is Node2D))
		{
            child.Visible = child.Name == name;
			if (child.Visible)
			{
                _currentAnimation?.Stop();
                _currentAnimation = child.FindChild("AnimationPlayer") as AnimationPlayer;
				_currentDirection = name;
            }
        }
    }

	private void MoveAnimation()
	{
        if (_currentAnimation == null)
            return;

        if (!IsMoving && _currentAnimation.IsPlaying())
            _currentAnimation.Stop();
        if (IsMoving && !_currentAnimation.IsPlaying())
            _currentAnimation.Play("move");
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