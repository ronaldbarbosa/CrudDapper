using Security.Enums;

namespace Security.Interfaces;

public interface ISecurityHandler
{
    string HashPassword(string password);
    PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword);
}