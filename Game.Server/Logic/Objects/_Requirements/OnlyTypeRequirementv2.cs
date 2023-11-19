using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects._Requirements
{
    internal class OnlyTypeRequirementv2 : ICreationRequirement
    {
        private readonly string _mustBe;
        private readonly string _canBe;

        public OnlyTypeRequirementv2(string mustBe, string canBe)
        {
            _mustBe = mustBe;
            _canBe = canBe;
        }

        public bool Satisfy(Coordiante coordiante, Dictionary<Coordiante, GameObjectAggregator> area)
        {
            var mustBe = area.Values.Where(o => o.GameObject.ObjectType == _mustBe).ToArray();
            if (!mustBe.Any())
                return false;

            return area.Values.Except(mustBe).All(o => o.GameObject.ObjectType == _canBe);
        }
    }
}
