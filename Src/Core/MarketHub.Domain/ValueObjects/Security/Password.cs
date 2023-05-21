namespace MarketHub.Domain.ValueObjects.Security;

using System.Security.Cryptography;
using System.Text;
using Infrastructure.Domain.ValueObjects;

public sealed class Password : ValueObject, IEquatable<Password>
{
    private const int SaltLength = 64;

    private static readonly Random Random = new();
    private static readonly HashAlgorithm HashAlgorithm = SHA512.Create();
    
    private Password()
    {
    }

    public Password(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentNullException(nameof(password));
        
        Salt = CreateSalt();
        Hash = ComputeHash(password, Salt);
    }

    public byte[] Hash { get; protected set; }

    public byte[] Salt { get; protected set; }


    public bool Check(string password)
    {
        return Hash.SequenceEqual(ComputeHash(password, Salt));
    }

    private static byte[] CreateSalt()
    {
        byte[] salt = new byte[SaltLength];

        Random.NextBytes(salt);

        return salt;
    }

    private static byte[] ComputeHash(string password,
        byte[] salt)
    {
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        byte[] saltedPassword = new byte[passwordBytes.Length + salt.Length];

        Array.Copy(passwordBytes,
            saltedPassword,
            password.Length);

        Array.Copy(salt,
            0,
            saltedPassword,
            passwordBytes.Length,
            salt.Length);

        return HashAlgorithm.ComputeHash(saltedPassword);
    }


    public bool Equals(Password other)
    {
        if (ReferenceEquals(null, other))
            return false;
        
        if (ReferenceEquals(this, other))
            return true;

        return Hash.SequenceEqual(other.Hash) && Salt.Equals(other.Salt);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) 
            return false;
        
        if (ReferenceEquals(this, obj)) 
            return true;

        if (obj.GetType() != GetType()) 
            return false;

        return Equals((Password) obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Hash, Salt);
    }
}