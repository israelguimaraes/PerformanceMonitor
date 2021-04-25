import { Component, OnInit } from '@angular/core';
import * as Highcharts from 'highcharts';

@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

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
            categories: ["Jan", "Feb", "Mar", "Apr", "May", "Jun",
                "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"]
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
                data: [7.0, 6.9, 9.5, 14.5, 18.2, 21.5, 25.2, 26.5, 23.3, 18.3, 13.9, 9.6]
            }
        ]
    };

    constructor() { }

    ngOnInit() {
    }

}
