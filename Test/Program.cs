using Searching.Searchings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using SternsMath.Models;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {

        }

        private static void TestMath()
        {
            Console.WriteLine("Введите число:");
            //var num = Convert.ToUInt64(Console.ReadLine());

            var num = ulong.MaxValue;

            var res = num.GetTrialDivision();

            Console.WriteLine($@"Простые множители: {num} = {string.Join("*", res)}");
        }

        private static void TestSearching()
        {
            var tasks = new List<Task>();
            var list = new List<int>();
            int size = 100000;

            Console.WriteLine($@"Размер массива: {size}");

            for (int i = 0; i < size; i++)
            {
                var r = new Random();
                list.Add(r.Next());
            }

            var random = new Random();
            var find = list[random.Next(0, 11)];

            var array = Sorting.Sortings.Sorting.QuickSort(list.ToArray());

            var linear = new Task(() =>
            {
                var watch = new Stopwatch();
                watch.Start();
                var r = array.LinearSearch(find);
                watch.Stop();
                Console.WriteLine($@"Линейный: {watch.Elapsed}" + Environment.NewLine + $@"Результат: {r}");
            });
            tasks.Add(linear);

            var binary = new Task(() =>
            {
                var watch = new Stopwatch();
                watch.Start();
                var r = array.BinarySearch(find);
                watch.Stop();
                Console.WriteLine($@"Бинарный: {watch.Elapsed}" + Environment.NewLine + $@"Результат: {r}");
            });
            tasks.Add(binary);

            tasks.ForEach(x => x.Start());

            Task.WaitAll(tasks.ToArray());
        }

        private static void Write(Dictionary<string, int[]> dic)
        {
            foreach (var i in dic)
            {
                Console.WriteLine($@"{i.Key}: {string.Join(" ", i.Value)}");
            }
            Console.WriteLine();
        }

        private static void TestSortings()
        {
            var tasks = new List<Task>();
            var list = new List<int>();
            int size = 100000;

            Console.WriteLine($@"Размер массива: {size}");

            for (int i = 0; i < size; i++)
            {
                var r = new Random();
                list.Add(r.Next(10000));
            }

            var array = list.ToArray();
            var dic = new Dictionary<string, int[]>();

            var rnd = new Task(() =>
            {
                var watch = new Stopwatch();
                watch.Start();
                var res = Sorting.Sortings.Sorting.RandomSort(array);
                watch.Stop();
                //dic.Add("Случайная", res);
                Console.WriteLine($@"Случайная: {watch.Elapsed}");
            });
            //tasks.Add(rnd);

            var bubble = new Task(() =>
            {
                var watch = new Stopwatch();
                watch.Start();
                var res = Sorting.Sortings.Sorting.BubbleSort(array);
                watch.Stop();
                //dic.Add("Пузырьком", res);
                Console.WriteLine($@"Пузырьком: {watch.Elapsed}");
            });
            tasks.Add(bubble);

            var shaker = new Task(() =>
            {
                var watch = new Stopwatch();
                watch.Start();
                var res = Sorting.Sortings.Sorting.ShakerSort(array);
                watch.Stop();
                //dic.Add("Перемешиванием", res);
                Console.WriteLine($@"Перемешиванием: {watch.Elapsed}");
            });
            tasks.Add(shaker);

            var insertion = new Task(() =>
            {
                var watch = new Stopwatch();
                watch.Start();
                var res = Sorting.Sortings.Sorting.InsertionSort(array);
                watch.Stop();
                //dic.Add("Вставками", res);
                Console.WriteLine($@"Вставками: {watch.Elapsed}");
            });
            //tasks.Add(insertion);

            var stooge = new Task(() =>
            {
                var watch = new Stopwatch();
                watch.Start();
                var res = Sorting.Sortings.Sorting.StoogeSort(array);
                watch.Stop();
                //dic.Add("По частям", res);
                Console.WriteLine($@"По частям: {watch.Elapsed}");
            });
            //tasks.Add(stooge);

            var pancake = new Task(() =>
            {
                var watch = new Stopwatch();
                watch.Start();
                var res = Sorting.Sortings.Sorting.PancakeSort(array);
                watch.Stop();
                //dic.Add("Блинная", res);
                Console.WriteLine($@"Блинная: {watch.Elapsed}");
            });
            tasks.Add(pancake);

            var shell = new Task(() =>
            {
                var watch = new Stopwatch();
                watch.Start();
                var res = Sorting.Sortings.Sorting.ShellSort(array);
                watch.Stop();
                //dic.Add("Шелла", res);
                Console.WriteLine($@"Шелла: {watch.Elapsed}");
            });
            tasks.Add(shell);

            var merge = new Task(() =>
            {
                var watch = new Stopwatch();
                watch.Start();
                var res = Sorting.Sortings.Sorting.MergeSort(array);
                watch.Stop();
                //dic.Add("Слиянием", res);
                Console.WriteLine($@"Слиянием: {watch.Elapsed}");
            });
            tasks.Add(merge);

            var selection = new Task(() =>
            {
                var watch = new Stopwatch();
                watch.Start();
                var res = Sorting.Sortings.Sorting.SelectionSort(array);
                watch.Stop();
                //dic.Add("Выбором", res);
                Console.WriteLine($@"Выбором: {watch.Elapsed}");
            });
            //tasks.Add(selection);

            var quick = new Task(() =>
            {
                var watch = new Stopwatch();
                watch.Start();
                var res = Sorting.Sortings.Sorting.QuickSort(array);
                watch.Stop();
                //dic.Add("Быстрая", res);
                Console.WriteLine($@"Быстрая: {watch.Elapsed}");
            });
            tasks.Add(quick);

            var gnome = new Task(() =>
            {
                var watch = new Stopwatch();
                watch.Start();
                var res = Sorting.Sortings.Sorting.GnomeSort(array);
                watch.Stop();
                //dic.Add("Гномья", res);
                Console.WriteLine($@"Гномья: {watch.Elapsed}");
            });
            tasks.Add(gnome);

            var tree = new Task(() =>
            {
                var watch = new Stopwatch();
                watch.Start();
                var res = Sorting.Sortings.Sorting.TreeSort(array);
                watch.Stop();
                //dic.Add("Деревом", res);
                Console.WriteLine($@"Деревом: {watch.Elapsed}");
            });
            //tasks.Add(tree);

            var comb = new Task(() =>
            {
                var watch = new Stopwatch();
                watch.Start();
                var res = Sorting.Sortings.Sorting.CombSort(array);
                watch.Stop();
                //dic.Add("Расчёсткой", res);
                Console.WriteLine($@"Расчёсткой: {watch.Elapsed}");
            });
            tasks.Add(comb);

            var basic = new Task(() =>
            {
                var watch = new Stopwatch();
                watch.Start();
                var res = Sorting.Sortings.Sorting.BasicCountingSort(array);
                watch.Stop();
                //dic.Add("Подсчётом", res);
                Console.WriteLine($@"Подсчётом: {watch.Elapsed}");
            });
            tasks.Add(basic);

            tasks.ForEach(t => t.Start());

            Task.WaitAll(tasks.ToArray());

            Write(dic);
        }
    }
}
