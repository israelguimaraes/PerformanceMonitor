using System;
using System.Collections.Generic;

namespace PerformanceMonitor.Web.Models.DotnetCounters
{
    public class CollectResult
    {
        public string TargetProcess { get; set; }
        public string StartTime { get; set; }
        public IEnumerable<Event> Events { get; set; }
    }

    public class Event
    {
        public const string CpuUsage = "CPU Usage (%)";
        public const string GcHeapSize = "GC Heap Size (MB)";
        public const string WorkingSetMB = "Working Set (MB)";

        public DateTime Timestamp { get; set; }
        public string Provider { get; set; }
        public string Name { get; set; }
        public string CounterType { get; set; }
        public int Value { get; set; }

        public bool IsCpuUsage => Name == CpuUsage;
        public bool IsWorkingSet => Name == WorkingSetMB;
        public bool IsGcHeapSize => Name == GcHeapSize;
    }
}
