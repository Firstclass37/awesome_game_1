using Godot;

public partial class character : Node2D
{
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}

	public override void _UnhandledKeyInput(InputEvent @event)
	{
		var keyEvent = @event as InputEventKey;
		if (keyEvent != null)
			return;

		var key = keyEvent.Keycode;
		GD.Print(key);
		if (key == Key.Left)
			ActivateDirection("left");

		if (key == Key.Right)
			ActivateDirection("right");

		if (key == Key.Up)
			ActivateDirection("back");

		if (key == Key.Down)
			ActivateDirection("front");

		

    }

	private void ActivateDirection(string name)
	{
		foreach(Node2D child in GetChildren())
            child.Visible = child.Name == name;
	}
}