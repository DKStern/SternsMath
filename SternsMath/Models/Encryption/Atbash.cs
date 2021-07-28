using SternsMath.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SternsMath.Models.Encryption
{
    public static class Atbash
    {
        private const string _engAlphabet = "abcdefghijklmnopqrstuvwxyz";

        private const string _rusAlphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";

        /// <summary>
        /// Метод шифрования/дешифрования
        /// </summary>
        /// <param name="text">Текст</param>
        /// <returns>Зашифрованный/Дешифрованные текст</returns>
        public static string EncryptDecrypt(string text)
        {
            var cipherEng = string.Join("", _engAlphabet.Reverse().ToList());
            var cipherRus = string.Join("", _rusAlphabet.Reverse().ToList());

            Dictionary<LanguageShort, string> alphabetContainer = new Dictionary<LanguageShort, string>()
            {
                {LanguageShort.None, string.Empty },
                {LanguageShort.Rus, _rusAlphabet },
                {LanguageShort.Eng, _engAlphabet }
            };

            Dictionary<LanguageShort, string> cipherContainer = new Dictionary<LanguageShort, string>()
            {
                {LanguageShort.None, string.Empty },
                {LanguageShort.Rus, cipherRus },
                {LanguageShort.Eng, cipherEng }
            };

            var outputText = string.Empty;
            foreach (var t in text)
            {
                bool isUpper = Regex.IsMatch(t.ToString(), @"^[A-ZА-Я]?$", RegexOptions.Compiled);
                var lang = LanguageShort.None;

                if (Regex.IsMatch(t.ToString(), $@"^[а-яА-Я]$")) 
                    lang = LanguageShort.Rus;

                if (Regex.IsMatch(t.ToString(), $@"^[a-zA-Z]$")) 
                    lang = LanguageShort.Eng;

                var index = alphabetContainer[lang].IndexOf(t.ToString().ToLower(), StringComparison.Ordinal);
                if (index >= 0)
                {
                    var replace = cipherContainer[lang][index].ToString();
                    if (isUpper)
                        replace = replace.ToUpper();
                    outputText += replace;
                }
                else
                {
                    outputText += t;
                }
            }

            return outputText;
        }

        public static async Task<string> EncryptDecryptAsync(string text)
        {
            return await Task.Run(() => EncryptDecrypt(text));
        }
    }
}