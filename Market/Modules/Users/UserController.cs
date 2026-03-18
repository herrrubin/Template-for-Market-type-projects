using Application.DTOs;
using Application.DTOs.UserDTOs;
using Application.Interactors.UserInteractors;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Market.Modules.Users
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get_all")]
        public async Task<ActionResult<PaginatedResponse<UserResponse>>> GetAll([FromQuery] GetAllQuery cmd)
        {
            var paginatedTasks = await _mediator.Send(cmd);
            if (paginatedTasks.Items == null || !paginatedTasks.Items.Any())
                return NotFound();
            return Ok(paginatedTasks);

        }
    }
}
