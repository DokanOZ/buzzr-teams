using Buzzr.AppLogic.Interfaces;
using Buzzr.Domain;

namespace Buzzr.Infrastructure.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly Dictionary<Guid, Player> _dictionary;

        public PlayerRepository()
        {
            _dictionary = new Dictionary<Guid, Player>();    
        }

        public void Add(Player player)
        {
            _dictionary.Add(player.Id, player);
        }

        public void Delete(Guid id)
        {
            _dictionary.Remove(id);
        }

        public IList<Player> GetAll()
        {
            var list = _dictionary.Values.ToList();

            return list;
        }

        public Player GetById(Guid id)
        {
            if(_dictionary.TryGetValue(id, out Player player))
            {
                return player;
            }
            throw new Exception("player not found");
        }
    }
}
