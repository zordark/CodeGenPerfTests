using System;
using System.Reflection;
using System.Text;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

namespace Xsd
{
    public class EntryPoint
    {
        static EntryPoint()
        {
            var encodingProvider = CodePagesEncodingProvider.Instance;
            Encoding.RegisterProvider(encodingProvider);

            encoding = Encoding.GetEncoding(1251);
        }

        private static byte[] ReadResource(string name)
        {
            string resourceName = "Xsd.Data." + name;
            var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
            if(resourceStream == null)
                throw new InvalidOperationException($"Resource '{resourceName}' is not found");
            var content = new byte[resourceStream.Length];
            if(resourceStream.Read(content, 0, content.Length) != content.Length)
                throw new InvalidOperationException($"Unable to read data from resource '{resourceName}'");
            return content;
        }

        public static void Main(string[] args)
        {
            //new EntryPoint().Test();

            BenchmarkRunner.Run<EntryPoint>(
                ManualConfig.Create(DefaultConfig.Instance)
                    .AddExporter(RPlotExporter.Default)
                    .AddJob(Job.ShortRun.WithPlatform(Platform.X64).WithJit(Jit.LegacyJit).WithRuntime(ClrRuntime.Net48))
                    .AddJob(Job.ShortRun.WithPlatform(Platform.X64).WithJit(Jit.RyuJit).WithRuntime(ClrRuntime.Net48))
                    .AddJob(Job.ShortRun.WithPlatform(Platform.X64).WithJit(Jit.RyuJit).WithRuntime(CoreRuntime.Core31))
                    .AddJob(Job.ShortRun.WithPlatform(Platform.X64).WithJit(Jit.RyuJit).WithRuntime(CoreRuntime.Core60))
                    .AddJob(Job.ShortRun.WithPlatform(Platform.X64).WithJit(Jit.Llvm).WithRuntime(new MonoRuntime("mono", "c:\\Program Files\\Mono\\bin\\mono.exe")))
            );
        }

        public void Test()
        {
            var xsd = xsdRunner.Run();
            var inlinedAutomaton = inlinedAutomatonRunner.Run();
            Console.WriteLine("xsd: {0}\r\ninlined automaton: {1}", xsd, inlinedAutomaton);
        }

        // The execution time of this method should be subtracted from execution time other benchmarks methods - this method is kind of setup and is not part of algorithm
        [Benchmark(Baseline = true)]
        public int Scan()
        {
            return scanRunner.Run();
        }

        [Benchmark]
        public int Xsd()
        {
            return xsdRunner.Run();
        }

        [Benchmark]
        public int InlinedAutomaton()
        {
            return inlinedAutomatonRunner.Run();
        }

        // ReSharper disable FieldCanBeMadeReadOnly.Local
        private static byte[] xsd1 = ReadResource("110201.xsd");
        private static byte[] xml1 = ReadResource("110201.xml");
        // ReSharper restore FieldCanBeMadeReadOnly.Local
        public static readonly Encoding encoding;

        private readonly _110210_AutomatonRunner inlinedAutomatonRunner = new _110210_AutomatonRunner(xml1);
        private readonly XsdRunner xsdRunner = new XsdRunner(xml1, xsd1, true);
        private readonly XsdRunner scanRunner = new XsdRunner(xml1, xsd1, false);


    }
}