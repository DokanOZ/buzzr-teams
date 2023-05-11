using Buzzr.API.models;
using Buzzr.API.Models;
using Buzzr.AppLogic;
using Buzzr.AppLogic.Interfaces;
using Buzzr.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Buzzr.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly IService _service;

        public TeamController(IService service)
        {
            _service = service;
        }

        [HttpPost("add")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult AddTeam([FromBody] CreateTeamModel model)
        {
            try
            {
                var team = _service.CreateTeam(model.Name);

                if (team == null)
                {
                    return BadRequest("Could not create team");
                }
                return Ok(team.Id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("remove")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult RemoveTeam([FromBody] Guid teamId)
        {
            try
            {
                var team = _service.GetTeam(teamId);

                if (team == null)
                {
                    return BadRequest("Could not find team");
                }


                _service.RemoveTeam(teamId);

                return Ok("Team removed");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("addPoints")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult AddPoints([FromBody] PointsModel model)
        {
            try
            {
                var team = _service.GetTeam(model.TeamId);

                if (team == null)
                {
                    return BadRequest("Could not find team");
                }

                _service.AddPoints(model.TeamId, model.Points);

                return Ok("points added");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("removePoints")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult RemovePoints([FromBody] PointsModel model)
        {
            try
            {
                var team = _service.GetTeam(model.TeamId);

                if (team == null)
                {
                    return BadRequest("Could not find team");
                }

                _service.RemovePoints(model.TeamId, model.Points);

                return Ok("points removed");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{teamId:Guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetOneTeam([FromRoute] Guid teamId)
        {
            try
            {
                var team = _service.GetTeam(teamId);

                TeamModel teamModel = new TeamModel();
                IList<PlayerModel> playerModelList = new List<PlayerModel>();

                if (team == null)
                {
                    return NotFound("No team found");
                }

                teamModel.Name = team.Name;
                teamModel.Id = teamId;
                teamModel.Points = team.Points;

                foreach (var player in team.players)
                {
                    PlayerModel model = new PlayerModel();
                    model.Name = player.Name;
                    model.Id = player.Id;
                    model.teamId = player.TeamId;

                    playerModelList.Add(model);
                }

                teamModel.Players = playerModelList;

                return Ok(teamModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("getAll")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetAllTeams()
        {
            try
            {
                IList<Team> teams = _service.GetTeams();
                

                if (teams == null || teams.Count <= 0)
                {
                    return NotFound("No teams found");
                }

                return Ok(teams);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
