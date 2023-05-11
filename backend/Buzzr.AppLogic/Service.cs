using Buzzr.AppLogic.Interfaces;
using Buzzr.Domain;
using System.Collections.Generic;

namespace Buzzr.AppLogic
{
    public class Service : IService
    {
        private IPlayerRepository _playerRepo;
        private ITeamRepository _teamRepo;
        private IList<Player> _buzzedPlayers;

        public Service(IPlayerRepository playerRepo, ITeamRepository teamRepo)
        {
            _playerRepo = playerRepo;
            _teamRepo = teamRepo;
            _buzzedPlayers = new List<Player>();
        }
        public void AddPoints(Guid teamId, int points)
        {
            _teamRepo.GetById(teamId).AddPoints(points);
        }

        public void Buzz(Guid playerId, TimeOnly buzzTime)
        {
            var player = _playerRepo.GetById(playerId);
            player.Buzzed(buzzTime.Hour, buzzTime.Minute, buzzTime.Second, buzzTime.Millisecond);

            _buzzedPlayers.Add(player);
            _buzzedPlayers.OrderBy(t => t.BuzzedTime);
        }

        public void BuzzBan(Guid playerId)
        {
            _playerRepo.GetById(playerId).BuzzBan(false);
        }

        public Player CreatePlayer(string name, Guid teamId)
        {
            Player newPlayer = new Player(name, teamId);
            _teamRepo.GetById(teamId).AddPlayer(newPlayer);
            return newPlayer;

        }

        public Team CreateTeam(string name)
        {
            Team newTeam = new Team(name);
            _teamRepo.Add(newTeam); 
            return newTeam;
        }

        public IList<Player> GetBuzzedList()
        {
            return _buzzedPlayers;
        }

        public Player GetPlayer(Guid playerId)
        {
            return _playerRepo.GetById(playerId);
        }

        public IList<Player> GetPlayers()
        {
            return _playerRepo.GetAll();
        }

        public Player GetRandomPlayer()
        {
            Random rnd = new Random();
            int r = rnd.Next(_buzzedPlayers.Count);
            return _buzzedPlayers[r];
        }

        public Team GetTeam(Guid Id)
        {
            return _teamRepo.GetById(Id);
        }

        public IList<Team> GetTeams()
        {
            return _teamRepo.GetAll();
        }

        public void RemovePlayer(Guid playerId)
        {
            var player = _playerRepo.GetById(playerId);

            var team = _teamRepo.GetById(player.TeamId);

            team.RemovePlayer(player);
            _playerRepo.Delete(playerId);

            
        }

        public void RemovePoints(Guid teamId, int points)
        {
            _teamRepo.GetById(teamId).RemovePoints(points);
        }

        public void RemoveTeam(Guid teamId)
        {
            _teamRepo.Delete(teamId);
        }

        public void ResetBuzzBan()
        {
            var list = _playerRepo.GetAll();

            foreach(var player in list)
            {
                player.BuzzBan(true);
            }
        }

        public void ResetBuzzer()
        {
            var list = _playerRepo.GetAll();

            foreach (var player in list)
            {
                player.ResetBuzzer();
            }

            _buzzedPlayers.Clear();
        }
    }
}
