using Buzzr.Domain;

namespace Buzzr.AppLogic.Interfaces
{
    public interface IPlayerRepository
    {
        Player GetById(Guid id);
        IList<Player> GetAll();
        void Add(Player player);
        void Delete(Guid id);
    }
}
