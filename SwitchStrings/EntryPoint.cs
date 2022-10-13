using System;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

namespace SwitchStrings
{
    public delegate bool TryGetValueDelegate<T>(string key, out T value);

    public class EntryPoint
    {
        public static void Main()
        {
            //            var test = new EntryPoint();
            //            test.numberOfKeys = 1000;
            //            test.Setup();
            //            test.Test();
            //            return;

            BenchmarkRunner.Run<EntryPoint>(
                ManualConfig.Create(DefaultConfig.Instance)
                    .AddJob(Job.ShortRun.WithPlatform(Platform.X64).WithJit(Jit.LegacyJit).WithRuntime(ClrRuntime.Net48))
                    .AddJob(Job.ShortRun.WithPlatform(Platform.X64).WithJit(Jit.RyuJit).WithRuntime(ClrRuntime.Net48))
                    .AddJob(Job.ShortRun.WithPlatform(Platform.X64).WithJit(Jit.RyuJit).WithRuntime(CoreRuntime.Core31))
                    .AddJob(Job.ShortRun.WithPlatform(Platform.X64).WithJit(Jit.RyuJit).WithRuntime(CoreRuntime.Core60))
                    .AddJob(Job.ShortRun.WithPlatform(Platform.X64).WithJit(Jit.Llvm).WithRuntime(new MonoRuntime("mono", "c:\\Program Files\\Mono\\bin\\mono.exe")))
            );

        }

        public void Test()
        {
            Console.WriteLine(Dictionary());
            Console.WriteLine(BunchOfIffs());
            Console.WriteLine(PerfectHashtable());
            Console.WriteLine(PerfectHashtableWithRoslynHash());
            Console.WriteLine(Trie());
            Console.WriteLine(UnrolledBSWithRoslynHash());
            Console.WriteLine(UnrolledBSWithFastHash());
        }

        // The execution time of this method should be subtracted from execution time other benchmarks methods - this method is kind of setup and is not part of algorithm
        [Benchmark(Baseline = true)]
        public int Empty()
        {
            return runner.Run_Empty();
        }

        [Benchmark]
        public int Dictionary()
        {
            return runner.Run_Dictionary();
        }

        [Benchmark]
        public int BunchOfIffs()
        {
            return runner.Run_BunchOfIfs();
        }

        [Benchmark]
        public int PerfectHashtable()
        {
            return runner.Run_PerfectHashtable();
        }

        [Benchmark]
        public int PerfectHashtableWithRoslynHash()
        {
            return runner.Run_PerfectHashtableWithRoslynHash();
        }

        [Benchmark]
        public int Trie()
        {
            return runner.Run_Trie();
        }

        [Benchmark]
        public int UnrolledBSWithRoslynHash()
        {
            return runner.Run_UnrolledBSWithRoslynHash();
        }

        [Benchmark]
        public int UnrolledBSWithFastHash()
        {
            return runner.Run_UnrolledBSWithFastHash();
        }

        [GlobalSetup]
        public void Setup()
        {
            runner = new Runner(numberOfKeys, 283767);
        }

        //[Params(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 16, 17, 18, 19, 20, 25, 35, 50, 100, 150, 200)]
        //[Params(100, 200, 300, 500)]
        [Params(1)]
        public int numberOfKeys;

        private Runner runner;
    }
}