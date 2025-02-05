using Microsoft.AspNetCore.Mvc;
using PokerPlanningBackend.Requests.MeetingRoom;
using PokerPlanningBackend.Services;

namespace PokerPlanningBackend.Controllers
{
    [ApiController]
    [Route("api/v1/meeting-rooms")]
    public class MeetingRoomController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create()
        {
            return Ok();
        }

        [HttpPost("join")]
        public async Task<IActionResult> Join([FromBody] JoinRoomRequest request)
        {
            return Ok(request.RoomId);
        }
    }
}
