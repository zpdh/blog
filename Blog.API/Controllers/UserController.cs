using Blog.API.Attributes;
using Blog.Application.User.ChangeName;
using Blog.Application.User.Get;
using Blog.Application.User.Login;
using Blog.Application.User.Register;
using Blog.Domain.Communication.Requests;
using Blog.Domain.Communication.Requests.User;
using Blog.Domain.Communication.Responses;
using Blog.Domain.Communication.Responses.User;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers;

public class UserController : BlogController {
    [HttpPost]
    [Route("register")]
    [ProducesResponseType(typeof(RegisterUserResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterUserUseCase useCase,
        [FromBody] RegisterUserRequest request
    ) {
        var result = await useCase.ExecuteAsync(request);

        return Created(string.Empty, result);
    }

    [HttpPost]
    [Route("login")]
    [ProducesResponseType(typeof(LoginUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login(
        [FromServices] ILoginUserUseCase useCase,
        [FromBody] LoginUserRequest request
    ) {
        var result = await useCase.ExecuteAsync(request);

        return Ok(result);
    }

    [HttpGet]
    [Route("get")]
    [AuthenticatedUser]
    [ProducesResponseType(typeof(GetUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(
        [FromServices] IGetUserUseCase useCase
    ) {
        var user = await useCase.Execute();

        return Ok(user);
    }

    [HttpPut]
    [AuthenticatedUser]
    [Route("change/username")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ChangeUsername(
        [FromServices] IChangeUserNameUseCase useCase,
        [FromBody] ChangeUsernameUserRequest request
    ) {
        await useCase.Execute(request);

        return NoContent();
    }
}