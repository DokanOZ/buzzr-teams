using Buzzr.API.Models;
using Buzzr.AppLogic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Buzzr.API.Controllers.Player
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IService _service;

        public GameController(IService service)
        {
            _service = service;
        }

        [HttpPost("buzz")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult PlayerBuzz([FromBody] BuzzedPlayerModel model)
        {
            try
            {
                var player = _service.GetPlayer(model.PlayerId);

                if (player == null)
                {
                    return BadRequest("Could not find player");
                }
                if (!player.BuzzActive)
                {
                    return Ok("player banned");
                }
                TimeOnly time = new TimeOnly(model.BuzzTime.Hour, model.BuzzTime.Minute, model.BuzzTime.Second, model.BuzzTime.Milisecond);

                _service.Buzz(model.PlayerId, time);

                return Ok("Player buzz received");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
