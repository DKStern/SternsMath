using SternsMath.Models.Trees;
using System;
using System.Linq;

namespace SternsMath.Models.Sortings
{
    public static class Sorting
    {
        private static bool IsSorted(int[] list)
        {
            for (int i = 0; i < list.Length - 1; i++)
            {
                if (list[i] > list[i + 1])
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Случайное перемешивание
        /// </summary>
        private static int[] RandomPermutation(int[] array)
        {
            Random random = new Random();
            var n = array.Length;
            while (n > 1)
            {
                n--;
                var i = random.Next(n + 1);
                var temp = array[i];
                array[i] = array[n];
                array[n] = temp;
            }

            return array;
        }

        /// <summary>
        /// Случайная сортировка (Худшая сортировка)
        /// </summary>
        public static int[] RandomSort(int[] array)
        {
            int[] help = array.ToArray();

            while (!IsSorted(help))
            {
                help = RandomPermutation(help);
            }

            return help;
        }

        private static void Swap<T>(ref T a, ref T b)
        {
            var help = a;
            a = b;
            b = help;
        }

        /// <summary>
        /// Сортировка пузырьком
        /// </summary>
        public static int[] BubbleSort(int[] array)
        {
            var help = array.ToArray();

            var len = help.Length;
            for (var i = 1; i < len; i++)
            {
                for (var j = 0; j < len - i; j++)
                {
                    if (help[j] > help[j + 1])
                    {
                        Swap(ref help[j], ref help[j + 1]);
                    }
                }
            }

            return help;
        }

        /// <summary>
        /// Сортировка перемешиванием
        /// </summary>
        public static int[] ShakerSort(int[] array)
        {
            var help = array.ToArray();

            for (var i = 0; i < help.Length / 2; i++)
            {
                var swapFlag = false;
                //проход слева направо
                for (var j = i; j < help.Length - i - 1; j++)
                {
                    if (help[j] > help[j + 1])
                    {
                        Swap(ref help[j], ref help[j + 1]);
                        swapFlag = true;
                    }
                }

                //проход справа налево
                for (var j = help.Length - 2 - i; j > i; j--)
                {
                    if (help[j - 1] > help[j])
                    {
                        Swap(ref help[j - 1], ref help[j]);
                        swapFlag = true;
                    }
                }

                //если обменов не было выходим
                if (!swapFlag)
                {
                    break;
                }
            }

            return help;
        }

        /// <summary>
        /// Сортировка вставками (Не работает)
        /// </summary>
        public static int[] InsertionSort(int[] array) //Неправильно!!!
        {
            var help = array.ToArray();

            for (var i = 1; i < help.Length; i++)
            {
                var key = help[i];
                var j = i;
                while (j > 1 && help[j - 1] > key)
                {
                    Swap(ref help[j - 1], ref help[j]);
                    j--;
                }

                help[j] = key;
            }

            return help;
        }

        /// <summary>
        /// Сортировка по частям (часть)
        /// </summary>
        public static int[] StoogeSort(int[] array, int startIndex, int endIndex)
        {
            var help = array.ToArray();

            if (help[startIndex] > help[endIndex])
            {
                Swap(ref help[startIndex], ref help[endIndex]);
            }

            if (endIndex - startIndex > 1)
            {
                var len = (endIndex - startIndex + 1) / 3;
                StoogeSort(help, startIndex, endIndex - len);
                StoogeSort(help, startIndex + len, endIndex);
                StoogeSort(help, startIndex, endIndex - len);
            }

            return help;
        }

        /// <summary>
        /// Сортировка по частям (полностью) - Медленная
        /// </summary>
        public static int[] StoogeSort(int[] array)
        {
            var help = array.ToArray();

            return StoogeSort(help, 0, help.Length - 1);
        }

        /// <summary>
        /// Получение индекса максимального элемента подмассива
        /// </summary>
        private static int IndexOfMax(int[] array, int n)
        {
            int result = 0;
            for (var i = 1; i <= n; ++i)
            {
                if (array[i] > array[result])
                {
                    result = i;
                }
            }

            return result;
        }

        /// <summary>
        /// Переворачивает массив
        /// </summary>
        private static void Flip(int[] array, int end)
        {
            for (var start = 0; start < end; start++, end--)
            {
                var temp = array[start];
                array[start] = array[end];
                array[end] = temp;
            }
        }

        /// <summary>
        /// Блинная сортировка
        /// </summary>
        public static int[] PancakeSort(int[] array)
        {
            var help = array.ToArray();

            for (var subArrayLength = help.Length - 1; subArrayLength >= 0; subArrayLength--)
            {
                //получаем позицию максимального элемента подмассива
                var indexOfMax = IndexOfMax(help, subArrayLength);
                if (indexOfMax != subArrayLength)
                {
                    //переворот массива до индекса максимального элемента
                    //максимальный элемент подмассива расположится вначале
                    Flip(help, indexOfMax);
                    //переворот всего подмассива
                    Flip(help, subArrayLength);
                }
            }

            return help;
        }

        /// <summary>
        /// Сортировка Шелла
        /// </summary>
        public static int[] ShellSort(int[] array)
        {
            var help = array.ToArray();

            // расстояние между элементами, которые сравниваются
            var d = help.Length / 2;
            while (d >= 1)
            {
                for (var i = d; i < help.Length; i++)
                {
                    var j = i;
                    while (j >= d && help[j - d] > help[j])
                    {
                        Swap(ref help[j], ref help[j - d]);
                        j -= d;
                    }
                }

                d /= 2;
            }

            return help;
        } //Подозрительно быстро, но тесты вроде как правильные

        //метод для слияния массивов
        private static void Merge(int[] array, int lowIndex, int middleIndex, int highIndex)
        {
            var left = lowIndex;
            var right = middleIndex + 1;
            var tempArray = new int[highIndex - lowIndex + 1];
            var index = 0;

            while ((left <= middleIndex) && (right <= highIndex))
            {
                if (array[left] < array[right])
                {
                    tempArray[index] = array[left];
                    left++;
                }
                else
                {
                    tempArray[index] = array[right];
                    right++;
                }

                index++;
            }

            for (var i = left; i <= middleIndex; i++)
            {
                tempArray[index] = array[i];
                index++;
            }

            for (var i = right; i <= highIndex; i++)
            {
                tempArray[index] = array[i];
                index++;
            }

            for (var i = 0; i < tempArray.Length; i++)
            {
                array[lowIndex + i] = tempArray[i];
            }
        }

        private static int[] MergeSort(int[] array, int lowIndex, int highIndex)
        {
            if (lowIndex < highIndex)
            {
                var middleIndex = (lowIndex + highIndex) / 2;
                MergeSort(array, lowIndex, middleIndex);
                MergeSort(array, middleIndex + 1, highIndex);
                Merge(array, lowIndex, middleIndex, highIndex);
            }

            return array;
        }

        /// <summary>
        /// Сортировка слиянием
        /// </summary>
        public static int[] MergeSort(int[] array)
        {
            var help = array.ToArray();

            return MergeSort(help, 0, help.Length - 1);
        } //Проверить

        //метод поиска позиции минимального элемента подмассива, начиная с позиции n
        static int IndexOfMin(int[] array, int n)
        {
            int result = n;
            for (var i = n; i < array.Length; ++i)
            {
                if (array[i] < array[result])
                {
                    result = i;
                }
            }

            return result;
        }

        /// <summary>
        /// Сортировка выбором (Не работает на больших объемах данных, переполнение памяти из-за рекурсии)
        /// </summary>
        public static int[] SelectionSort(int[] array, int currentIndex = 0)
        {
            var help = array.ToArray();

            if (currentIndex == help.Length)
                return help;

            var index = IndexOfMin(help, currentIndex);
            if (index != currentIndex)
            {
                Swap(ref help[index], ref help[currentIndex]);
            }

            return SelectionSort(help, currentIndex + 1);
        } //Переделать без рекурсии

        //метод возвращающий индекс опорного элемента
        private static int Partition(int[] array, int minIndex, int maxIndex)
        {
            var pivot = minIndex - 1;
            for (var i = minIndex; i < maxIndex; i++)
            {
                if (array[i] < array[maxIndex])
                {
                    pivot++;
                    Swap(ref array[pivot], ref array[i]);
                }
            }

            pivot++;
            Swap(ref array[pivot], ref array[maxIndex]);
            return pivot;
        }

        /// <summary>
        /// Быстрая сортировка (действительно одна из быстрейших)
        /// </summary>
        private static int[] QuickSort(int[] array, int minIndex, int maxIndex)
        {
            if (minIndex >= maxIndex)
                return array;

            var pivotIndex = Partition(array, minIndex, maxIndex);
            QuickSort(array, minIndex, pivotIndex - 1);
            QuickSort(array, pivotIndex + 1, maxIndex);

            return array;
        }

        /// <summary>
        /// Быстрая сортировка
        /// </summary>
        public static int[] QuickSort(int[] array)
        {
            var help = array.ToArray();

            return QuickSort(help, 0, help.Length - 1);
        }

        /// <summary>
        /// Гномья сортировка
        /// </summary>
        public static int[] GnomeSort(int[] array)
        {
            var help = array.ToArray();

            var index = 1;
            var nextIndex = index + 1;

            while (index < help.Length)
            {
                if (help[index - 1] < help[index])
                {
                    index = nextIndex;
                    nextIndex++;
                }
                else
                {
                    Swap(ref help[index - 1], ref help[index]);
                    index--;
                    if (index == 0)
                    {
                        index = nextIndex;
                        nextIndex++;
                    }
                }
            }

            return help;
        }

        /// <summary>
        /// Сортировка бинарным деревом (Не работает на больших объемах данных, переполнение памяти из-за рекурсии)
        /// </summary>
        public static int[] TreeSort(int[] array)
        {
            var help = array.ToArray();

            var treeNode = new TreeNode(help[0]);
            for (int i = 1; i < help.Length; i++)
            {
                treeNode.Insert(new TreeNode(help[i]));
            }

            return treeNode.Transform();
        }

        private static int GetNextStep(int s)
        {
            s = s * 1000 / 1247; //Уже высчитаный коэф
            return s > 1 ? s : 1;
        }

        /// <summary>
        /// Сортировка расчёской (Может потягаться с быстрой сортировкой)
        /// </summary>
        public static int[] CombSort(int[] array)
        {
            var help = array.ToArray();

            var arrayLength = help.Length;
            var currentStep = arrayLength - 1;

            while (currentStep > 1)
            {
                for (int i = 0; i + currentStep < help.Length; i++)
                {
                    if (help[i] > help[i + currentStep])
                    {
                        Swap(ref help[i], ref help[i + currentStep]);
                    }
                }

                currentStep = GetNextStep(currentStep);
            }

            //сортировка пузырьком
            for (var i = 1; i < arrayLength; i++)
            {
                var swapFlag = false;
                for (var j = 0; j < arrayLength - i; j++)
                {
                    if (help[j] > help[j + 1])
                    {
                        Swap(ref help[j], ref help[j + 1]);
                        swapFlag = true;
                    }
                }

                if (!swapFlag)
                {
                    break;
                }
            }

            return help;
        }

        /// <summary>
        /// Сортировка подсчётом (эффективна только при небольших разбросах данных)
        /// </summary>
        public static int[] BasicCountingSort(int[] array)
        {
            var help = array.ToArray();
            var k = help.Max();

            var count = new int[k + 1];
            foreach (var t in help)
            {
                count[t]++;
            }

            var index = 0;
            for (var i = 0; i < count.Length; i++)
            {
                for (var j = 0; j < count[i]; j++)
                {
                    help[index] = i;
                    index++;
                }
            }

            return help;
        } //Проверить, подозрительно быстро
    }
}