import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CampaignService } from '../../../services/campaign-service/campaign.service';
import { Campaign } from '../../../models/Campaign';
import { AuthService } from '../../../services/auth-service/auth.service';
import { User } from '../../../models/User';
import { ModifyCampaignComponent } from '../modify-campaign/modify-campaign.component';
import { ExtendCampaignComponent } from '../extend-campaign/extend-campaign.component';

@Component({
  selector: 'app-campaign-detail',
  standalone: true,
  imports: [CommonModule, ModifyCampaignComponent, ExtendCampaignComponent],
  templateUrl: './campaign-detail.component.html',
  styleUrls: ['./campaign-detail.component.scss'], // Updated key
})
export class CampaignDetailComponent implements OnInit {
  campaignId!: number;
  campaign!: Campaign;
  user!: User;

  constructor(
    private route: ActivatedRoute,
    private campaignService: CampaignService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.campaignId = Number.parseInt(
      this.route.snapshot.paramMap.get('campaignId') as string
    );
    if (this.campaignId && this.campaignId >= 0) {
      this.campaignService.getCampaignDetail(this.campaignId).subscribe({
        next: (response) => {
          console.log(response);
          this.campaign = response.body;
        },
        error: (error) => {
          console.log(error.message);
        },
      });
    }
    this.user = this.authService.getUser();
  }

  calculateTotalAmount(campaign: Campaign): number {
    // trả về tổng số tiền đã quyên góp đc
    return (campaign.donations || []).reduce(
      (total: number, donate: any) => total + donate.amount,
      0
    );
  }

  calculatePercentage(campaign: Campaign): number {
    // % quyên góp so vs mục tiêu
    const totalAmount = this.calculateTotalAmount(campaign);
    const targetAmount = campaign.limitation || 1; // tránh chia cho 0
    return (totalAmount / targetAmount) * 100;
  }

  calculateRemainingDays(campaign: Campaign): number {
    // đếm số ngày còn lại
    if (!campaign.endDate) {
      // tránh null
      return 0;
    }
    const today = new Date();
    const endDate = new Date(campaign.endDate);
    const timeDiff = endDate.getTime() - today.getTime();
    const daysDiff = Math.ceil(timeDiff / (1000 * 3600 * 24));
    return daysDiff >= 0 ? daysDiff : 0;
  }
}
