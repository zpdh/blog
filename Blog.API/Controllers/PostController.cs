using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers;

public class PostController : BlogController {
    [HttpPost]
    public async Task<IActionResult> Create() {
        return Created();
    }

    [HttpPut]
    public async Task<IActionResult> Edit() {
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete() {
        return NoContent();
    }
}