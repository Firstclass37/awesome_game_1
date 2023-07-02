using Game.Server.Logic.Objects._Requirements;
using Game.Server.Storage;

namespace Game.Server.Logic.Objects.Home.Requirement
{
    internal class HomeCreationRequirement : OnlyGroundRequirement
    {
        public HomeCreationRequirement(IStorage storage): base(storage)
        {
        }
    }
}