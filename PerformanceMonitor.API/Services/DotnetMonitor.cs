using System;
using System.Diagnostics;

namespace PerformanceMonitor.API.Services
{
    public class DotnetMonitor
    {
        public static readonly int ProcessID = Process.GetCurrentProcess().Id;

        private static readonly string[] _countersTypes = new string[]
        {
            "cpu-usage",
            "gc-heap-size",
            "working-set",
            //"gen-0-gc-count",
            //"gen-1-gc-count",
            //"gen-2-gc-count",
            //"time-in-gc",
            //"gen-0-size ",
            //"gen-1-size ",
            //"gen-2-size ",
            "loh-size",
            //"alloc-rate",
            //"assembly-count",
            //"exception-count",
            //"threadpool-thread-count",
            //"monitor-lock-contention-count",
            //"threadpool-queue-length",
            //"threadpool-completed-items-count",
            //"active-timer-count",
        };
        private static Process _collectorProcess;

        public static void InitializePerformanceMonitor()
        {
            InitializeMonitor();
            //InitializeCollector();
        }

        private static void InitializeMonitor()
        {
            var counters = string.Join(',', _countersTypes);

            var process = Process.Start(@"dotnet-counters.exe", @$"monitor --process-id {ProcessID} --refresh-interval 1 --counters System.Runtime[{counters}]");

            if (process == null || process.HasExited)
                throw new InvalidOperationException("Error - dotnet-counters MONITOR");
        }

        public static void InitializeCollector()
        {
            _collectorProcess = Process.Start(@"dotnet-counters.exe", @$"collect --process-id {ProcessID} --refresh-interval 1 --format json");

            if (_collectorProcess == null || _collectorProcess.HasExited)
                throw new InvalidOperationException("Error - dotnet-counters COLLECT");
        }

        public static void StopCollector()
        {
            _collectorProcess.Kill();
        }
    }
}
