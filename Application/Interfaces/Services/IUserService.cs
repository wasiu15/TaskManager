using Application.Dtos;
using TaskmanagerV2.Domain.Enums;

namespace Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<GenericResponse<IEnumerable<UserWithIdDto>>> GetAllUsers();
        Task<GenericResponse<UserDto>> GetByUserId(string userId);
        Task<GenericResponse<LoginResponse>> GetByEmailAndPassword(LoginDto loginRequest, bool track);
        Task<GenericResponse<Response>> CreateUser(RegisterDto task);
        Task<GenericResponse<Response>> UpdateUser(string userIdString, UpdateUserRequest request);
        Task<GenericResponse<Response>> DeleteUser(string userId);
        Task<GenericResponse<Response>> AssignTask(string userId, AddOrDelete operation, string taskId);
        Task<GenericResponse<TokenDto>> RefreshToken(RefreshTokenDto request);
    }
}
