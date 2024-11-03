namespace Api.DTOs;

public class UpdateUserDTO
{
    public int Id { get; init; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public double Salary { get; set; }
    public bool Active { get; set; }
}