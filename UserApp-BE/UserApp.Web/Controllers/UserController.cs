using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserApp.Core.Request;
using UserApp.Data.Commands;
using UserApp.Data.Queries.GetAllUsers;
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
        public async Task<ActionResult<IEnumerable<GetUserRequest>>> GetAllUsers()
        {
            var users = await _mediator.Send(new GetAllUsersQuery());
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserRequest>> GetUserById(string id)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(id));
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<GetUserRequest>> CreateUser([FromBody] RegisterUserRequest newUser)
        {
            ErrorOr<GetUserRequest> result = await _mediator.Send(new CreateUserCommand(newUser));

            if (result.IsError)
            {
                var errors = result.Errors.Select(e => new { e.Code, e.Description });
                return BadRequest(new { Errors = errors });
            }

            return CreatedAtAction(nameof(GetUserById), new { id = result.Value.Id }, result.Value);
        }
    }
}
