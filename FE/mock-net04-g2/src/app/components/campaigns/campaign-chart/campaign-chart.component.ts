import { Component, Input, OnInit, HostListener } from '@angular/core';
import { Chart, registerables } from 'chart.js';
import { Donate } from '../../../models/Donate';

Chart.register(...registerables); // Register all components

@Component({
  selector: 'app-campaign-chart',
  standalone: true,
  templateUrl: './campaign-chart.component.html',
  styleUrls: ['./campaign-chart.component.scss']
})
export class CampaignChartComponent implements OnInit {
  @Input() donations?: Donate[];
  sortedDonations?: Donate[] = [];
  public dailyChart: any;
  public cumulativeChart: any;

  ngOnInit() {
    // Sort donations by date
    this.sortedDonations = [...(this.donations || [])].sort((a, b) => new Date(a.date).getTime() - new Date(b.date).getTime());

    // If there are more than 50 donations, group them by month
    if (this.sortedDonations.length >= 50) {
      const groupedDonations = this.groupByMonth(this.sortedDonations);
      this.createDailyChart(groupedDonations, true); // Pass `true` to indicate monthly grouping
      this.createCumulativeChart(groupedDonations, true);
    } else {
      this.createDailyChart(this.sortedDonations, false); // Use daily data
      this.createCumulativeChart(this.sortedDonations, false);
    }
  }

  // Group donations by month if necessary
  groupByMonth(sortedDonations: Donate[] | undefined) {
    const grouped: { [month: string]: number } = {};
    sortedDonations?.forEach(donation => {
      const date = new Date(donation.date);
      const month = new Intl.DateTimeFormat('en-GB', { month: 'short', year: 'numeric' }).format(date); // e.g., "Jan 2024"
      if (!grouped[month]) {
        grouped[month] = 0;
      }
      grouped[month] += donation.amount;
    });

    // Convert the grouped object into an array format
    return Object.entries(grouped).map(([month, amount]) => ({
      date: month,
      amount
    }));
  }

  createDailyChart(data: any[], isGroupedByMonth: boolean) {
    const labels = data.map(d => d.date);
    const donationAmounts = data.map(d => d.amount);
  
    this.dailyChart = new Chart('dailyCanvas', {
      type: 'bar',
      data: {
        labels: labels,
        datasets: [
          {
            label: isGroupedByMonth ? 'Số tiền quyên góp theo tháng' : 'Số tiền quyên góp mỗi ngày',
            data: donationAmounts,
            borderColor: 'rgba(255, 105, 180, 1)',
            backgroundColor: 'rgba(255, 105, 180, 0.2)',
            borderWidth: 1
          }
        ]
      },
      options: {
        responsive: true,
        maintainAspectRatio: false,
        scales: {
          x: {
            title: {
              display: true,
              text: isGroupedByMonth ? 'Tháng' : 'Ngày'
            },
            ticks: {
              callback: function(value: any, index: number, values: any) {
                const date = new Date(labels[index]);
                return new Intl.DateTimeFormat('en-GB').format(date); // Format date to dd/MM/yyyy
              }
            }
          },
          y: {
            beginAtZero: true,
            title: {
              display: true,
              text: 'VND'
            }
          }
        }
      }
    });
  }
  
  createCumulativeChart(data: any[], isGroupedByMonth: boolean) {
    const labels = data.map(d => d.date);
    const cumulativeAmounts: number[] = [];
    data.reduce((total, donation) => {
      const newTotal = total + donation.amount;
      cumulativeAmounts.push(newTotal);
      return newTotal;
    }, 0);
  
    this.cumulativeChart = new Chart('cumulativeCanvas', {
      type: 'line',
      data: {
        labels: labels,
        datasets: [
          {
            label: isGroupedByMonth ? 'Tổng số tiền quyên góp theo tháng' : 'Tổng số tiền quyên góp hàng ngày',
            data: cumulativeAmounts,
            borderColor: 'rgba(75, 192, 192, 1)',
            backgroundColor: 'rgba(75, 192, 192, 0.2)',
            fill: true,
            borderWidth: 2
          }
        ]
      },
      options: {
        responsive: true,
        maintainAspectRatio: false,
        scales: {
          x: {
            title: {
              display: true,
              text: isGroupedByMonth ? 'Tháng' : 'Ngày'
            },
            ticks: {
              callback: function(value: any, index: number, values: any) {
                const date = new Date(labels[index]);
                return new Intl.DateTimeFormat('en-GB').format(date); // Format date to dd/MM/yyyy
              }
            }
          },
          y: {
            beginAtZero: true,
            title: {
              display: true,
              text: 'VND'
            }
          }
        }
      }
    });
  }
  

  // Listen to window resize events
  @HostListener('window:resize')
  onResize() {
    if (this.dailyChart) {
      this.dailyChart.resize();
    }
    if (this.cumulativeChart) {
      this.cumulativeChart.resize();
    }
  }
}
