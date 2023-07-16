namespace Game.Server.Models.Resources
{
    internal class ResourceChunk
    {
        public ResourceChunk(int resourceId, float amout)
        {
            ResourceId = resourceId;
            Amout = amout;
        }

        public int ResourceId { get; init; }
        
        public float Amout { get; init; }
    }
}