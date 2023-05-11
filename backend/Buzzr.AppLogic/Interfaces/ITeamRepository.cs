using Buzzr.Domain;

namespace Buzzr.AppLogic.Interfaces
{
    public interface ITeamRepository
    {
        Team GetById(Guid id);
        IList<Team> GetAll();
        void Add(Team team);
        void Delete(Guid id);
    }
}
