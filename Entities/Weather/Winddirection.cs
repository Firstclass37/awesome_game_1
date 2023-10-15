using Godot;
using Godot.Collections;
using My_awesome_character.Core.Game.Constants;

public partial class Winddirection : Node2D
{
	private TextureRect Texture => GetNode<TextureRect>("TextureRect");

	private readonly Dictionary<DirectionUI, float> _rotateAngleMapper = new Dictionary<DirectionUI, float>() 
	{
		{ DirectionUI.Top, -30 },
		{ DirectionUI.Bottom, 150 },
		{ DirectionUI.Left, -150 },
		{ DirectionUI.Right, 30 },
	};

	public void Rotate(DirectionUI directionUI)
	{
		Texture.RotationDegrees = _rotateAngleMapper[directionUI];
	}
}