using Game.Server.Logic.Objects._Requirements;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;
using Game.Server.Storage;

namespace Game.Server.Logic.Objects.Gorund.Requirements
{
    internal class GroundRequirement : ICreationRequirement
    {
        private readonly IStorage _storage;

        public GroundRequirement(IStorage storage)
        {
            _storage = storage;
        }

        public bool Satisfy(Coordiante[] area)
        {
            var anyObjectsExist = _storage.Find<GameObjectPosition>(p => p.Coordiante.Equals(area.First())).Any();
            return !anyObjectsExist;
        }
    }
}