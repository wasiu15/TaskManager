using Application.Dtos;

namespace Infrastructure.Services.TokenManager
{
    public interface ITokenManager
    {
        string GenerateToken(ref GenericResponse<LoginResponse> loginResponse);
        string GenerateRefreshToken();
    }
}
