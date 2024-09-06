using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers;

public class UserController : BlogController {
    [HttpPost]
    public async Task<IActionResult> Register() {
        return Created();
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