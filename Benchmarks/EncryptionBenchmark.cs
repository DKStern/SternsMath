using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using SternsMath.Models.Encryption;

namespace Benchmarks
{
    [MemoryDiagnoser]
    [RankColumn]
    public class EncryptionBenchmark
    {
        const string str = "Выяснению, кто же и кого травмировал, предшествовал душераздирающий конфликт в Мурине. Местная жительница поставила детскую коляску на газон в соседнем дворе — и лишилась ногтя. Всё потому, что это не понравилось даме, которая считает двор «своим» и изгоняет чужаков.";

        [Benchmark]
        public void XOR()
        {
            str.XOREncryptDecrypt(str);
        }

      
        public void XORAsync()
        {
            str.XOREncryptDecryptAsync(str);
        }

        [Benchmark]
        public void Atbash()
        {
            str.Atbash();
        }

  
        public void AtbashAsync()
        {
            str.AtbashAsync();
        }
    }
}