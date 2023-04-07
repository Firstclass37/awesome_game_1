using Godot;
using System;

namespace My_awesome_character.Core
{
    public static class SceneFactory
    {
        public static T Create<T>(string name, string path) where T : class
        {
            var inventoryScene = (PackedScene)ResourceLoader.Load(path);
            if (inventoryScene == null)
                throw new ApplicationException($"Can't find scene \'{path}\'.");

            var scene = inventoryScene.Instantiate();
            scene.Name = name;
            return scene is T 
                ? scene as T 
                : throw new ArgumentException($"can't cast scene ({path}) to type {typeof(T).Name} couse original type is {scene.GetType().Name} ");
        }
    }
}