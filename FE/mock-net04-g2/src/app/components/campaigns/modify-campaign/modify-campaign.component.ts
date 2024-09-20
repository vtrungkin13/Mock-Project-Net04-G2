import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Campaign } from '../../../models/Campaign';
import { FormsModule, NgForm } from '@angular/forms';
import { MultipleSelectComponent } from '../../shared/multiple-select/multiple-select.component';
import { CampaignService } from '../../../services/campaign-service/campaign.service';
import { OrganizationService } from '../../../services/organization-service/organization.service';

@Component({
  selector: 'app-modify-campaign',
  standalone: true,
  imports: [FormsModule, MultipleSelectComponent],
  templateUrl: './modify-campaign.component.html',
  styleUrl: './modify-campaign.component.scss',
})
export class ModifyCampaignComponent implements OnInit {
  @Input() campaign?: Campaign;
  @Output() onCampaignAdded = new EventEmitter();

  organizationIds: number[] = [];
  organizations: any[] = [];

  constructor(
    private campaignService: CampaignService,
    private organizationService: OrganizationService
  ) {}

  ngOnInit(): void {
    this.getOrganizations();
  }

  onSubmit(form: NgForm) {
    const modifyCampaignData = {
      ...form.value,
      organizationIds: this.organizationIds,
    };
    this.campaignService.addCampaign(modifyCampaignData).subscribe({
      next: (response) => {
        console.log('Campaign added successfully:', response);
        this.onCampaignAdded.emit();
      },
      error: (error) => {
        console.error('Error adding campaign:', error);
        if (error.status === 401) {
          console.error('Unauthorized. Please log in.');
        }
      },
    });
  }

  getOrganizationIds(organizationIds: number[]) {
    this.organizationIds = organizationIds;
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
}
