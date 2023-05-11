using Buzzr.AppLogic.Interfaces;
using Buzzr.Domain;

namespace Buzzr.Infrastructure.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly Dictionary<Guid, Team> _dictionary;

        public TeamRepository() 
        {
            _dictionary = new Dictionary<Guid, Team>();
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
            IList<Team> teams = _dictionary.Values
                .ToList();
            return teams;
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
