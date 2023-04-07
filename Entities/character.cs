using Godot;
using My_awesome_character.Core;
using My_awesome_character.Core.Constatns;
using System;

public partial class character : Node2D
{
	private Node2D CharacterNode => GetNode<Node2D>("Character_v"); 

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}

	public override void _UnhandledKeyInput(InputEvent @event)
	{
		var key = @event.AsText();
		GD.Print(key);

		var position = CharacterNode.Position;
		var globalPosition = CharacterNode.GlobalPosition;

		if (key == KeyNames.Left)
		{
			RemoveChild(CharacterNode);
			var newCharacter = SceneFactory.Create<Node2D>("Character_v", ScenePaths.Player_Left);
			newCharacter.Position= position;
			AddChild(newCharacter);
		}
		if (key == KeyNames.Right)
		{
			RemoveChild(CharacterNode);
			var newCharacter = SceneFactory.Create<Node2D>("Character_v", ScenePaths.Player_Right);
			newCharacter.Position = position;
			AddChild(newCharacter, true);
		}


	}
}
