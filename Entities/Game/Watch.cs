using Godot;

public partial class Watch : Node2D
{
	private Label HoursLabel => GetNode<Label>("HBoxContainer/Label");

	
	public int Hours 
	{
		set 
		{
			HoursLabel.Text = value < 10 ? $"0{value}" : value.ToString();
		}
	}
}