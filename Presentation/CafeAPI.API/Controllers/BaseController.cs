using CafeAPI.Application.Dtos.ResponseDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CafeAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IActionResult CreateResponse<T>(ResponseDto<T> response) where T : class
        {
            if (response.Success)
            {
                return Ok(response);
            }
            return response.ErrorCode switch
            {
                ErrorCodes.NotFound => NotFound(response),
                ErrorCodes.ValidationError => BadRequest(response),
                ErrorCodes.Unauthorized => Unauthorized(response),
                ErrorCodes.Forbidden => StatusCode(StatusCodes.Status403Forbidden,response),
                ErrorCodes.Exception => StatusCode(StatusCodes.Status500InternalServerError,response),
                ErrorCodes.DuplicateError => Conflict(response),
                ErrorCodes.BadRequest => BadRequest(response),
                _ => BadRequest(response)

            };
        }
    }
}
