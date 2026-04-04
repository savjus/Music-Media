using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class AuthService
{
    private readonly IConfiguration _config;

    // Pakeisti i EF callus kai bus DB
    private readonly List<UserAccount> _users =
    [
        new UserAccount { Id = 1, Username = "admin", Email = "admin@musicmedia.com", Password = "Admin123!" },
        new UserAccount { Id = 2, Username = "test", Email = "test", Password = "test"  },
        new UserAccount { Id = 3, Username = "a", Email = "a", Password = "a"  },
    ];

    public AuthService(IConfiguration config) => _config = config;

    public UserAccount? ValidateLogin(string email, string password) =>
        _users.FirstOrDefault(u =>
            u.Email.Equals(email, StringComparison.OrdinalIgnoreCase) &&
            u.Password == password);

    public bool Register(string username, string email, string password)
    {
        if (_users.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
            return false;

        _users.Add(new UserAccount
        {
            Id = _users.Max(u => u.Id) + 1,
            Username = username,
            Email = email,
            Password = password
        });
        return true;
    }

    public string GenerateToken(UserAccount user, bool rememberMe)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiry = rememberMe ? DateTime.UtcNow.AddHours(20) : DateTime.UtcNow.AddHours(1);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Name, user.Username),
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: expiry,
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public bool ChangePassword(int userId, string currentPassword, string newPassword)
    {
        var user = _users.FirstOrDefault(u => u.Id == userId);

        if (user == null)
            return false;

        if (user.Password != currentPassword)
            return false;

        user.Password = newPassword;
        return true;
    }
}
