using System;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

namespace SwitchChars
{
    public class EntryPoint
    {
        public static void Main(string[] args)
        {
            //new EntryPoint().Test();
            //return;

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
            keysSetup = 5 * 1000 + 10;
            SetUp();
            Console.WriteLine(BunchOfIfs());
            Console.WriteLine(Dictionary());
            Console.WriteLine(UnrolledBinarySearch());
            Console.WriteLine(BinarySearch());
            Console.WriteLine(ArrayOfSegments());
            Console.WriteLine(ArrayOfSegmentsWithBS());
            Console.WriteLine(PerfectHashtable());
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
        public int BunchOfIfs()
        {
            return runner.Run_BunchOfIfs();
        }

        [Benchmark]
        public int UnrolledBinarySearch()
        {
            return runner.Run_UnrolledBinarySearch();
        }

        [Benchmark]
        public int BinarySearch()
        {
            return runner.Run_BinarySearch();
        }

        [Benchmark]
        public int ArrayOfSegments()
        {
            return runner.Run_ArrayOfSegments();
        }

        [Benchmark]
        public int ArrayOfSegmentsWithBS()
        {
            return runner.Run_ArrayOfSegmentsWithBS();
        }

        [Benchmark]
        public int PerfectHashtable()
        {
            return runner.Run_PerfectHashtable();
        }

        [GlobalSetup]
        public void SetUp()
        {
            runner = new Runner(keysSetup / 1000, keysSetup % 1000, 123456);
        }

        //[Params((1 * 1000) + 1, (1 * 1000) + 2, (1 * 1000) + 3, (1 * 1000) + 4, (1 * 1000) + 5, (1 * 1000) + 6, (1 * 1000) + 7, (1 * 1000) + 8, (1 * 1000) + 9, (1 * 1000) + 10, (2 * 1000) + 10, (5 * 1000) + 10, (10 * 1000) + 10)]
        //[Params((1 * 1000) + 11, (1 * 1000) + 12, (1 * 1000) + 13, (1 * 1000) + 14, (1 * 1000) + 15, (1 * 1000) + 16, (1 * 1000) + 17, (1 * 1000) + 18, (1 * 1000) + 19, (1 * 1000) + 20)]
        //[Params(10 * 1000 + 10, 25 * 1000 + 10, 50 * 1000 + 10, 100 * 1000 + 10)]
        //[Params(10 * 1000 + 10, 10 * 1000 + 25, 10 * 1000 + 50, 10 * 1000 + 100, 10 * 1000 + 200)]
        //[Params((1 * 1000) + 1, (1 * 1000) + 2, (1 * 1000) + 3, (1 * 1000) + 4, (1 * 1000) + 5, (1 * 1000) + 6, (1 * 1000) + 7, (1 * 1000) + 8, (1 * 1000) + 9, (1 * 1000) + 10, (1 * 1000) + 11, (1 * 1000) + 12, (1 * 1000) + 13, (1 * 1000) + 14, (1 * 1000) + 15, (1 * 1000) + 16, (1 * 1000) + 17, (1 * 1000) + 18, (1 * 1000) + 19, (1 * 1000) + 20)]
        //[Params((1 * 1000) + 157)]
        //[Params(5 * 1000 + 10, 10 * 1000 + 10, 15 * 1000 + 10, 20 * 1000 + 10, 25 * 1000 + 10)]
        [Params(1 * 1000 + 3)]
        public int keysSetup;

        private Runner runner;
    }
}