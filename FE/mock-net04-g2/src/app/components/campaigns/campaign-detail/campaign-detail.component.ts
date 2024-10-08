import { CommonModule } from '@angular/common';
import {
  Component,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
  SimpleChanges,
} from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { CampaignService } from '../../../services/campaign-service/campaign.service';
import { Campaign } from '../../../models/Campaign';
import { CampaignChartComponent } from "../campaign-chart/campaign-chart.component";
import { AuthService } from '../../../services/auth-service/auth.service';
import { User } from '../../../models/User';
import { ModifyCampaignComponent } from '../modify-campaign/modify-campaign.component';
import { ExtendCampaignComponent } from '../extend-campaign/extend-campaign.component';
import { DonateFormComponent } from '../../donate-form/donate-form.component';
import { UpdateCampaignComponent } from '../update-campaign/update-campaign/update-campaign.component';
import { Organization } from '../../../models/Organization';
import { Cooperate } from '../../../models/Cooperate';
import { OrganizationService } from '../../../services/organization-service/organization.service';
import { Router } from '@angular/router';
import { CampaignStatusEnum } from '../../../models/enum/CampaignStatusEnum';
import { ApiResponse } from '../../../models/ApiResponse';
import { UpdateCampaignRequest } from '../../../models/UpdateCampaignRequest';
import { CampaignDetailResponse } from '../../../models/CampaignDetailResponse ';
import { ToastComponent } from '../../shared/toast/toast.component';

@Component({
  selector: 'app-campaign-detail',
  standalone: true,
  imports: [
    CommonModule,
    ModifyCampaignComponent,
    ExtendCampaignComponent,
    DonateFormComponent,
    UpdateCampaignComponent,
    RouterLink,CampaignChartComponent,
    ToastComponent,
  ],
  templateUrl: './campaign-detail.component.html',
  styleUrls: ['./campaign-detail.component.scss'], // Updated key
})
export class CampaignDetailComponent implements OnInit, OnChanges {
  campaignId!: number;
  campaign!: Campaign;
  topDonors: any[] = []; // To store the top donors
  user?: User;
  
  @Output() onCampaignUpdated = new EventEmitter<void>();

  campaignDetailStatus: number = 0;
  campaignDetailMessage: string = '';
  campaignDetailShowToast: boolean = false;

  selectedOrganizations: number[] = [];
  organizations: Organization[] = [];

  constructor(
    private route: ActivatedRoute,
    private campaignService: CampaignService,
    private authService: AuthService,
    private organizationService: OrganizationService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.campaignId = Number.parseInt(
      this.route.snapshot.paramMap.get('campaignId') as string
    );

    
    if (this.campaignId && this.campaignId >= 0) {
      this.campaignService.getCampaignDetail(this.campaignId).subscribe({
        next: (response) => {
          this.campaign = response.body;
          this.calculateTopDonors(); 
          this.extractSelectedOrganizations(this.campaign.cooperations);
        },
        error: (error) => {
          console.log(error.message);
        },
      });
    }
    this.user = this.authService.getUser();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['campaign'] && this.campaign && this.organizations.length > 0) {
      this.extractSelectedOrganizations(this.campaign.cooperations);
    }
  }

  extractSelectedOrganizations(cooperations: Cooperate[]) {
    this.selectedOrganizations = cooperations.map(
      (coop) => coop.organizationId
    );
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
  
  getOrganizations() {
    this.organizationService.getOrganizations().subscribe({
      next: (response) => {
        this.organizations = response.body;
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  getOrganizationIds(organizationIds: number[]) {
    console.log('Selected Organization IDs:', organizationIds); // Debugging
    this.selectedOrganizations = organizationIds;
  }

  onDeleteCampaign() {
    if (!this.campaign) {
      console.error('Campaign is not defined');
      return;
    }

    // Hiển thị xác nhận trước khi xoá
    if (confirm('Bạn có chắc chắn muốn xoá chiến dịch này?')) {
      this.campaignService.deleteCampaign(this.campaign.id).subscribe({
        next: (response) => {
          this.handleShowToast(1, 'Chiến dịch đã được xoá thành công!');
          this.onCampaignUpdated.emit();
          this.router.navigateByUrl('/');
        },
        error: (error) => {
          this.handleShowToast(2, error.message); 
        },
      });
    }
  }

  onCampaignUpdatedHandler() {
    // Cập nhật lại dữ liệu sau khi chiến dịch được cập nhật hoặc xoá
    this.campaignService.getCampaignDetail(this.campaignId).subscribe({
      next: (response) => {
        this.campaign = response.body;
        this.extractSelectedOrganizations(this.campaign.cooperations);
      },
      error: (error) => {
        console.log(error.message);
      },
    });
  }

  onChangeStatusCampaign(id: number) {
    const newStatus: CampaignStatusEnum = CampaignStatusEnum.CLOSED;

    this.campaignService.changeStatusCampaign(id, newStatus).subscribe({
      next: (response: CampaignDetailResponse) => {
        console.log('Thành công:', response);
        this.campaign.status = newStatus;
        this.onCampaignUpdated.emit();
      },
      error: (error: any) => {
        console.error(
          'Đã xảy ra lỗi khi thay đổi trạng thái chiến dịch:',
          error
        );
        if (error.error && error.error.errors) {
          console.error('Chi tiết lỗi:', error.error.errors);
        } else {
          console.error('Chi tiết lỗi:', error.error);
        }
      },
    });
  }

  handleShowToast(status: number, message: string) {
    this.campaignDetailShowToast = true;
    this.campaignDetailStatus = status;
    this.campaignDetailMessage = message;
    setTimeout(() => {
      this.campaignDetailShowToast = false;
    }, 3000);
  }
}
