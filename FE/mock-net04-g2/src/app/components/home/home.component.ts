import { Component, OnInit } from '@angular/core';
import { HeaderComponent } from '../shared/header/header.component';
import { CommonModule } from '@angular/common';
import { Campaign } from '../../models/Campaign';
import { CampaignService } from '../../services/campaign-service/campaign.service';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [HeaderComponent, CommonModule, RouterLink],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent implements OnInit {
  campaigns!: Campaign[];

  constructor(
    private router: Router,
    private campaignService: CampaignService
  ) {}

  ngOnInit(): void {
    this.campaigns = this.campaignService.getAllCampaigns();
  }

  redirectToCampaignDeital(campaignId: number) {
    this.router.navigateByUrl(`/campaign-detail/${campaignId}`);
  }
}
