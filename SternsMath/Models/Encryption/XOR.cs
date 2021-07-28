namespace SternsMath.Models.Encryption
{
    public static class XOR
    {
        /// <summary>
        /// Генератор повторений пароля
        /// </summary>
        /// <param name="secretKey">Ключ</param>
        /// <param name="length">Длина сообщения</param>
        /// <returns>Ключ шифрования</returns>
        private static string GetRepeatKey(string secretKey, int length)
        {
            var res = secretKey;
            while (res.Length < length)
            {
                res += res;
            }

            return res.Substring(0, length);
        }

        /// <summary>
        /// Метод шифрования/дешифровки
        /// </summary>
        private static string Cipher(string text, string secretKey)
        {
            var currentKey = GetRepeatKey(secretKey, text.Length);
            var res = string.Empty;
            for (var i = 0; i < text.Length; i++)
            {
                res += ((char)(text[i] ^ currentKey[i])).ToString();
            }

            return res;
        }

        /// <summary>
        /// Шифрование/дешифрования
        /// </summary>
        /// <param name="plainText">Текст</param>
        /// <param name="password">Пароль</param>
        /// <returns>Зашифрованный/дешифрованный текст</returns>
        public static string EncryptDecrypt(string plainText, string password)
            => Cipher(plainText, password);
    }
}