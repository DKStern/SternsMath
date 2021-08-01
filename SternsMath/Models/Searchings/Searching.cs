using System;

namespace SternsMath.Models.Searchings
{
    public static class Searching
    {
        private static void CheckArray<T>(T[] a)
        {
            if (a == null)
            {
                throw new ArgumentNullException("Массив не может быть нулевым");
            }

            if (a.Length == 0)
            {
                throw new ArgumentException("Длина массива должна быть больше нуля");
            }
        }

        /// <summary>
        /// Линейный поиск
        /// </summary>
        public static int LinearSearch<T>(this T[] a, T searchedValue) where T : IEquatable<T>
        {
            CheckArray(a);

            for (int i = 0; i < a.Length; ++i)
            {
                if (a[i].Equals(searchedValue))
                {
                    return i;
                }
            }

            return -1; //если ничего не нашли
        }

        /// <summary>
        /// Бинарный поиск
        /// </summary>
        public static int BinarySearch(this int[] array, int searchedValue)
        {
            CheckArray(array);

            var left = 0;
            var right = array.Length - 1;

            while (left <= right)
            {
                var middle = (left + right) / 2;

                if (searchedValue == array[middle])
                {
                    return middle;
                }

                if (searchedValue < array[middle])
                {
                    right = middle - 1;
                }
                else
                {
                    left = middle + 1;
                }
            }

            return -1;
        }
    }
}