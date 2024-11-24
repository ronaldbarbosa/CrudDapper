using Api.DTOs;
using Api.Models;

namespace Api.Handlers;

public interface IUserHandler
{
    Task<Response<List<UserDTO>>> GetAllUsers();
    Task<Response<UserDTO>> GetUserById(int id);
    Task<Response<UserDTO>> CreateUser(CreateUserDTO user);
    Task<Response<UserDTO>> UpdateUser(UpdateUserDTO user);
    Task<Response<UserDTO>> DeleteUser(int id);
    Task<Response<string>> GetUserPassword(int id);
}