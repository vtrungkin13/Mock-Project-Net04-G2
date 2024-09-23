import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CampaignService } from '../../../services/campaign-service/campaign.service';
import { Campaign } from '../../../models/Campaign';
import { CampaignChartComponent } from "../campaign-chart/campaign-chart.component";

@Component({
  selector: 'app-campaign-detail',
  standalone: true,
  imports: [CommonModule, CampaignChartComponent],
  templateUrl: './campaign-detail.component.html',
  styleUrls: ['./campaign-detail.component.scss'],  // Updated key
})
export class CampaignDetailComponent implements OnInit {
  campaignId!: number;
  campaign?: Campaign;
  topDonors: any[] = []; // To store the top donors

  constructor(private route: ActivatedRoute, private campaignService: CampaignService) { }

  ngOnInit(): void {
    this.campaignId = Number.parseInt(
      this.route.snapshot.paramMap.get('campaignId') as string
    );

    
    if (this.campaignId && this.campaignId >= 0) {
      this.campaignService.getCampaignDetail(this.campaignId).subscribe({
        next: (response) => {
          this.campaign = response.body;
          this.calculateTopDonors(); 
        },
        error: (error) => {
          console.log(error.message);
        }
      });
    }
  }

  calculateTotalAmount(campaign: Campaign): number { // trả về tổng số tiền đã quyên góp đc 
    return (campaign.donations || []).reduce((total: number, donate: any) => total + donate.amount, 0);
  }

  calculatePercentage(campaign: Campaign): number { // % quyên góp so vs mục tiêu
    const totalAmount = this.calculateTotalAmount(campaign);
    const targetAmount = campaign.limitation || 1; // tránh chia cho 0
    return (totalAmount / targetAmount) * 100;
  }

  calculateRemainingDays(campaign: Campaign): number { // đếm số ngày còn lại
    if (!campaign.endDate) { // tránh null
      return 0;
    }
    const today = new Date();
    const endDate = new Date(campaign.endDate);
    const timeDiff = endDate.getTime() - today.getTime();
    const daysDiff = Math.ceil(timeDiff / (1000 * 3600 * 24));
    return daysDiff >= 0 ? daysDiff : 0;
  }
  // Method to calculate total donations per user and get top 10 donors
  calculateTopDonors() {
    const donorMap: { [key: string]: number } = {};

    // Group donations by user and sum the amounts
    (this.campaign?.donations || []).forEach(donation => {
      const userName = donation.user.name;
      const amount = donation.amount;

      if (!donorMap[userName]) {
        donorMap[userName] = 0;
      }
      donorMap[userName] += amount;
    });

    // Convert the map to an array and sort it by total donation amount
    this.topDonors = Object.entries(donorMap)
      .map(([name, totalAmount]) => ({ name, totalAmount }))
      .sort((a, b) => b.totalAmount - a.totalAmount)
      .slice(0, 10); // Get top 10 donors
  }
}
