import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BaseChartDirective } from 'ng2-charts';
import { ChartConfiguration, ChartData, ChartType } from "chart.js";
import { ChartService } from '../../services/chart-service';

@Component({
  selector: 'app-chart',
  imports: [CommonModule, BaseChartDirective],
  templateUrl: './chart.html',
  styleUrl: './chart.scss'
})
export class Chart implements OnInit {
// 柱状图类型
  public barChartType: ChartType = 'bar';

    barChartData: ChartConfiguration['data'] = { labels: [], datasets: [] };


    barChartData_from_redis: ChartConfiguration['data'] = { labels: [], datasets: [] };


  constructor(private chartService: ChartService) {}

  ngOnInit(): void {
    this.getData();

    this.getData_from_redis();
  }


  private getData() {
    this.chartService.getTop3ByModuleAndOperation().subscribe( {
      next: (res) => {
        console.log(res);
        this.barChartData = {
          labels: res.map(item => `${item.module} - ${item.operation}`),
          datasets: [
            {
              label: 'Count',
              data: res.map(item => item.count),
              backgroundColor: [
                'rgba(75, 192, 192, 0.6)',
                'rgba(255, 99, 132, 0.6)',
                'rgba(255, 206, 86, 0.6)'
              ]
            }
          ]
        };
      },
      error: (err) => {
        console.error('请求失败:', err);
      }
    }
    )
  }


  
  private getData_from_redis() {
    this.chartService.getTop3ByModuleAndOperationFromRedis().subscribe( {
      next: (res) => {
        console.log(res);
        this.barChartData_from_redis = {
          labels: res.map(item => `${item.module} - ${item.operation}`),
          datasets: [
            {
              label: 'Count',
              data: res.map(item => item.count),
              backgroundColor: [
                'rgba(75, 192, 192, 0.6)',
                'rgba(255, 99, 132, 0.6)',
                'rgba(255, 206, 86, 0.6)'
              ]
            }
          ]
        };
      },
      error: (err) => {
        console.error('请求失败:', err);
      }
    }
    )
  }


  // 柱状图配置
  public barChartOptions: ChartConfiguration['options'] = {
    responsive: true,
        scales: {
      x: {},
      y: {
        beginAtZero: true,
      }
    }
  };
}
