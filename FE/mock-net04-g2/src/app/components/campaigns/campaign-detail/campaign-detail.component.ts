import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-campaign-detail',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './campaign-detail.component.html',
  styleUrl: './campaign-detail.component.scss',
})
export class CampaignDetailComponent implements OnInit {
  campaignId!: number;

  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.campaignId = Number.parseInt(
      this.route.snapshot.paramMap.get('campaignId') as string
    );
  }
}
