using Game.Server.Events.Core;
using Game.Server.Logic.Systems;
using Game.Server.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Server.Logic.Objects.Weather.Time
{
    internal class TimeOfSaySystem : ISystem
    {
        private readonly IStorage _storage;

        public TimeOfSaySystem(IStorage storage)
        {
            _storage = storage;
        }

        public void Process(double gameTimeSeconds)
        {
            var game = _storage.First<Models.Game>();
        }
    }
}