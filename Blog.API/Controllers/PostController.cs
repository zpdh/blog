using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers;

public class PostController : BlogController {
    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> Create() {
        return Created();
    }

    [HttpPut]
    [Route("edit")]
    public async Task<IActionResult> Edit() {
        return NoContent();
    }

    [HttpDelete]
    [Route("code")]
    public async Task<IActionResult> Delete(
        [FromRoute] string code
    ) {
        return NoContent();
    }
}