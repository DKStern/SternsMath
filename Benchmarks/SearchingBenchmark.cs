using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using SternsMath.Models.Sortings;

namespace Benchmarks
{
    [MemoryDiagnoser]
    [RankColumn]
    [BaselineColumn]
    public class SearchingBenchmark
    {
        private int[] _mas;
        private int find;

        [Params(10, 100)]
        public int Size { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            var list = new List<int>();
            for (int i = 0; i < Size; i++)
            {
                var r = new Random();
                list.Add(r.Next());
            }

            var random = new Random();
            find = list[random.Next(0, 11)];
            _mas = list.ToArray();
        }

        [Benchmark]
        public void BubbleSort()
        {
            Sorting.BubbleSort(_mas);
        }

        [Benchmark]
        public void MergeSort()
        {
            Sorting.MergeSort(_mas);
        }

        [Benchmark(Baseline = true)]
        public void QuickSort()
        {
            Sorting.QuickSort(_mas);
        }

        [Benchmark]
        public void InsertionSort()
        {
            Sorting.InsertionSort(_mas);
        }

        [Benchmark]
        public void ShakerSort()
        {
            Sorting.ShakerSort(_mas);
        }

        [Benchmark]
        public void ShellSort()
        {
            Sorting.ShellSort(_mas);
        }

        [Benchmark]
        public void GnomeSort()
        {
            Sorting.GnomeSort(_mas);
        }

        [Benchmark]
        public void CombSort()
        {
            Sorting.CombSort(_mas);
        }
    }
}