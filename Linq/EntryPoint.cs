using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

namespace Linq
{
    public class EntryPoint
    {
        public static void Main(string[] args)
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

            BenchmarkRunner.Run<Test3>(
                ManualConfig.Create(DefaultConfig.Instance)
                    .AddJob(Job.ShortRun.WithPlatform(Platform.X64).WithJit(Jit.LegacyJit).WithRuntime(ClrRuntime.Net48))
                    .AddJob(Job.ShortRun.WithPlatform(Platform.X64).WithJit(Jit.RyuJit).WithRuntime(ClrRuntime.Net48))
                    .AddJob(Job.ShortRun.WithPlatform(Platform.X64).WithJit(Jit.RyuJit).WithRuntime(CoreRuntime.Core31))
                    .AddJob(Job.ShortRun.WithPlatform(Platform.X64).WithJit(Jit.RyuJit).WithRuntime(CoreRuntime.Core60))
                    .AddJob(Job.ShortRun.WithPlatform(Platform.X64).WithJit(Jit.Llvm).WithRuntime(new MonoRuntime("mono", "c:\\Program Files\\Mono\\bin\\mono.exe")))
            );
        }
    }
}