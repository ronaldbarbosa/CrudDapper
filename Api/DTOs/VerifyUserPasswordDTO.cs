using System.Globalization;

namespace Api.DTOs;

public class VerifyUserPasswordDTO
{
    public int Id { get; set; }
    public string Password { get; set; }
}