import { Component, OnInit } from '@angular/core';
import { HeaderComponent } from '../shared/header/header.component';
import { CommonModule } from '@angular/common';
import { Campaign } from '../../models/Campaign';
import { CampaignService } from '../../services/campaign-service/campaign.service';
import { Router, RouterLink } from '@angular/router';
import { CampaignStatusEnum } from '../../models/enum/CampaignStatusEnum';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [HeaderComponent, CommonModule, RouterLink, FormsModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent implements OnInit {

  // campaignsResponse$: Observable<any>;
  CampaignStatusEnum = CampaignStatusEnum;
  campaigns: Campaign[] = [];
  totalCount: number = 0; // tổng số campaigns (có thể thay đổi tùy theo search/filter)
  page: number = 1; // page hiện tại
  pageSize: number = 9; //để mặc định = 9
  totalPage: number = 0; // tổng số pages có thể có (totalCount chia pagesize) làm tròn lên

  // Param cho chức năng filter/search
  statusFilter?: CampaignStatusEnum;
  codeSearch: string = "";
  phoneSearch: string = "";
  loading: boolean = false; // To track the loading state

  constructor(private router: Router, private campaignService: CampaignService) { }

  ngOnInit(): void {
    this.getCampaignList();
  }

  redirectToCampaignDeital(campaignId: number) {
    this.router.navigateByUrl(`/campaign-detail/${campaignId}`);
  }

  getCampaignList() { // kết hợp search + filter + paging vào 1 cho đồng nhất
    this.loading = true; // Set loading to true when starting the request
    const focusedElement = document.activeElement;// Save reference to the focused element
    this.campaignService.getCampaigns(
      this.pageSize
      , this.page
      , this.codeSearch
      , this.phoneSearch
      , this.statusFilter
    ).subscribe({
      next: (response) => {
        this.campaigns = response.body;
        this.getTotalCampaignCount();
        this.totalPage = Math.ceil(this.totalCount / this.pageSize);
        this.loading = false; // Set loading to false after data is received

        // Restore focus after data has been loaded
        if (focusedElement) {
          (focusedElement as HTMLElement).focus();
        }
      },
      error: (error) => {
        console.log(error.message);
        this.loading = false; // Set loading to false after data is received
      }
    });
  }

  getTotalCampaignCount() {
    this.campaignService.getCampaignsCount(this.codeSearch
      , this.phoneSearch
      , this.statusFilter
    ).subscribe({
      next: (response) => {
        console.log(response)
        this.totalCount = response.body;
        this.totalPage = Math.ceil(this.totalCount / this.pageSize);
      },
      error: (error) => {
        alert(error.message);
      }
    });
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

  previousPage() { // sang trang tiếp
    if (this.page > 1) {
      this.page -= 1;
      this.getCampaignList();
      window.location.href = '#campaigns-container';
    }
  }

  nextPage() { // quay lại trang trước
    if (this.page < this.totalPage) {
      this.page += 1;
      this.getCampaignList();
      window.location.href = '#campaigns-container';
    }
  }

  changePage(i: number) { // đổi trang theo số thứ tự
    if (i >= 1 && i <= this.totalPage) {
      this.page = i;
      this.getCampaignList();
      window.location.href = '#campaigns-container';
    }
  }

  onStatusChange(status?: CampaignStatusEnum) { // lọc theo status
    this.page = 1;
    this.statusFilter = status;
    this.getCampaignList();
    window.location.href = '#campaigns-container';
  }

  onSearchChange() { // lọc theo mã hoặc số đt
    this.page = 1;
    this.getCampaignList();
    window.location.href = '#campaigns-container';
  }
}
