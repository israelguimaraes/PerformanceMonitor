using System;

namespace PerformanceMonitor.Web.Models.Dashboard
{
    public class CpuUsage
    {
        public DateTime Timestamp { get; set; }
        public int Percentage { get; set; }
    }
}
