﻿using Godot;
using My_awesome_character.Core.Constatns;
using System.Collections.Generic;

namespace My_awesome_character.Core.Helpers
{
    internal class BuildingPreviewInfoSelector : ISelector<string, Texture2D>
    {
        private readonly Dictionary<string, Texture2D> _textures = new Dictionary<string, Texture2D>
        {
            { BuildingTypesTrue.Home, ResourceLoader.Load<Texture2D>("C:\\Projects\\Mine\\My_awesome_character\\Assets\\Map\\Building\\home_test_2x2.png") },
            { BuildingTypesTrue.UranusMine, ResourceLoader.Load<Texture2D>("C:\\Projects\\Mine\\My_awesome_character\\Assets\\Map\\Building\\Mine.png") },
            { BuildingTypesTrue.Road, ResourceLoader.Load<Texture2D>("C:\\Projects\\Mine\\My_awesome_character\\Assets\\Map\\Ground\\road_asphalt_pewviewinfo.png") },
            { BuildingTypesTrue.PowerStation, ResourceLoader.Load<Texture2D>("C:\\Projects\\Mine\\My_awesome_character\\Assets\\Map\\Building\\PowerStation.png") },
            { BuildingTypesTrue.SolarBattery, ResourceLoader.Load<Texture2D>("C:\\Projects\\Mine\\My_awesome_character\\Assets\\Map\\Building\\solar-batary.png") },
            { BuildingTypesTrue.WindTurbine, ResourceLoader.Load<Texture2D>("C:\\Projects\\Mine\\My_awesome_character\\Assets\\Map\\Building\\wind-turbine.png")}
        };

        private static Texture2D Default => ResourceLoader.Load<Texture2D>("C:\\Projects\\Mine\\My_awesome_character\\Assets\\Map\\Building\\unknown_preview.png");

        public Texture2D Select(string from) =>
             _textures.ContainsKey(from) ? _textures[from] : Default;
    }
}