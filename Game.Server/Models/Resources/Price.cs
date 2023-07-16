namespace Game.Server.Models.Resources
{
    internal class Price
    {
        public static Price Free => new Price();

        public static Price Create(params ResourceChunk[] resourceChunks) => new Price(resourceChunks);

        public Price(params ResourceChunk[] resourceChunks)
        {
            Chunks = resourceChunks;
        }

        public IReadOnlyCollection<ResourceChunk> Chunks { get; }
    }
}