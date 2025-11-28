using IdevNoStudio.Api.Models;

namespace IdevNoStudio.Api.Services;

public interface IJwtService
{
    string GenerateToken(User user);
}

