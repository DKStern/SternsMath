using LongArithmetic.Enums;
using LongArithmetic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongArithmetic.Models
{
    public class LongNumber : ILongNumber
    {
        private readonly List<byte> digits = new List<byte>();

        public LongNumber(List<byte> bytes)
        {
            digits = bytes.ToList();
            RemoveNulls();
        }

        public LongNumber(Sign sign, List<byte> bytes)
        {
            Sign = sign;
            digits = bytes;
            RemoveNulls();
        }

        public LongNumber(string s)
        {
            if (s.StartsWith("-"))
            {
                Sign = Sign.Minus;
                s = s.Substring(1);
            }

            foreach (var c in s.Reverse())
            {
                digits.Add(Convert.ToByte(c.ToString()));
            }

            RemoveNulls();
        }

        public LongNumber(uint x) => digits.AddRange(GetBytes(x));

        public LongNumber(int x)
        {
            if (x < 0)
            {
                Sign = Sign.Minus;
            }

            digits.AddRange(GetBytes((uint)Math.Abs(x)));
        }

        /// <summary>
        /// метод для получения списка цифр из целого беззнакового числа
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private List<byte> GetBytes(uint num)
        {
            var bytes = new List<byte>();
            do
            {
                bytes.Add((byte)(num % 10));
                num /= 10;
            } while (num > 0);

            return bytes;
        }

        /// <summary>
        /// метод для удаления лидирующих нулей длинного числа
        /// </summary>
        private void RemoveNulls()
        {
            for (var i = digits.Count - 1; i > 0; i--)
            {
                if (digits[i] == 0)
                {
                    digits.RemoveAt(i);
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// метод для получения больших чисел формата valEexp(пример 1E3 = 1000)
        /// </summary>
        /// <param name="val">значение числа</param>
        /// <param name="exp">экспонента(количество нулей после значения val)</param>
        /// <returns></returns>
        public static LongNumber Exp(byte val, int exp)
        {
            var bigInt = Zero;
            bigInt.SetByte(exp, val);
            bigInt.RemoveNulls();
            return bigInt;
        }

        public static LongNumber Zero => new LongNumber(0);
        public static LongNumber One => new LongNumber(1);

        //длина числа
        public int Size => digits.Count;

        //знак числа
        public Sign Sign { get; private set; } = Sign.Plus;

        //получение цифры по индексу
        public byte GetByte(int i) => i < Size ? digits[i] : (byte)0;

        //установка цифры по индексу
        public void SetByte(int i, byte b)
        {
            while (digits.Count <= i)
            {
                digits.Add(0);
            }

            digits[i] = b;
        }

        //преобразование длинного числа в строку
        public override string ToString()
        {
            if (this == Zero) return "0";
            var s = new StringBuilder(Sign == Sign.Plus ? "" : "-");

            for (int i = digits.Count - 1; i >= 0; i--)
            {
                s.Append(Convert.ToString(digits[i]));
            }

            return s.ToString();
        }

        #region Методы арифметических действий над большими числами

        private static async Task<LongNumber> AddAsync(LongNumber a, LongNumber b)
        {
            return await Task.Run(() => Add(a, b));
        }

        private static LongNumber Add(LongNumber a, LongNumber b)
        {
            var digits = new List<byte>();
            var maxLength = Math.Max(a.Size, b.Size);
            byte t = 0;
            for (int i = 0; i < maxLength; i++)
            {
                byte sum = (byte)(a.GetByte(i) + b.GetByte(i) + t);
                if (sum > 10)
                {
                    sum -= 10;
                    t = 1;
                }
                else
                {
                    t = 0;
                }

                digits.Add(sum);
            }

            if (t > 0)
            {
                digits.Add(t);
            }

            return new LongNumber(a.Sign, digits);
        }

        private static async Task<LongNumber> SubAsync(LongNumber a, LongNumber b)
        {
            return await Task.Run(() => Sub(a, b));
        }

        private static LongNumber Sub(LongNumber a, LongNumber b)
        {
            var digits = new List<byte>();

            LongNumber max = Zero;
            LongNumber min = Zero;

            //сравниваем числа игнорируя знак
            var compare = Comparison(a, b, ignoreSign: true);

            switch (compare)
            {
                case -1:
                    min = a;
                    max = b;
                    break;
                case 0:
                    return Zero;
                case 1:
                    min = b;
                    max = a;
                    break;
            }

            //из большего вычитаем меньшее
            var maxLength = Math.Max(a.Size, b.Size);

            var t = 0;
            for (var i = 0; i < maxLength; i++)
            {
                var s = max.GetByte(i) - min.GetByte(i) - t;
                if (s < 0)
                {
                    s += 10;
                    t = 1;
                }
                else
                {
                    t = 0;
                }

                digits.Add((byte)s);
            }

            return new LongNumber(max.Sign, digits);
        }

        private static async Task<LongNumber> MultiplyAsync(LongNumber a, LongNumber b)
        {
            return await Task.Run(() => Multiply(a, b));
        }

        private static LongNumber Multiply(LongNumber a, LongNumber b)
        {
            var retValue = Zero;

            for (var i = 0; i < a.Size; i++)
            {
                for (int j = 0, carry = 0; (j < b.Size) || (carry > 0); j++)
                {
                    var cur = retValue.GetByte(i + j) + a.GetByte(i) * b.GetByte(j) + carry;
                    retValue.SetByte(i + j, (byte)(cur % 10));
                    carry = cur / 10;
                }
            }

            retValue.Sign = a.Sign == b.Sign ? Sign.Plus : Sign.Minus;
            return retValue;
        }

        private static async Task<LongNumber> DivAsync(LongNumber a, LongNumber b)
        {
            return await Task.Run(() => Div(a, b));
        }

        private static LongNumber Div(LongNumber a, LongNumber b)
        {
            var retValue = Zero;
            var curValue = Zero;

            for (var i = a.Size - 1; i >= 0; i--)
            {
                curValue += Exp(a.GetByte(i), i);

                var x = 0;
                var l = 0;
                var r = 10;
                while (l <= r)
                {
                    var m = (l + r) / 2;
                    var cur = b * Exp((byte)m, i);
                    if (cur <= curValue)
                    {
                        x = m;
                        l = m + 1;
                    }
                    else
                    {
                        r = m - 1;
                    }
                }

                retValue.SetByte(i, (byte)(x % 10));
                var t = b * Exp((byte)x, i);
                curValue = curValue - t;
            }

            retValue.RemoveNulls();

            retValue.Sign = a.Sign == b.Sign ? Sign.Plus : Sign.Minus;
            return retValue;
        }

        private static async Task<LongNumber> ModAsync(LongNumber a, LongNumber b)
        {
            return await Task.Run(() => Mod(a, b));
        }

        private static LongNumber Mod(LongNumber a, LongNumber b)
        {
            var retValue = Zero;

            for (var i = a.Size - 1; i >= 0; i--)
            {
                retValue += Exp(a.GetByte(i), i);

                var x = 0;
                var l = 0;
                var r = 10;

                while (l <= r)
                {
                    var m = (l + r) >> 1;
                    var cur = b * Exp((byte)m, i);
                    if (cur <= retValue)
                    {
                        x = m;
                        l = m + 1;
                    }
                    else
                    {
                        r = m - 1;
                    }
                }

                retValue -= b * Exp((byte)x, i);
            }

            retValue.RemoveNulls();

            retValue.Sign = a.Sign == b.Sign ? Sign.Plus : Sign.Minus;
            return retValue;
        }

        #endregion

        #region Методы для сравнения больших чисел

        private static async Task<int> ComparisonAsync(LongNumber a, LongNumber b, bool ignoreSign = false)
        {
            return await Task.Run(() => Comparison(a, b, ignoreSign));
        }

        private static int Comparison(LongNumber a, LongNumber b, bool ignoreSign = false)
        {
            return CompareSign(a, b, ignoreSign);
        }

        private static int CompareSign(LongNumber a, LongNumber b, bool ignoreSign = false)
        {
            if (!ignoreSign)
            {
                if (a.Sign < b.Sign)
                {
                    return -1;
                }
                else if (a.Sign > b.Sign)
                {
                    return 1;
                }
            }

            return CompareSize(a, b);
        }

        private static int CompareSize(LongNumber a, LongNumber b)
        {
            if (a.Size < b.Size)
            {
                return -1;
            }
            else if (a.Size > b.Size)
            {
                return 1;
            }

            return CompareDigits(a, b);
        }

        private static int CompareDigits(LongNumber a, LongNumber b)
        {
            var maxLength = Math.Max(a.Size, b.Size);
            for (var i = maxLength; i >= 0; i--)
            {
                if (a.GetByte(i) < b.GetByte(i))
                {
                    return -1;
                }
                else if (a.GetByte(i) > b.GetByte(i))
                {
                    return 1;
                }
            }

            return 0;
        }

        #endregion

        #region Арифметические операторы

        // унарный минус(изменение знака числа)
        public static LongNumber operator -(LongNumber a)
        {
            var sign = a.Sign == Sign.Plus ? Sign.Minus : Sign.Plus;
            return new LongNumber(sign, a.digits);
        }

        //сложение
        public static LongNumber operator +(LongNumber a, LongNumber b) => a.Sign == b.Sign
                ? AddAsync(a, b).Result
                : SubAsync(a, b).Result;

        //вычитание
        public static LongNumber operator -(LongNumber a, LongNumber b) => a + -b;

        //умножение
        public static LongNumber operator *(LongNumber a, LongNumber b) => MultiplyAsync(a, b).Result;

        //целочисленное деление(без остатка)
        public static LongNumber operator /(LongNumber a, LongNumber b) => DivAsync(a, b).Result;

        //остаток от деления
        public static LongNumber operator %(LongNumber a, LongNumber b) => ModAsync(a, b).Result;

        #endregion

        #region Операторы сравнения

        public static bool operator <(LongNumber a, LongNumber b) => ComparisonAsync(a, b).Result < 0;

        public static bool operator >(LongNumber a, LongNumber b) => ComparisonAsync(a, b).Result > 0;

        public static bool operator <=(LongNumber a, LongNumber b) => ComparisonAsync(a, b).Result <= 0;

        public static bool operator >=(LongNumber a, LongNumber b) => ComparisonAsync(a, b).Result >= 0;

        public static bool operator ==(LongNumber a, LongNumber b) => ComparisonAsync(a, b).Result == 0;

        public static bool operator !=(LongNumber a, LongNumber b) => ComparisonAsync(a, b).Result != 0;

        public override bool Equals(object obj) => obj is LongNumber number && this == number;

        #endregion
    }
}