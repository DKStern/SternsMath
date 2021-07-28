using System.Threading.Tasks;

namespace SternsMath.Models.Encryption
{
    public static class Encryptions
    {
        /// <summary>
        /// Метод шифрования/дешифрования методом Атбаш
        /// </summary>
        public static string Atbash(this string text)
        {
            return Encryption.Atbash.EncryptDecrypt(text);
        }

        /// <summary>
        /// Метод шифрования/дешифрования методом Атбаш асинхронный
        /// </summary>
        public static async Task<string> AtbashAsync(this string text)
        {
            return await Encryption.Atbash.EncryptDecryptAsync(text);
        }

        /// <summary>
        /// Шифрование с помощью XOR
        /// </summary>
        /// <param name="text">Текст</param>
        /// <param name="password">Пароль</param>
        /// <returns>Зашифрованный текст</returns>
        public static string XOREncryptDecrypt(this string text, string password)
        {
            return XOR.EncryptDecrypt(text, password);
        }

        /// <summary>
        /// Шифрование с помощью XOR асинхронное
        /// </summary>
        /// <param name="text">Текст</param>
        /// <param name="password">Пароль</param>
        /// <returns>Зашифрованный текст</returns>
        public static async Task<string> XOREncryptDecryptAsync(this string text, string password)
        {
            return await Task.Run(() => XOR.EncryptDecrypt(text, password));
        }
    }
}