using Godot;
using My_awesome_character.Core.Game.Constants;
using System;
using System.Collections.Generic;

namespace My_awesome_character.Core.Helpers
{
    public class ResourcePreviewTextureSelector : ISelector<int, Texture2D>
    {
        private readonly Dictionary<int, Texture2D> _textures = new Dictionary<int, Texture2D>
        {
            { ResourceType.Money, ResourceLoader.Load<Texture2D>("C:\\Projects\\Mine\\My_awesome_character\\Assets\\Resources\\gold_preview.png") },
            { ResourceType.Water, ResourceLoader.Load<Texture2D>("C:\\Projects\\Mine\\My_awesome_character\\Assets\\Resources\\water_drop_preview.png") },
            { ResourceType.Food, ResourceLoader.Load<Texture2D>("C:\\Projects\\Mine\\My_awesome_character\\Assets\\Resources\\food-preview.png") },
            { ResourceType.Electricity, ResourceLoader.Load<Texture2D>("C:\\Projects\\Mine\\My_awesome_character\\Assets\\Resources\\electrosity_preview.png") },
            { ResourceType.Steel, ResourceLoader.Load<Texture2D>("C:\\Projects\\Mine\\My_awesome_character\\Assets\\Resources\\steel_preview.png") },
            { ResourceType.Uranus, ResourceLoader.Load<Texture2D>("C:\\Projects\\Mine\\My_awesome_character\\Assets\\Resources\\uranus_preview.png") },
            { ResourceType.Microchip, ResourceLoader.Load<Texture2D>("C:\\Projects\\Mine\\My_awesome_character\\Assets\\Resources\\microchip_preview.png") },
        };

        private static Texture2D Default => ResourceLoader.Load<Texture2D>("C:\\Projects\\Mine\\My_awesome_character\\Assets\\Map\\Building\\unknown_preview.png");

        public Texture2D Select(int from) =>
             _textures.ContainsKey(from) ? _textures[from] : Default;
    }
}