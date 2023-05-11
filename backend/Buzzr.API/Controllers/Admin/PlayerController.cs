using Buzzr.API.models;
using Buzzr.API.Models;
using Buzzr.AppLogic.Interfaces;
using Buzzr.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Buzzr.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IService _service;

        public PlayerController(IService service)
        {
            _service = service;
        }

        [HttpPost("add")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult AddPlayer([FromBody] CreatePlayerModel model)
        {
            try
            {
                var player = _service.CreatePlayer(model.Name, model.TeamId);

                if (player == null)
                {
                    return BadRequest("Could not create player");
                }
                return Ok(player.Id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("remove")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult RemovePlayer([FromBody] Guid playerId)
        {
            try
            {
                var player = _service.GetPlayer(playerId);

                if (player == null)
                {
                    return BadRequest("Could not find player");
                }

                _service.RemovePlayer(playerId);

                return Ok("Player removed");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("ban")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult BanPlayer([FromBody] Guid playerId)
        {
            try
            {
                var player = _service.GetPlayer(playerId);

                if (player == null)
                {
                    return BadRequest("Could not find player");
                }

                _service.BuzzBan(playerId);

                return Ok("Player banned");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("unbannAll")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult UnBanAll()
        {
            try
            {
                _service.ResetBuzzBan();

                return Ok("Players unBanned");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("resetBuzzer")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult ResetBuzzer()
        {
            try
            {
                _service.ResetBuzzer();

                return Ok("Buzzer resetted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("getAllBuzzed")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetAllBuzzedPlayers()
        {
            try
            {
                var players = _service.GetBuzzedList();

                IList<PlayerModel> playerModelList = new List<PlayerModel>();

                if (players == null || players.Count <= 0)
                {
                    return NotFound("No players found");
                }

                foreach (var player in players)
                {
                    PlayerModel model = new PlayerModel();
                    model.Name = player.Name;
                    model.Id = player.Id;
                    model.teamId = player.TeamId;

                    playerModelList.Add(model);
                }

                return Ok(playerModelList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("getRandomBuzzed")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetRandomBuzzedPlayer()
        {
            try
            {
                var player = _service.GetRandomPlayer();

                if (player == null)
                {
                    return NotFound("No player found");
                }

                PlayerModel playerModel = new PlayerModel();

                playerModel.Id = player.Id;
                playerModel.Name = player.Name;
                playerModel.teamId = player.TeamId;

                return Ok(playerModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("{playerId:Guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetOnePlayer([FromRoute] Guid playerId)
        {
            try
            {
                var player = _service.GetPlayer(playerId);

                if (player == null)
                {
                    return NotFound("No player found");
                }

                PlayerModel playerModel = new PlayerModel();

                playerModel.Id = playerId;
                playerModel.Name = player.Name;
                playerModel.teamId = player.TeamId;

                return Ok(playerModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("getAll")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetAllPlayers()
        {
            try
            {
                var players = _service.GetPlayers();

                IList<PlayerModel> playerModelList = new List<PlayerModel>();

                if (players == null || players.Count <= 0)
                {
                    return NotFound("No players found");
                }

                foreach(var player in players)
                {
                    PlayerModel model = new PlayerModel();
                    model.Name = player.Name;
                    model.Id = player.Id;
                    model.teamId = player.TeamId;

                    playerModelList.Add(model);
                }

                return Ok(playerModelList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
