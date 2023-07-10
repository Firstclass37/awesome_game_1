namespace Game.Server.API.Resources
{
    public interface IResourceController
    {
        int GetAmount(int resourceType);

        IReadOnlyCollection<ResourceInfo> GetList();
    }
}