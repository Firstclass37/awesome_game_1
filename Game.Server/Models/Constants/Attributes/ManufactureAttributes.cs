using Game.Server.Models.GameObjects;
using Game.Server.Models.Resources;

namespace Game.Server.Models.Constants.Attributes
{
    internal class ManufactureAttributesTypes
    {
        public const string LastProduceTime = "Manufacture.LastProduceTime";
        public const string ProduceQueueSize = "Manufacture.ProduceQueueSize";
        public const string RequriedResources = "Manufacture.RequriedResources";
        public const string ResultResources = "Manufacture.ResultResources";
        public const string Requirements = "Manufacture.Requirements";



        public void Test()
        {
            var testType = TypeType<IProduceRequirement>.Create<TestRequirement>();

            var a = (TypeType<IProduceRequirement>)testType;
        }
    }

    internal class ManufactureAttributes
    {
        public static GameObjectAttribute<double> LastProduceTime => new GameObjectAttribute<double>(ManufactureAttributesTypes.LastProduceTime);
        public static GameObjectAttribute<int> ProduceQueueSize => new GameObjectAttribute<int>(ManufactureAttributesTypes.ProduceQueueSize);
        public static GameObjectAttribute<ResourceChunk[]> RequriedResources = new GameObjectAttribute<ResourceChunk[]>(ManufactureAttributesTypes.RequriedResources);
        public static GameObjectAttribute<ResourceChunk[]> ResultResources = new GameObjectAttribute<ResourceChunk[]>(ManufactureAttributesTypes.ResultResources);
        public static GameObjectAttribute<TypeType<IProduceRequirement>[]> Requirements = new GameObjectAttribute<TypeType<IProduceRequirement>[]>(ManufactureAttributesTypes.Requirements);
    }



    internal class TypeType<TType>
    {
        public static TypeType<TType> Create<TOriginalType>() where TOriginalType: TType => new TypeType<TType>(nameof(TOriginalType));

        private TypeType(string typeName)
        {
            TypeName = typeName;
        }

        public string TypeName { get; }
    }

    internal interface IProduceRequirement
    {
        bool Can();
    }


    internal class TestRequirement : IProduceRequirement
    {
        public bool Can()
        {
            throw new NotImplementedException();
        }
    }
}