using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _svc;
    private readonly UserProfileService _profiles;

    public AuthController(AuthService svc, UserProfileService profiles)
    {
        _svc = svc;
        _profiles = profiles;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var user = _svc.ValidateLogin(request.Email, request.Password);
        if (user is null)
            return Unauthorized();

        var token = _svc.GenerateToken(user, request.RememberMe);
        return Ok(new LoginResponse
        {
            Username = user.Username,
            Email = user.Email,
            Token = token
        });
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterRequest request)
    {
        if (!_svc.Register(request.Username, request.Email, request.Password))
        {
            return Conflict("Email already in use.");
        }

        var user = _svc.ValidateLogin(request.Email, request.Password);
        if (user != null)
        {
            _profiles.EnsureProfileExistsForUser(user.Id);
        }

        return Ok();
    }

    [Authorize]
    [HttpPost("change-password")]
    public IActionResult ChangePassword([FromBody] ChangePasswordRequest request)
    {
        var userIdClaim = User.Claims.FirstOrDefault(c =>
            c.Type == ClaimTypes.NameIdentifier || c.Type == "sub");

        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
            return Unauthorized();

        var success = _svc.ChangePassword(userId, request.CurrentPassword, request.NewPassword);

        if (!success)
            return BadRequest("Invalid current password");

        return Ok();
    }

    [Authorize]
    [HttpPost("delete-account")]
    public IActionResult DeleteAccount([FromBody] DeleteAccountRequest request)
    {
        var userIdClaim = User.Claims.FirstOrDefault(c =>
            c.Type == ClaimTypes.NameIdentifier || c.Type == "sub");

        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
            return Unauthorized();

        var success = _svc.DeleteAccount(userId, request.Password);

        if (!success)
            return BadRequest("Invalid password");

        return Ok();
    }
}