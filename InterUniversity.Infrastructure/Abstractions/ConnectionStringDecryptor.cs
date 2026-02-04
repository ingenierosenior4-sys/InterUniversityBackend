using System.Security.Cryptography;
using System.Text;

namespace InterUniversity.Infrastructure.Abstractions;

/// <summary>
/// Provides encryption and decryption services for sensitive connection strings.
/// </summary>
public static class ConnectionStringDecryptor
{
    private const string EncryptionKey = "InterUniversity2024SecureKeyFor256BitAES!";

    /// <summary>
    /// Decrypts a connection string encrypted with the Encrypt method.
    /// </summary>
    /// <param name="encryptedText">The base64-encoded encrypted string with IV prepended.</param>
    /// <returns>The decrypted connection string.</returns>
    public static string Decrypt(string encryptedText)
    {
        if (string.IsNullOrEmpty(encryptedText))
        {
            throw new ArgumentNullException(nameof(encryptedText), "Encrypted text cannot be null or empty.");
        }

        try
        {
            var buffer = Convert.FromBase64String(encryptedText);

            using var aes = Aes.Create();
            aes.Key = DeriveKey(EncryptionKey);
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            // Extract IV from the beginning of the buffer
            var iv = new byte[aes.IV.Length];
            Array.Copy(buffer, 0, iv, 0, iv.Length);
            aes.IV = iv;

            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream(buffer, iv.Length, buffer.Length - iv.Length);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs, Encoding.UTF8);

            return sr.ReadToEnd();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to decrypt connection string. Ensure the encryption key is correct.", ex);
        }
    }

    /// <summary>
    /// Derives a 256-bit key from the encryption key string using PBKDF2.
    /// </summary>
    private static byte[] DeriveKey(string keyString)
    {
        const int iterations = 10000;
        const int keySize = 32; // 256 bits
        var salt = Encoding.UTF8.GetBytes("InterUniversity_Salt_2024");

        using var pbkdf2 = new Rfc2898DeriveBytes(keyString, salt, iterations, HashAlgorithmName.SHA256);
        return pbkdf2.GetBytes(keySize);
    }
}
