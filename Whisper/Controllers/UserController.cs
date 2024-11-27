using Microsoft.AspNetCore.Mvc;

namespace Whisper.User.Controllers;

[Route("api/users")]
[ApiController]
public class UserController : Controller
{
    [HttpPost]
    public string Init([FromBody] string test)
    {
        return test + " initial";
    }
}