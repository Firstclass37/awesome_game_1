using Game.Server.DataBuilding;
using Game.Server.Models.Constants.Attributes;
using Game.Server.Models.Resources;

namespace Game.Server.Logic._Extentions
{
    internal class ManufactoringArgs
    {
        public ManufactoringArgs()
        {
            BaseOnQueue = true;
            Requirements = new TypeInfo<IProduceRequirement>[0];
            RequriedResources = new ResourceChunk[0];
            ResultResources = new ResourceChunk[0];
        }

        public bool BaseOnQueue { get; init; }
        public double PrduceSpeedSeconds { get; init; }
        public ResourceChunk[] RequriedResources { get; init; }
        public ResourceChunk[] ResultResources { get; init; }
        public TypeInfo<IProduceAction> ProduceAction { get; init; }
        public TypeInfo<IProduceRequirement>[] Requirements { get; init; }
    }

    internal static class GameObjectAgregatorBuilderManufactoringExtentions
    {
        public static GameObjectAggregatorBuilder AsManufactoring(this GameObjectAggregatorBuilder gameObjectAggregatorBuilder, ManufactoringArgs args)
        {
            gameObjectAggregatorBuilder.AddAttribute(ManufactureAttributes.Working, false);
            gameObjectAggregatorBuilder.AddAttribute(ManufactureAttributes.LastProduceTime, 0);
            gameObjectAggregatorBuilder.AddAttribute(ManufactureAttributes.ProduceQueueSize, args.BaseOnQueue ? 0 : -1);

            gameObjectAggregatorBuilder.AddAttribute(ManufactureAttributes.PrduceSpeedSeconds, args.PrduceSpeedSeconds);
            gameObjectAggregatorBuilder.AddAttribute(ManufactureAttributes.RequriedResources, args.RequriedResources);
            gameObjectAggregatorBuilder.AddAttribute(ManufactureAttributes.ResultResources, args.ResultResources);
            gameObjectAggregatorBuilder.AddAttribute(ManufactureAttributes.ProduceAction, args.ProduceAction);
            gameObjectAggregatorBuilder.AddAttribute(ManufactureAttributes.Requirements, args.Requirements);

            return gameObjectAggregatorBuilder;
        }
    }
}