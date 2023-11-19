using Game.Server.Models.GameObjects;
using Game.Server.Models.Resources;

namespace Game.Server.Models.Constants.Attributes
{
    internal class ManufactureAttributesTypes
    {
        public const string ProduceSpeedSeconds = "Manufacture.ProduceSpeedSeconds";
        public const string LastProduceTime = "Manufacture.LastProduceTime";
        public const string ProduceQueueSize = "Manufacture.ProduceQueueSize";
        public const string RequriedResources = "Manufacture.RequriedResources";
        public const string ResultResources = "Manufacture.ResultResources";
        public const string Requirements = "Manufacture.Requirements";
        public const string ProduceAction = "Manufacture.ProduceAction";
        public const string Working = "Manufacture.Working";
    }

    internal class ManufactureAttributes
    {
        public static GameObjectAttribute<bool> Working => new GameObjectAttribute<bool>(ManufactureAttributesTypes.Working);
        public static GameObjectAttribute<double> PrduceSpeedSeconds => new GameObjectAttribute<double>(ManufactureAttributesTypes.ProduceSpeedSeconds);
        public static GameObjectAttribute<double> LastProduceTime => new GameObjectAttribute<double>(ManufactureAttributesTypes.LastProduceTime);
        public static GameObjectAttribute<int> ProduceQueueSize => new GameObjectAttribute<int>(ManufactureAttributesTypes.ProduceQueueSize);
        public static GameObjectAttribute<ResourceChunk[]> RequriedResources = new GameObjectAttribute<ResourceChunk[]>(ManufactureAttributesTypes.RequriedResources);
        public static GameObjectAttribute<ResourceChunk[]> ResultResources = new GameObjectAttribute<ResourceChunk[]>(ManufactureAttributesTypes.ResultResources);
        public static GameObjectAttribute<TypeInfo<IProduceAction>> ProduceAction = new GameObjectAttribute<TypeInfo<IProduceAction>>(ManufactureAttributesTypes.ProduceAction);
        public static GameObjectAttribute<TypeInfo<IProduceRequirement>[]> Requirements = new GameObjectAttribute<TypeInfo<IProduceRequirement>[]>(ManufactureAttributesTypes.Requirements);
    }

    internal static class TypeInfoFactory
    {
        public static TypeInfo<TTypeInfo> Create<TTypeInfo, TOriginalType>() where TOriginalType : TTypeInfo => new TypeInfo<TTypeInfo>(typeof(TOriginalType).FullName);
    }

    internal class TypeInfo<TType>
    {
        public TypeInfo(string typeName)
        {
            TypeName = typeName;
        }

        public string TypeName { get; }
    }

    internal interface IProduceRequirement
    {
        bool Can(GameObjectAggregator gameObjectAggregator);
    }

    internal interface IProduceAction
    {
        bool Produce(GameObjectAggregator gameObjectAggregator);
    }
}