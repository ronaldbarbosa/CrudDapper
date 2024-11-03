using Api.Models;

namespace Api.DTOs;

public class CreateUserDTO
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public double Salary { get; set; }
    public string CPF { get; set; }
    public bool Active { get; set; }
}