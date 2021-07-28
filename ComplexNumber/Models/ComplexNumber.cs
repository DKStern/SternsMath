using ComplexNumber.Interfaces;
using System;

namespace ComplexNumber.Models
{
    public class ComplexNumber : IComplexNumber
    {
        private double _real;
        private double _imaginary;

        public double Real
        {
            get => _real;
            set
            {
                if (_real == value)
                    return;

                _real = value;
            }
        }

        public double Imaginary
        {
            get => _imaginary;
            set
            {
                if (_imaginary == value)
                    return;

                _imaginary = value;
            }
        }

        public double Radius => Abs();

        public double Angle => Math.Acos(Real / Radius); //Криво определяет

        public ComplexNumber(double real = default, double imaginary = default)
        {
            Real = real;
            Imaginary = imaginary;
        }

        public override string ToString()
        {
            return $@"{Real}{(Imaginary < 0 ? string.Empty : "+")}{Imaginary}i";
        }

        /// <summary>
        /// Определяет сопряжённые ли два числа
        /// </summary>
        /// <param name="num1">Первое число</param>
        /// <param name="num2">Второе число</param>
        /// <returns>True, если сопряжённые</returns>
        public static bool IsConjugate(ComplexNumber num1, ComplexNumber num2)
        {
            //double имеет погрешность округления, поэтому может быть, что "равные" числа не равны. Поэтому проверяем по погрешности.
            if (Math.Abs(num1.Imaginary + num2.Imaginary) < 0.000001)
                return true;

            return false;
        }

        public static ComplexNumber operator +(ComplexNumber n1, ComplexNumber n2)
        {
            return new ComplexNumber(n1.Real + n2.Real, n1.Imaginary + n2.Imaginary);
        }

        public static ComplexNumber operator -(ComplexNumber n1, ComplexNumber n2)
        {
            return new ComplexNumber(n1.Real - n2.Real, n1.Imaginary - n2.Imaginary);
        }

        public static ComplexNumber operator *(ComplexNumber n1, ComplexNumber n2)
        {
            var real = n1.Real * n2.Real - n1.Imaginary * n2.Imaginary;
            var imaginary = n1.Real * n2.Imaginary + n1.Imaginary * n2.Real;

            return new ComplexNumber(real, imaginary);
        }

        public static ComplexNumber operator /(ComplexNumber n1, ComplexNumber n2)
        {
            var help = new ComplexNumber(n2.Real, -n2.Imaginary);
            var newN1 = n1 * help;
            var newN2 = n2 * help;

            if (Math.Abs(newN2.Imaginary) > 0.000001)
                throw new Exception("Ошибка деления комплексных чисел");

            var real = newN1.Real / newN2.Real;
            var imaginary = newN1.Imaginary / newN2.Real;

            return new ComplexNumber(real, imaginary);
        }

        private static ComplexNumber GetSummand(int step, int pow, ComplexNumber num)
        {
            var powA = pow - step;
            var a = Math.Pow(num.Real, powA);

            var powB = step;
            var b = Math.Pow(num.Imaginary, powB);

            double mul = 1;
            double dev = 1;
            for (int i = 0; i < step; i++)
            {
                mul *= (pow - i) / dev;
                dev++;
            }

            var result = a * b * mul;
            double real = 0;
            double imaginary = 0;
            if (step % 2 == 0)
            {
                real = result;
                if (step % 4 != 0)
                    real *= -1;
            }
            else
            {
                imaginary = result;
                if (step % 3 == 0)
                    imaginary *= -1;
            }

            return new ComplexNumber(real, imaginary);
        }

        public static ComplexNumber operator ^(ComplexNumber num, int pow)
        {
            return PowBySum(num, pow);
        }

        private static ComplexNumber PowBySum(ComplexNumber num, int pow)
        {
            var sum = new ComplexNumber();
            for (int i = 0; i <= pow; i++)
            {
                sum += GetSummand(i, pow, num);
            }

            return sum;
        }

        private static ComplexNumber PowByMultiply(ComplexNumber num, int pow)
        {
            var res = new ComplexNumber(1);
            for (int i = 0; i < pow; i++)
            {
                res *= num;
            }

            return res;
        }

        public static ComplexNumber Pow(ComplexNumber num, int pow, bool useMultiplyOrSum = true)
        {
            if (useMultiplyOrSum)
                return PowByMultiply(num, pow);

            return PowBySum(num, pow);
        }

        public ComplexNumber Pow(int pow)
        {
            return Pow(this, pow);
        }

        public double Abs()
        {
            return Math.Sqrt(Math.Pow(Real, 2) + Math.Pow(Imaginary, 2));
        }
    }
}