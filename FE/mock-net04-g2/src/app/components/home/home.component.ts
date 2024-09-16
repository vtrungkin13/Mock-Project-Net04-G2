import { Component, OnInit } from '@angular/core';
import { HeaderComponent } from '../shared/header/header.component';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { Campaign } from '../../models/Campaign';
import { CampaignService } from '../../services/campaign-service/campaign.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [HeaderComponent, CommonModule, RouterLink],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent implements OnInit {
  campaigns!: Campaign[]

  constructor(private campaignService: CampaignService) {}

  ngOnInit(): void {
    this.campaigns = this.campaignService.getAllCampaigns();
  }

  redirectToCampaignDeital() {
    console.log('Hello');
  }
}
