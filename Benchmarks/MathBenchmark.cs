using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using SternsMath.Models.Math;
using Math = System.Math;

namespace Benchmarks
{
    [MemoryDiagnoser]
    [RankColumn]
    public class MathBenchmark
    {
        [Params(32)]
        public int Pow { get; set; }

        [Benchmark]
        public void PowFor()
        {
            int result = 1;

            for (int i = 0; i < Pow; i++)
            {
                result *= 2;
            }
        }

        [Benchmark]
        public void PowMath()
        {
            var result = Math.Pow(2, Pow);
        }

        [Benchmark]
        public void PowQuick()
        {
            var result = 2L.QuickPower(Pow);
        }
    }
}