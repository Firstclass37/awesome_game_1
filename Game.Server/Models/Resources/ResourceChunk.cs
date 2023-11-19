namespace Game.Server.Models.Resources
{
    internal class ResourceChunk
    {
        public static ResourceChunk Create(int resourceId, float amout) => new ResourceChunk(resourceId, amout);

        public ResourceChunk(int resourceId, float amout)
        {
            ResourceId = resourceId;
            Amout = amout;
        }

        public int ResourceId { get; init; }
        
        public float Amout { get; init; }
    }
}