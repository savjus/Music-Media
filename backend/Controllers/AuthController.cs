using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _svc;
    public AuthController(AuthService svc) => _svc = svc;

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var user = _svc.ValidateLogin(request.Email, request.Password);
        if (user is null)
            return Unauthorized();

        var token = _svc.GenerateToken(user, request.RememberMe);
        return Ok(new LoginResponse { Username = user.Username, Email = user.Email, Token = token });
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterRequest request)
    {
        if (!_svc.Register(request.Username, request.Email, request.Password))
        {
            return Conflict("Email already in use.");
        }

        return Ok();
    }
}
