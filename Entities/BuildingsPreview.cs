using Godot;
using My_awesome_character.Helpers;
using System;

public partial class BuildingsPreview : ColorRect
{
	private HBoxContainer PriceContainer => GetNode<HBoxContainer>("HBoxContainer2");
	private TextureRect Texture => GetNode<TextureRect>("TextureRect");
	private Label Label => GetNode<Label>("Label");

	private ColorRect BlockingRect => GetNode<ColorRect>("BlockRect");

	public Texture2D BuildingTexture { set { Texture.Texture = value; } }

	public string BuildingType { get; set; }

	public string Description { set { Label.Text = value; } }

	public bool IsSelected { get; set; }

	public bool Availabe 
	{ 
		get { return !BlockingRect.Visible; } 
		set { BlockingRect.Visible = !value; } 
	}

	public bool Hovered 
	{ 
		set 
		{ 
			Color = value ? new Color("7d4082", 183) : new Color("4e624c", 183); 
		} 
	}

    public override void _Ready()
    {
		MouseEntered += () => OnMouseEnter?.Invoke(this);
		MouseExited += () => OnMouseLeave?.Invoke(this);
    }

    public event Action<BuildingsPreview> OnMouseEnter;

	public event Action<BuildingsPreview> OnMouseLeave;

    public event Action<BuildingsPreview> OnClick;

    public void AddPrices(params Resource[] resource) 
	{
        foreach(var child in PriceContainer.GetChildren())
			PriceContainer.RemoveChild(child);

        foreach (var resourceItem in resource)
			PriceContainer.AddChild(resourceItem);
	}

    public override void _GuiInput(InputEvent @event)
    {
        if (@event.IsLeftClick())
            OnClick?.Invoke(this);
    }
}