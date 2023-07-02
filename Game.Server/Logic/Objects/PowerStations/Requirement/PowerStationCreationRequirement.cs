using Game.Server.Logic.Objects._Requirements;
using Game.Server.Storage;

namespace Game.Server.Logic.Objects.PowerStations.Requirement
{
    internal class PowerStationCreationRequirement : OnlyGroundRequirement
    {
        public PowerStationCreationRequirement(IStorage storage) : base(storage)
        {
        }
    }
}