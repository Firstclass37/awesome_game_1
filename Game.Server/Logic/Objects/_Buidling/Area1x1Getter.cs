using Game.Server.Logic.Maps;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects._Buidling
{
    internal interface IDefaultAreaGetterFactory
    {
        IAreaGetter Get1x1();
        IAreaGetter Get2x2();
        IAreaGetter Get3x3();
    }

    internal class DefaultAreaGetterFactory : IDefaultAreaGetterFactory
    {
        private readonly IAreaCalculator _areaCalculator;

        public DefaultAreaGetterFactory(IAreaCalculator areaCalculator)
        {
            _areaCalculator = areaCalculator;
        }

        public IAreaGetter Get1x1() => new Area1x1Getter();

        public IAreaGetter Get2x2() => new Area2x2Getter(_areaCalculator);

        public IAreaGetter Get3x3() => new Area3x3Getter(_areaCalculator);
    }

    internal class Area3x3Getter : IAreaGetter
    {
        private readonly IAreaCalculator _areaCalculator;

        public Area3x3Getter(IAreaCalculator areaCalculator)
        {
            _areaCalculator = areaCalculator;
        }

        public Coordiante[] GetArea(Coordiante root) => _areaCalculator.Get3X3Area(root);
    }

    internal class Area2x2Getter : IAreaGetter
    {
        private readonly IAreaCalculator _areaCalculator;

        public Area2x2Getter(IAreaCalculator areaCalculator)
        {
            _areaCalculator = areaCalculator;
        }

        public Coordiante[] GetArea(Coordiante root) => _areaCalculator.Get2x2Area(root);
    }

    internal class Area1x1Getter : IAreaGetter
    {
        public Coordiante[] GetArea(Coordiante root) => new Coordiante[] { root
    }
}