namespace Game.Server.API.Resources
{
    public interface IResourceController
    {
        IReadOnlyCollection<ResourceInfo> GetList();
    }
}