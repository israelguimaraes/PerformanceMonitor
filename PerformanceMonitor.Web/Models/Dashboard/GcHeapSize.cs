using System;

namespace PerformanceMonitor.Web.Models.Dashboard
{
    public class GcHeapSize
    {
        public DateTime Timestamp { get; set; }
        public int Mb { get; set; }
    }
}
