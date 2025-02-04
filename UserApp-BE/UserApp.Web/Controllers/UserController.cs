using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserApp.Core.Request;
using UserApp.Data.Commands;
using UserApp.Data.Queries;
using UserApp.Data.Queries.GetUsers;
using UserApp.Data.Queries.GetUserById;


namespace UserApp.Web.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery] GetUsersRequest request)
        {
            var query = new GetUsersQuery(
                request.Page,
                request.PageSize,
                request.Search,
                request.SortColumn,
                request.SortDirection);
            
            var result = await _mediator.Send(query);
            
            return result.Match<IActionResult>(
                Ok,
                BadRequest);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(id));

            return result.Match<IActionResult>(
                Ok,
                BadRequest);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] RegisterUserRequest newUser)
        {
            ErrorOr<GetUserResponse> result = await _mediator.Send(new CreateUserCommand(newUser));

            if (result.IsError)
            {
                var errors = result.Errors.Select(e => new { e.Code, e.Description });
                return BadRequest(new { Errors = errors });
            }

            return CreatedAtAction(nameof(GetUserById), new { id = result.Value.Id }, result.Value);
        }
    }
}
