using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Diagnostics.Windows;
using BenchmarkDotNet.Running;

namespace BenchmarkTests.Tests
{
    [Config(typeof(Config))]
    public class StringTests
    {
        private class Config : ManualConfig
        {
            public Config() => AddDiagnoser(MemoryDiagnoser.Default, new EtwProfiler());
        }

        [Params(42, 1337)]
        public int Data;

        [Benchmark] public string Format() => string.Format("{0:x2}", Data);
        [Benchmark] public string Interpolate() => $"{Data:x2}";
        [Benchmark] public string InterpolateExplicit() => $"{Data.ToString("x2")}";
    }

    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<StringTests>();
        }
    }
}