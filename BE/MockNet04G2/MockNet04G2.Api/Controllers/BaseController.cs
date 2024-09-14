using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MockNet04G2.Core.Common.Enums;
using MockNet04G2.Core.Common;

namespace MockNet04G2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected int UserId
        {
            get
            {
                var userIdClaim = User.FindFirst("userId");
                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    return userId;
                }
                return 0;
            }
        }

        public IActionResult HandleApiResponse<TBody, TError>(ApiResponse<TBody, TError> response)
        {
            switch (response.Status)
            {
                case StatusResponseEnum.Success:
                    return Ok(new { status = response.Status, body = response.Body });
                case StatusResponseEnum.Created:
                    return StatusCode((int)StatusResponseEnum.Created, new { status = response.Status, body = response.Body });
                case StatusResponseEnum.NoContent:
                    return NoContent();
                case StatusResponseEnum.BadRequest:
                    return BadRequest(new { status = response.Status, error = response.Error });
                case StatusResponseEnum.Unauthorized:
                    return Unauthorized(new { status = response.Status, error = response.Error });
                case StatusResponseEnum.NotFound:
                    return NotFound(new { status = response.Status, error = response.Error });
                case StatusResponseEnum.Conflict:
                    return Conflict(new { status = response.Status, error = response.Error });
                default:
                    return StatusCode((int)response.Status, new { status = response.Status, error = response.Error });
            }
        }
    }
}
