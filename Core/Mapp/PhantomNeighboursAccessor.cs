using Game.Server.Map;
using Game.Server.Models.Constants;
using Game.Server.Models.Maps;
using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Ui;
using System;
using System.Collections.Generic;

namespace My_awesome_character.Core.Mapp
{
    internal class PhantomNeighboursAccessor : IPhantomNeighboursAccessor
    {
        private readonly Lazy<Map> _map;

        public PhantomNeighboursAccessor(ISceneAccessor sceneAccessor)
        {
            _map = new Lazy<Map>(() => sceneAccessor.GetScene<Map>(SceneNames.Map));
        }

        public IReadOnlyDictionary<Coordiante, Direction> GetNeightborsOf(Coordiante coordiante)
        {
            return _map.Value.GetNeightborsOf(coordiante);
        }
    }
}