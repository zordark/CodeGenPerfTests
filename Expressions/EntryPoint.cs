using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

namespace Expressions
{
    public class EntryPoint
    {
        private static void Main(string[] args)
        {
            BenchmarkRunner.Run<Test1>(
                ManualConfig.Create(DefaultConfig.Instance)
                    .AddJob(Job.ShortRun.WithPlatform(Platform.X64).WithJit(Jit.LegacyJit).WithRuntime(ClrRuntime.Net48))
                    .AddJob(Job.ShortRun.WithPlatform(Platform.X64).WithJit(Jit.RyuJit).WithRuntime(ClrRuntime.Net48))
                    .AddJob(Job.ShortRun.WithPlatform(Platform.X64).WithJit(Jit.RyuJit).WithRuntime(CoreRuntime.Core31))
                    .AddJob(Job.ShortRun.WithPlatform(Platform.X64).WithJit(Jit.RyuJit).WithRuntime(CoreRuntime.Core60))
                    .AddJob(Job.ShortRun.WithPlatform(Platform.X64).WithJit(Jit.Llvm).WithRuntime(new MonoRuntime("mono", "c:\\Program Files\\Mono\\bin\\mono.exe")))
            );

            BenchmarkRunner.Run<Test2>(
                ManualConfig.Create(DefaultConfig.Instance)
                    .AddJob(Job.ShortRun.WithPlatform(Platform.X64).WithJit(Jit.LegacyJit).WithRuntime(ClrRuntime.Net48))
                    .AddJob(Job.ShortRun.WithPlatform(Platform.X64).WithJit(Jit.RyuJit).WithRuntime(ClrRuntime.Net48))
                    .AddJob(Job.ShortRun.WithPlatform(Platform.X64).WithJit(Jit.RyuJit).WithRuntime(CoreRuntime.Core31))
                    .AddJob(Job.ShortRun.WithPlatform(Platform.X64).WithJit(Jit.RyuJit).WithRuntime(CoreRuntime.Core60))
                    .AddJob(Job.ShortRun.WithPlatform(Platform.X64).WithJit(Jit.Llvm).WithRuntime(new MonoRuntime("mono", "c:\\Program Files\\Mono\\bin\\mono.exe")))
            );
        }
    }

    public class TestClassA
    {
        public string S { get; set; }

        public decimal Y { get; set; }

        public TestClassB[] ArrayB { get; set; }
    }

    public class TestClassB
    {
        public string S { get; set; }

        public int? X { get; set; }

        public decimal Y { get; set; }

        public TestClassC C { get; set; }
    }

    public class TestClassC
    {
        public TestClassD[] ArrayD { get; set; }
    }

    public class TestClassD
    {
        public int X { get; set; }
    }
}