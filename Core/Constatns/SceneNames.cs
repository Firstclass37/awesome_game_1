﻿using System;

namespace My_awesome_character.Core.Constatns
{
    internal class SceneNames
    {
        public const string Map = "map";
        public static string Character(int id) => $"character_{id}";
        public const string Game = "Game";
        public static string ResourceInfo(int resourceId) => $"resource_info_{resourceId}";
        public static string ResourceCost(string objectType, int resourceId) => $"resource_cost_{objectType}_{resourceId}";
        public static string HomeFactory(int id) => $"Building_home_{id}";




        public static string Builidng_preview(Type type) => $"building_preview_{type.Name}";
        public static string BuidlingPreviewInfo(string buildingType) => $"buidlig_preview_info_{buildingType}";

        public static string TrafficLight(int id) => $"traffic_light_{id}";




        public static string LoadingBar(int id) => $"loading_bar_{id}";
    }
}