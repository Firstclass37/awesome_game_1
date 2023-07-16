using Game.Server.Models.Constants;

namespace Game.Server.Logic.Objects._Requirements
{
    internal class OnlyGroundRequirement : OnlyTypeRequirement
    {
        public OnlyGroundRequirement() : base(BuildingTypes.Ground)
        {
        }
    }
}