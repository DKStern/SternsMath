using System;
using System.Collections.Generic;

namespace SternsMath.Models.Math
{
    public static class Math
    {
        /// <summary>
        /// Быстрое возвредение в степень
        /// </summary>
        /// <param name="num">Число</param>
        /// <param name="pow">Степень</param>
        /// <returns>Число</returns>
        public static long QuickPower(this long num, int pow)
        {
            long result = 1;

            checked
            {
                try
                {
                    while (pow > 0)
                    {
                        if ((pow & 1) == 0)
                        {
                            num *= num;
                            pow >>= 1;
                        }
                        else
                        {
                            result *= num;
                            --pow;
                        }
                    }
                }
                catch(Exception exception)
                {
                    return 0;
                }
            }

            return result;
        }

        /// <summary>
        /// Наибольший общий делитель (НОД)
        /// </summary>
        public static long GCD(long a, long b)
        {
            while (b != 0)
            {
                var t = b;
                b = a % b;
                a = t;
            }

            return a;
        }

        /// <summary>
        /// Наибольший общий делитель (НОД)
        /// </summary>
        public static long GCD(List<long> list)
        {
            var nod = list[0];

            for (int i = 1; i < list.Count; i++)
            {
                nod = GCD(nod, list[i]);
            }

            return nod;
        }

        /// <summary>
        /// Наибольший общий делитель (НОД)
        /// </summary>
        public static long GCD(List<int> list)
        {
            var nod = Convert.ToInt64(list[0]);

            for (int i = 1; i < list.Count; i++)
            {
                nod = GCD(nod, list[i]);
            }

            return nod;
        }
    }
}