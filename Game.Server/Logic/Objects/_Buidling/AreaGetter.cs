﻿using Game.Server.Logic.Maps;
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

        public IAreaGetter Get1x1() => new AreaGetter();

        public IAreaGetter Get2x2() => new Area2x2Getter(_areaCalculator);

        public IAreaGetter Get3x3() => new Area3x3Getter(_areaCalculator);
    }

    internal interface IArea3x3Getter: IAreaGetter { }

    internal class Area3x3Getter : IArea3x3Getter
    {
        private readonly IAreaCalculator _areaCalculator;

        public Area3x3Getter(IAreaCalculator areaCalculator)
        {
            _areaCalculator = areaCalculator;
        }

        public Coordiante[] GetArea(Coordiante root) => _areaCalculator.GetArea(root, AreaSize.Area1x1);
    }

    internal interface IArea2x2Getter : IAreaGetter { }

    internal class Area2x2Getter : IArea2x2Getter
    {
        private readonly IAreaCalculator _areaCalculator;

        public Area2x2Getter(IAreaCalculator areaCalculator)
        {
            _areaCalculator = areaCalculator;
        }

        public Coordiante[] GetArea(Coordiante root) => _areaCalculator.GetArea(root, AreaSize.Area2x2);
    }

    internal interface IArea1x1Getter : IAreaGetter { }

    internal class AreaGetter : IArea1x1Getter
    {
        public Coordiante[] GetArea(Coordiante root) => new Coordiante[] { root };
    }
}