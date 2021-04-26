import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import * as Highcharts from 'highcharts';

@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

    performanceData: PerformanceData;
    updateFlag = false;

    highcharts = Highcharts;
    chartOptions = {
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

    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
        this.http.get<PerformanceData>(this.baseUrl + 'dashboard').subscribe(result => {
            this.performanceData = result;
            this.chartOptions.xAxis.categories = result.cpu.map(c => c.timestamp.toString());
            this.chartOptions.series[0].data = result.cpu.map(c => c.percentage);
            this.updateFlag = true;
        })
     }

    ngOnInit() {
        
    }
}

interface PerformanceData {
    cpu: CpuUsage[];
    workingSet: WorkingSet[];
}

interface CpuUsage {
    timestamp: Date;
    percentage: number;
}

interface WorkingSet {
    timestamp: Date;
    mb: number;
}