using Microsoft.AspNetCore.Mvc;
using PerformanceMonitor.Web.Models.Dashboard;
using PerformanceMonitor.Web.Models.DotnetCounters;
using PerformanceMonitor.Web.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace PerformanceMonitor.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DashboardController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetReportData()
        {
            var collectResult = GetCollectResult();

            var cpu = GetCpuUsage(collectResult);
            var workingSet = GetWorkingSet(collectResult);
            var gcHeapSize = GetGcHeapSize(collectResult);

            return Ok(new { cpu, workingSet, gcHeapSize });
        }

        private CollectResult GetCollectResult()
        {
            var filePath = @"E:\Projetos\Pessoais\PerformanceMonitor\PerformanceMonitor.API\counter.json";
            var textJson = System.IO.File.ReadAllText(filePath);

            var settings = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            settings.Converters.Add(new DateTimeJsonConverter());

            var collectResult = JsonSerializer.Deserialize<CollectResult>(textJson, settings);
            return collectResult;
        }

        private ICollection<WorkingSet> GetWorkingSet(CollectResult collectResult)
        {
            var groupedByTimestamp = collectResult.Events
               .Where(e => e.IsWorkingSet)
               .GroupBy(g => g.Timestamp)
               .ToList();

            var result = new List<WorkingSet>();
            foreach (var item in groupedByTimestamp)
            {
                foreach (var @event in item)
                    result.Add(new WorkingSet { MB = @event.Value, Timestamp = @event.Timestamp });
            }

            return result;
        }

        private ICollection<CpuUsage> GetCpuUsage(CollectResult collectResult)
        {
            var groupedByTimestamp = collectResult.Events
                .Where(e => e.IsCpuUsage)
                .GroupBy(g => g.Timestamp)
                .ToList();

            var result = new List<CpuUsage>();
            foreach (var item in groupedByTimestamp)
            {
                foreach (var @event in item)
                    result.Add(new CpuUsage { Percentage = @event.Value, Timestamp = @event.Timestamp });
            }

            return result;
        }

        private ICollection<GcHeapSize> GetGcHeapSize(CollectResult collectResult)
        {
            var groupedByTimestamp = collectResult.Events
                .Where(e => e.IsGcHeapSize)
                .GroupBy(g => g.Timestamp)
                .ToList();

            var result = new List<GcHeapSize>();
            foreach (var item in groupedByTimestamp)
            {
                foreach (var @event in item)
                    result.Add(new GcHeapSize { Mb = @event.Value, Timestamp = @event.Timestamp });
            }

            return result;
        }
    }
}
