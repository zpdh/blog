using Blog.Application.User.Register;
using Blog.Domain.Communication.Requests;
using Blog.Domain.Communication.Requests.User;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers;

public class UserController : BlogController {
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterUserUseCase useCase,
        [FromBody] RegisterUserRequest request
    ) {
        var result = await useCase.ExecuteAsync(request);

        return Created(string.Empty, result);
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login() {
        return Ok();
    }

    [HttpDelete]
    [Route("id")]
    public async Task<IActionResult> Delete() {
        return NoContent();
    }
}