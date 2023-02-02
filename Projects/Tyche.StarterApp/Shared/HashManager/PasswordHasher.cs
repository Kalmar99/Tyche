﻿using System.Security.Cryptography;
using System.Text;

namespace Tyche.StarterApp.Shared.HashManager;

internal static class PasswordHasher
{
    private const int KeySize = 64;
    
    private const int Iterations = 350000;
    
    private static readonly HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA512;
    
    public static string GenerateHash(string password, out byte[] salt)
    {
        salt = RandomNumberGenerator.GetBytes(KeySize);
        
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            Iterations,
            HashAlgorithm,
            KeySize);
        
        return Convert.ToHexString(hash);
    }
    
    public static bool VerifyHash(string password, string hash, byte[] salt)
    {
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithm, KeySize);
        
        return hashToCompare.SequenceEqual(Convert.FromHexString(hash));
    }
}