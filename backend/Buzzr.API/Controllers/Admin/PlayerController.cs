using Buzzr.API.Models;
using Buzzr.AppLogic.Interfaces;
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

        [HttpPost()]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddPlayer([FromBody] CreatePlayerModel model)
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

        [HttpPost()]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> RemovePlayer([FromBody] Guid playerId)
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

        [HttpPost()]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> BanPlayer([FromBody] Guid playerId)
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

        [HttpPost()]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UnBanAll()
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

        [HttpPost()]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ResetBuzzBan()
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

        [HttpPost()]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ResetBuzzer()
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

        [HttpGet()]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAllBuzzedPlayers()
        {
            try
            {
                var players = _service.GetBuzzedList();

                if (players == null || players.Count <= 0)
                {
                    return NotFound("No players found");
                }

                return Ok(players);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet()]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetRandomBuzzedPlayer()
        {
            try
            {
                var player = _service.GetRandomPlayer();

                if (player == null)
                {
                    return NotFound("No player found");
                }

                return Ok(player);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("{playerId:Guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetOnePlayer([FromRoute] Guid playerId)
        {
            try
            {
                var player = _service.GetPlayer(playerId);

                if (player == null)
                {
                    return NotFound("No player found");
                }

                return Ok(player);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet()]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAllPlayers()
        {
            try
            {
                var players = _service.GetPlayers();

                if (players == null || players.Count <= 0)
                {
                    return NotFound("No players found");
                }

                return Ok(players);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
