using Buzzr.AppLogic.Interfaces;
using Buzzr.Domain;

namespace Buzzr.Infrastructure.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly Dictionary<Guid, Team> _dictionary;

        public TeamRepository() 
        {
            _dictionary = new();
        }
        public void Add(Team team)
        {
            _dictionary.Add(team.Id, team);
        }

        public void Delete(Guid id)
        {
            _dictionary.Remove(id);
        }

        public IList<Team> GetAll()
        {
            var list = _dictionary.Values.ToList();
            return list;
        }

        public Team GetById(Guid id)
        {
            if (_dictionary.TryGetValue(id, out Team team))
            {
                return team;
            }
            throw new Exception("team not found");
        }
    }
}
