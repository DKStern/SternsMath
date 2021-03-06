using SternsMath.Models;
using SternsMath.Models.Encryption;
using SternsMath.Models.Math;
using SternsMath.Models.Searchings;
using SternsMath.Models.Sortings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using SternsMath.Models.NumberSystems;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = 255;
            for (int i = 2; i <= 16; i++)
            {
                var num = a.ToBase(i);
                Console.WriteLine($@"{i:00}: {num}");
            }
        }
        
        private static async void TestEncryption()
        {
            string str = "Выяснению, кто же и кого травмировал, предшествовал душераздирающий конфликт в Мурине. Местная жительница поставила детскую коляску на газон в соседнем дворе — и лишилась ногтя. Всё потому, что это не понравилось даме, которая считает двор «своим» и изгоняет чужаков.";
            Console.WriteLine(str);

            Stopwatch sw = new Stopwatch();
            sw.Start();
            var res = str.XOREncryptDecrypt("Привет");
            sw.Stop();
            Console.WriteLine(res);
            Console.WriteLine(sw.Elapsed);

            sw.Restart();
            res = await str.XOREncryptDecryptAsync("Привет");
            sw.Stop();
            Console.WriteLine(res);
            Console.WriteLine(sw.Elapsed);

            res = res.XOREncryptDecrypt("Привет");
            Console.WriteLine(res);
        }

        private static void TestMath()
        {
            Console.WriteLine("Введите число:");
            var num = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine("Введите степень:");
            var pow = Convert.ToInt32(Console.ReadLine());
            
            var res = num.GetTrialDivision();

            Console.WriteLine($@"Простые множители: {num} = {string.Join("*", res)}");

            var result = num.QuickPower(pow);
            Console.WriteLine($@"{num}^{pow} = {result}");
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

            var array = Sorting.QuickSort(list.ToArray());

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
                var res = Sorting.RandomSort(array);
                watch.Stop();
                //dic.Add("Случайная", res);
                Console.WriteLine($@"Случайная: {watch.Elapsed}");
            });
            //tasks.Add(rnd);

            var bubble = new Task(() =>
            {
                var watch = new Stopwatch();
                watch.Start();
                var res = Sorting.BubbleSort(array);
                watch.Stop();
                //dic.Add("Пузырьком", res);
                Console.WriteLine($@"Пузырьком: {watch.Elapsed}");
            });
            tasks.Add(bubble);

            var shaker = new Task(() =>
            {
                var watch = new Stopwatch();
                watch.Start();
                var res = Sorting.ShakerSort(array);
                watch.Stop();
                //dic.Add("Перемешиванием", res);
                Console.WriteLine($@"Перемешиванием: {watch.Elapsed}");
            });
            tasks.Add(shaker);

            var insertion = new Task(() =>
            {
                var watch = new Stopwatch();
                watch.Start();
                var res = Sorting.InsertionSort(array);
                watch.Stop();
                //dic.Add("Вставками", res);
                Console.WriteLine($@"Вставками: {watch.Elapsed}");
            });
            //tasks.Add(insertion);

            var stooge = new Task(() =>
            {
                var watch = new Stopwatch();
                watch.Start();
                var res = Sorting.StoogeSort(array);
                watch.Stop();
                //dic.Add("По частям", res);
                Console.WriteLine($@"По частям: {watch.Elapsed}");
            });
            //tasks.Add(stooge);

            var pancake = new Task(() =>
            {
                var watch = new Stopwatch();
                watch.Start();
                var res = Sorting.PancakeSort(array);
                watch.Stop();
                //dic.Add("Блинная", res);
                Console.WriteLine($@"Блинная: {watch.Elapsed}");
            });
            tasks.Add(pancake);

            var shell = new Task(() =>
            {
                var watch = new Stopwatch();
                watch.Start();
                var res = Sorting.ShellSort(array);
                watch.Stop();
                //dic.Add("Шелла", res);
                Console.WriteLine($@"Шелла: {watch.Elapsed}");
            });
            tasks.Add(shell);

            var merge = new Task(() =>
            {
                var watch = new Stopwatch();
                watch.Start();
                var res = Sorting.MergeSort(array);
                watch.Stop();
                //dic.Add("Слиянием", res);
                Console.WriteLine($@"Слиянием: {watch.Elapsed}");
            });
            tasks.Add(merge);

            var selection = new Task(() =>
            {
                var watch = new Stopwatch();
                watch.Start();
                var res = Sorting.SelectionSort(array);
                watch.Stop();
                //dic.Add("Выбором", res);
                Console.WriteLine($@"Выбором: {watch.Elapsed}");
            });
            //tasks.Add(selection);

            var quick = new Task(() =>
            {
                var watch = new Stopwatch();
                watch.Start();
                var res = Sorting.QuickSort(array);
                watch.Stop();
                //dic.Add("Быстрая", res);
                Console.WriteLine($@"Быстрая: {watch.Elapsed}");
            });
            tasks.Add(quick);

            var gnome = new Task(() =>
            {
                var watch = new Stopwatch();
                watch.Start();
                var res = Sorting.GnomeSort(array);
                watch.Stop();
                //dic.Add("Гномья", res);
                Console.WriteLine($@"Гномья: {watch.Elapsed}");
            });
            tasks.Add(gnome);

            var tree = new Task(() =>
            {
                var watch = new Stopwatch();
                watch.Start();
                var res = Sorting.TreeSort(array);
                watch.Stop();
                //dic.Add("Деревом", res);
                Console.WriteLine($@"Деревом: {watch.Elapsed}");
            });
            //tasks.Add(tree);

            var comb = new Task(() =>
            {
                var watch = new Stopwatch();
                watch.Start();
                var res = Sorting.CombSort(array);
                watch.Stop();
                //dic.Add("Расчёсткой", res);
                Console.WriteLine($@"Расчёсткой: {watch.Elapsed}");
            });
            tasks.Add(comb);

            var basic = new Task(() =>
            {
                var watch = new Stopwatch();
                watch.Start();
                var res = Sorting.BasicCountingSort(array);
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
