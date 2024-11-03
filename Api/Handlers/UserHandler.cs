using System.Data.SqlClient;
using Api.DTOs;
using Api.Models;
using Dapper;

namespace Api.Handlers;

public class UserHandler(IConfiguration configuration) : IUserHandler
{
    private readonly IConfiguration _configuration = configuration;
    
    public async Task<Response<List<UserDTO>>> GetAllUsers()
    {
        var response = new Response<List<UserDTO>>();
        using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            var users = await conn.QueryAsync<User>("SELECT * FROM Users");

            var enumerable = users as User[] ?? users.ToArray();
            if (enumerable.Length == 0)
            {
                response.Message = "No users found";
                response.Status = false;
                
                return response;
            }
            
            response.Data = [];
            foreach (var user in enumerable)
            {
                response.Data.Add((UserDTO) user);
            }
            response.Message = "Users found";
        }
        
        return response;
    }

    public async Task<Response<UserDTO>> GetUserById(int id)
    {
        var response = new Response<UserDTO>();

        using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            var user = await conn.QueryFirstOrDefaultAsync<User>("SELECT * FROM Users WHERE Id = @Id", new { Id = id });

            if (user is null)
            {
                response.Message = "No user found";
                response.Status = false;
                
                return response;
            }
            
            response.Data = (UserDTO) user!;
            response.Message = "User found";
        }
        
        return response;
    }

    public async Task<Response<UserDTO>> CreateUser(CreateUserDTO user)
    {
        var response = new Response<UserDTO>();

        using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            var rowsAffected = await conn.ExecuteAsync("INSERT INTO Users (Name, Email, Password, [Role], Salary, CPF, Active)" +
                                            "VALUES (@Name, @Email, @Password, @Role, @Salary, @CPF, @Active)", user);

            if (rowsAffected == 0)
            {
                response.Message = "Error creating user";
                response.Status = false;
                
                return response;
            }

            var userId = await GetCreatedUserId(conn);
            
            response.Data = new UserDTO() { Id = userId };
            response.Message = "User created";
            response.Status = true;
        }
        
        return response;
    }

    public async Task<Response<UserDTO>> UpdateUser(UpdateUserDTO user)
    {
        var response = new Response<UserDTO>();

        using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            var rowsAffected = await conn.ExecuteAsync("UPDATE Users SET " + 
                "Name = @Name, Email = @Email, [Role] = @Role, Salary = @Salary, Active = @Active WHERE Id = @Id", user);

            if (rowsAffected == 0)
            {
                response.Message = "Error updating user";
                response.Status = false;
                
                return response;
            }
            
            response.Message = "User updated";
            response.Status = true;
        }
        
        return response;
    }

    public async Task<Response<UserDTO>> DeleteUser(int id)
    {
        var response = new Response<UserDTO>();

        using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            var rowsAffected = await conn.ExecuteAsync("DELETE FROM Users WHERE Id = @Id", new { Id = id });

            if (rowsAffected == 0)
            {
                response.Message = "Error deleting user";
                response.Status = false;
                
                return response;
            }
            
            response.Message = "User deleted";
            response.Status = true;
        }
        
        return response;
    }

    private static async Task<int> GetCreatedUserId(SqlConnection conn)
    {
        return await conn.ExecuteScalarAsync<int>("SELECT Id FROM CrudDapper.dbo.Users ORDER BY ID DESC");
    }
}