using Buzzr.AppLogic.Interfaces;
using Buzzr.Domain;
using System.Collections.Generic;

namespace Buzzr.AppLogic
{
    public class Service : IService
    {
        private IPlayerRepository _playerRepository;
        private ITeamRepository _teamRepository;
        private IList<Player> _buzzedPlayers;

        public Service(IPlayerRepository playerRepository, ITeamRepository teamRepository)
        {
            _playerRepository = playerRepository;
            _teamRepository = teamRepository;
            _buzzedPlayers = new List<Player>();
        }
        public void AddPoints(Guid teamId, int points)
        {
            _teamRepository.GetById(teamId).AddPoints(points);
        }

        public void Buzz(Guid playerId, TimeOnly buzzTime)
        {
            var player = _playerRepository.GetById(playerId);
            player.Buzzed(buzzTime.Hour, buzzTime.Minute, buzzTime.Second, buzzTime.Millisecond);

            _buzzedPlayers.Add(player);
            _buzzedPlayers.OrderBy(t => t.BuzzedTime);
        }

        public void BuzzBan(Guid playerId)
        {
            _playerRepository.GetById(playerId).BuzzBan(false);
        }

        public Player CreatePlayer(string name, Guid teamId)
        {
            Player newPlayer = new Player(name, teamId);
            _teamRepository.GetById(teamId).AddPlayer(newPlayer);
            return newPlayer;

        }

        public Team CreateTeam(string name)
        {
            Team newTeam = new Team(name);
            _teamRepository.Add(newTeam); 
            return newTeam;
        }

        public IList<Player> GetBuzzedList()
        {
            return _buzzedPlayers;
        }

        public Player GetPlayer(Guid playerId)
        {
            return _playerRepository.GetById(playerId);
        }

        public IList<Player> GetPlayers()
        {
            return _playerRepository.GetAll();
        }

        public Player GetRandomPlayer()
        {
            Random rnd = new Random();
            int r = rnd.Next(_buzzedPlayers.Count);
            return _buzzedPlayers[r];
        }

        public Team GetTeam(Guid Id)
        {
            return _teamRepository.GetById(Id);
        }

        public IList<Team> GetTeams()
        {
            return _teamRepository.GetAll();
        }

        public void RemovePlayer(Guid playerId)
        {
            var player = _playerRepository.GetById(playerId);

            var team = _teamRepository.GetById(player.TeamId);

            team.RemovePlayer(player);
            _playerRepository.Delete(playerId);

            
        }

        public void RemovePoints(Guid teamId, int points)
        {
            _teamRepository.GetById(teamId).RemovePoints(points);
        }

        public void RemoveTeam(Guid teamId)
        {
            _teamRepository.Delete(teamId);
        }

        public void ResetBuzzBan()
        {
            var list = _playerRepository.GetAll();

            foreach(var player in list)
            {
                player.BuzzBan(true);
            }
        }

        public void ResetBuzzer()
        {
            var list = _playerRepository.GetAll();

            foreach (var player in list)
            {
                player.ResetBuzzer();
            }

            _buzzedPlayers.Clear();
        }
    }
}
