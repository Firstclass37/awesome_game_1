using Godot;
using System;
using System.Collections.Generic;

namespace My_awesome_character.Core.Helpers
{
    internal class TextureSelectorBase : ISelector<string, Texture2D>
    {
        private readonly Dictionary<string, Texture2D> _textures = new();

        protected void AddTexture(string key, string path)
        {
            _textures.Add(key, ResourceLoader.Load<Texture2D>(path));
        }

        public Texture2D Select(string from)
        {
            return _textures.ContainsKey(from) ? _textures[from] : throw new ArgumentOutOfRangeException($"no texture for {from}");
        }
    }
}