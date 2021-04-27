import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import * as Highcharts from 'highcharts';

@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {

    performanceData: PerformanceData;
    updateFlag = false;

    cpuChart = Highcharts;
    cpuChartOptions = {
        chart: {
            type: "spline"
        },
        title: {
            text: "CPU Usage (%)"
        },
        subtitle: {
            text: "Informations from dotnet-counters monitor"
        },
        xAxis: {
            categories: []
        },
        yAxis: {
            title: {
                text: "CPU %"
            }
        },
        tooltip: {

            valueSuffix: "%"
        },
        series: [
            {
                name: 'CPU',
                data: []
            }
        ]
    };

    workingSetChart = Highcharts;
    workingSetChartOptions = {
        chart: {
            type: "spline"
        },
        title: {
            text: "Memory (MB)"
        },
        subtitle: {
            text: "Informations from dotnet-counters monitor"
        },
        xAxis: {
            categories: []
        },
        yAxis: {
            title: {
                text: "MB"
            }
        },
        tooltip: {

            valueSuffix: "MB"
        },
        series: [
            {
                name: 'GC Heap Size (MB)',
                data: []
            },
            {
                name: 'Working Set (MB)',
                data: []
            }
        ]
    };

    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
        this.http.get<PerformanceData>(this.baseUrl + 'dashboard').subscribe(result => {
            this.performanceData = result;

            this.buildCpuReport(result);
            this.buildWorkingSetReport(result);
            this.buildGcHeapReport(result);

            this.updateFlag = true;
        })
    }

    private buildCpuReport(result: PerformanceData) {
        this.cpuChartOptions.xAxis.categories = result.cpu.map(c => c.timestamp.toString());
        this.cpuChartOptions.series[0].data = result.cpu.map(c => c.percentage);
    }

    private buildWorkingSetReport(result: PerformanceData) {
        this.workingSetChartOptions.xAxis.categories = result.workingSet.map(c => c.timestamp.toString());
        this.workingSetChartOptions.series[0].data = result.workingSet.map(c => c.mb);
    }

    private buildGcHeapReport(result: PerformanceData) {
        this.workingSetChartOptions.series[1].data = result.gcHeapSize.map(c => c.mb);
    }
}

interface PerformanceData {
    cpu: CpuUsage[];
    workingSet: WorkingSet[];
    gcHeapSize: GcHeapSize[];
}

interface CpuUsage {
    timestamp: Date;
    percentage: number;
}

interface WorkingSet {
    timestamp: Date;
    mb: number;
}

interface GcHeapSize {
    timestamp: Date;
    mb: number;
}