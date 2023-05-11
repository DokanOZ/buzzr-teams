using Buzzr.Domain;

namespace Buzzr.AppLogic.Interfaces
{
    public interface IService
    {
        // Teams
        public Team CreateTeam(string name);
        public IList<Team> GetTeams();
        public Team GetTeam(Guid teamId);
        public void RemoveTeam(Guid teamId);
        public void AddPoints(Guid teamId, int points);
        public void RemovePoints(Guid teamId, int points);
        
        //Players
        public Player CreatePlayer(string name, Guid teamId);
        public IList<Player> GetPlayers();
        public Player GetPlayer(Guid playerId);
        public void RemovePlayer(Guid playerId);
        public void Buzz(Guid playerId, TimeOnly buzzTime);
        public void ResetBuzzBan();
        public void ResetBuzzer();
        public IList<Player> GetBuzzedList();
        public Player GetRandomPlayer();

        public void BuzzBan(Guid playerId);
    }
}
;