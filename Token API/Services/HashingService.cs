using System.Security.Cryptography;
using System.Text;

namespace Token_API.Services;



public static class HashingService{
    private static int keySize { get; } = 64;
    private static int iterations { get; } = 350000;
    private static HashAlgorithmName hashAlgorithm { get; }= HashAlgorithmName.SHA512;

    public static string Hash(string password, out byte[] salt) {
        salt = RandomNumberGenerator.GetBytes(keySize);
        
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            iterations,
            hashAlgorithm,
            keySize);
        
        return Convert.ToHexString(hash);
    }
    
    public static bool Verify(string password, string hash, byte[] salt)
    {
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);
        return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
    }
}