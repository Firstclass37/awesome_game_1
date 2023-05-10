using Godot;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.System;
using My_awesome_character.Core.Ui;
using My_awesome_character.Entities;
using System.Linq;

namespace My_awesome_character.Core.Systems
{
    internal class HomeCreatingSystem : ISystem
    {
        private readonly ISceneAccessor _sceneAccessor;

        public HomeCreatingSystem(ISceneAccessor sceneAccessor)
        {
            _sceneAccessor = sceneAccessor;
        }

        public void OnStart()
        {
            _sceneAccessor.GetScene<Map>(SceneNames.Map).OnCellClicked += HomeCreatingSystem_OnCellClicked;
        }

        public void Process(double gameTime)
        {
        }

        private void HomeCreatingSystem_OnCellClicked(MapCell obj)
        {
            var otherHomes = _sceneAccessor.FindAll<Home>();
            var newHomeId = otherHomes.Any() ? otherHomes.Max(h => h.Id) + 1 : 1; 

            var home = SceneFactory.Create<Home>(SceneNames.HomeFactory(newHomeId), ScenePaths.HomeFactory);
            home.Id = newHomeId;
            home.SpawnCell = obj;
            home.LastFireTime = SystemNode.GameTime;

            var game = _sceneAccessor.GetScene<Node2D>(SceneNames.Game);
            game.AddChild(home);

            Godot.GD.Print($"home created on: {obj}");
        }
    }
}