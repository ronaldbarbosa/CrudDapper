using Api.Models;

namespace Api.DTOs;

public class UserDTO
{
    public int Id { get; init; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public double Salary { get; set; }
    public bool Active { get; set; }

    public static explicit operator UserDTO(User user)
    {
        return new UserDTO()
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role,
            Salary = user.Salary,
            Active = user.Active
        };
    }
}