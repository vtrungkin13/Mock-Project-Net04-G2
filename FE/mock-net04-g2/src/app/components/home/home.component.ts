import { Component, OnInit } from '@angular/core';
import { HeaderComponent } from '../shared/header/header.component';
import { CommonModule } from '@angular/common';
import { Campaign } from '../../models/Campaign';
import { CampaignService } from '../../services/campaign-service/campaign.service';
import { Router, RouterLink } from '@angular/router';
import { CampaignStatusEnum } from '../../models/enum/CampaignStatusEnum';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [HeaderComponent, CommonModule, RouterLink],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent implements OnInit {

  campaigns: Campaign[] = [];
  totalCount: number = 0; // tổng số items (có thể thay đổi tùy theo search/filter)
  page: number = 1; // page hiện tại
  pageSize: number = 9; //để mặc định = 9
  totalPage : number = 0; // tổng số pages có thể có (totalCount / pagesize) làm tròn lên

  currentStatus = -1; // nếu currentStatus = -1 thì không áp dụng filter

  constructor(
    private router: Router,
    private campaignService: CampaignService
  ) {}

  ngOnInit(): void {
    this.getCampaignByPage();
  }

  redirectToCampaignDeital(campaignId: number) {
    this.router.navigateByUrl(`/campaign-detail/${campaignId}`);
  }

  getCampaignByPage(){
    this.campaignService.getCampaignByPage(this.page).subscribe({
      next:(response)=>{
        this.campaigns = response.body;
        this.getTotalCampaignCount();
        this.totalPage = Math.ceil(this.totalCount/this.pageSize);
      },
      error:(error)=>{
        alert(error.message);
      }
    })
  }

  getTotalCampaignCount(){
    this.campaignService.getTotalCampaignCount().subscribe({
      next:(response) =>{
        this.totalCount = response.body;
        this.totalPage = Math.ceil(this.totalCount/this.pageSize);
      },
      error:(error)=>{
        alert(error.message);
      }
    })
  }

  calculateTotalAmount(campaign:Campaign) : number{ // trả về tổng số tiền đã quyên góp đc 
    return (campaign.donations || []).reduce((total: number, donate: any) => total + donate.amount, 0);
  }

  calculatePercentage(campaign: Campaign): number { // % quyên góp so vs mục tiêu
    const totalAmount = this.calculateTotalAmount(campaign);
    const targetAmount = campaign.limitation || 1; // tránh chia cho 0
    return (totalAmount / targetAmount) * 100;
  }

  calculateRemainingDays(campaign:Campaign):number{
    if (!campaign.endDate) { // tránh null
      return 0;
    }
    const today = new Date();
    const endDate = new Date(campaign.endDate);
    // Calculate the difference in time
    const timeDiff = endDate.getTime() - today.getTime();
    // Convert time difference from milliseconds to days
    const daysDiff = Math.ceil(timeDiff / (1000 * 3600 * 24));
    return daysDiff >= 0 ? daysDiff : 0; // Ensure non-negative value
  }

  previousPage(){
    if(this.page > 1){
      this.page -= 1;
      this.getCampaignByPage();
      
    }

    window.location.href = '#campaigns-container'
  }

  nextPage(){
    if(this.page <this.totalPage){
      this.page += 1;
      this.getCampaignByPage();
    }

    window.location.href = '#campaigns-container'
  }

  changePage(i:number){
    if(i >= 1 && i <= this.totalPage){
      this.page = i;
      this.getCampaignByPage();
    }

    window.location.href = '#campaigns-container'
  }

  filter(status : number){
    switch(status){
      case -1 :{
        this.currentStatus = -1;
        this.page = 1;
        this.getCampaignByPage();
        break;
      }
      case 1:{
        this.currentStatus = 1;
        this.page = 1;
        this
      }
    }
  }
  
}
