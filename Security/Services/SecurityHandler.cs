using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Security.Enums;
using Security.Interfaces;

namespace Security.Services;

public class SecurityHandler : ISecurityHandler
{
    private readonly RandomNumberGenerator _rng = RandomNumberGenerator.Create();
    
    public string HashPassword(string password)
    {
        const KeyDerivationPrf pbkdf2Prf = KeyDerivationPrf.HMACSHA256;
        const int iterationCount = 100000;
        const int subkeyLength = 256 / 8;
        const int saltLength = 128 / 8;
        
        byte[] salt = new byte[saltLength];
        _rng.GetBytes(salt);
        byte[] subkey = KeyDerivation.Pbkdf2(password, salt, pbkdf2Prf, iterationCount, subkeyLength);
        
        var outputBytes = new byte[salt.Length + subkey.Length];
        Buffer.BlockCopy(salt, 0, outputBytes, 0, salt.Length);
        Buffer.BlockCopy(subkey, 0, outputBytes, saltLength, subkeyLength);
        
        return Convert.ToBase64String(outputBytes);
    }

    public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
    {
        byte[] decodedHashedPassword = Convert.FromBase64String(hashedPassword);
        
        const KeyDerivationPrf pbkdf2Prf = KeyDerivationPrf.HMACSHA256;
        const int iterationCount = 100000;
        const int subkeyLength = 256 / 8;
        const int saltLength = 128 / 8;
        
        if (decodedHashedPassword.Length != saltLength + subkeyLength) return PasswordVerificationResult.Failed;
        
        byte[] salt = new byte[saltLength];
        Buffer.BlockCopy(decodedHashedPassword, 0, salt, 0, salt.Length);
        
        byte[] expectedSubkey = new byte[subkeyLength];
        Buffer.BlockCopy(decodedHashedPassword, salt.Length, expectedSubkey, 0, expectedSubkey.Length);
        
        byte[] actualSubkey = KeyDerivation.Pbkdf2(providedPassword, salt, pbkdf2Prf, iterationCount, subkeyLength);
        
        return ByteArraysEqual(actualSubkey, expectedSubkey) && CryptographicOperations.FixedTimeEquals(actualSubkey, expectedSubkey)
            ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
    }

    private bool ByteArraysEqual(byte[] a, byte[] b)
    {
        if (a == null && b == null)
        {
            return true;
        }
        if (a == null || b == null || a.Length != b.Length)
        {
            return false;
        }
        var areSame = true;
        for (var i = 0; i < a.Length; i++)
        {
            areSame &= (a[i] == b[i]);
        }
        return areSame;
    }
}