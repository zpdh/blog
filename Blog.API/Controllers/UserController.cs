using Blog.Application.User.Register;
using Blog.Domain.Communication.Requests.ClassRequests;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers;

public class UserController : BlogController {
    [HttpPost]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterUserUseCase useCase,
        [FromBody] RegisterUserRequest request
    ) {
        var result = await useCase.ExecuteAsync(request);

        return Created(string.Empty, result);
    }

    [HttpPost]
    public async Task<IActionResult> Login() {
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> DisplayInfo() {
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete() {
        return NoContent();
    }
}