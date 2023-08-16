using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;
using Game.Server.Storage;

namespace Game.Server.Logic.Maps
{
    internal interface IPlayerGrid
    {
        public IReadOnlyCollection<Coordiante> GetAvailableFor(int playerId);
    }

    internal class PlayerGrid: IPlayerGrid
    {
        private readonly Lazy<Dictionary<int, List<Coordiante>>> _playersCoordinates;

        public PlayerGrid(IStorage storage)
        {
            _playersCoordinates = new Lazy<Dictionary<int, List<Coordiante>>>(
                () => storage.Find<PlayerToPosition>(t => true).GroupBy(pp => pp.PlayerNumber).ToDictionary(p => p.Key, p => p.Select(a => a.Coordinate).ToList()));
        }

        public IReadOnlyCollection<Coordiante> GetAvailableFor(int playerId) =>
            _playersCoordinates.Value.ContainsKey(playerId) ? _playersCoordinates.Value[playerId] : throw new ArgumentOutOfRangeException($"no info about coordinates for player {playerId}");
    }
}