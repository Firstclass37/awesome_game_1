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


	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}

	public override void _UnhandledKeyInput(InputEvent @event)
	{
		var keyEvent = @event as InputEventKey;
		if (keyEvent == null)
			return;

		var newDirection = _keyToDirectionMap
			.Where(kv => kv.Value.All(k => Input.IsActionPressed("ui_" + k.ToString().ToLower())))
			.Select(k => k.Key)
			.FirstOrDefault();

		if (newDirection != null)
			ActivateDirection(newDirection);
    }

	private void ActivateDirection(string name)
	{
		foreach(Node2D child in GetChildren().Where(c => c is Node2D))
            child.Visible = child.Name == name;
	}
}