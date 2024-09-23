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
import { FormsModule, NgForm } from '@angular/forms';
import { Campaign } from '../../../models/Campaign';
import { CampaignService } from '../../../services/campaign-service/campaign.service';
import { OrganizationService } from '../../../services/organization-service/organization.service';
import { ExtendCampaignRequest } from '../../../models/ExtendCampaignRequest';
import { Cooperate } from '../../../models/Cooperate';
import { MultipleSelectComponent } from '../../shared/multiple-select/multiple-select.component';
import { Organization } from '../../../models/Organization';
import { ToastComponent } from '../../shared/toast/toast.component';

@Component({
  selector: 'app-extend-campaign',
  standalone: true,
  imports: [CommonModule, FormsModule, MultipleSelectComponent, ToastComponent],
  templateUrl: './extend-campaign.component.html',
  styleUrl: './extend-campaign.component.scss',
})
export class ExtendCampaignComponent implements OnInit, OnChanges {
  @Input() campaign?: Campaign;
  @Output() onCampaignUpdated = new EventEmitter<void>();

  selectedOrganizations: any[] = [];
  organizations: Organization[] = [];

  // 1: success, 2: fail, 3: loading
  extendCampaignStatus: number = 0;
  extendCampaignMessage: string = '';
  extendCampaignShowToast: boolean = false;

  constructor(
    private campaignService: CampaignService,
    private organizationService: OrganizationService
  ) {}

  ngOnInit(): void {
    this.getOrganizations();
    if (this.campaign) {
      this.extractSelectedOrganizations(this.campaign.cooperations);
    }
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['campaign'] && this.campaign && this.organizations.length > 0) {
      this.extractSelectedOrganizations(this.campaign.cooperations);
    }
  }
  extractSelectedOrganizations(cooperations: Cooperate[]) {
    this.selectedOrganizations = cooperations.map((coop) => coop.organization);
  }

  getOrganizations() {
    this.organizationService.getOrganizations().subscribe({
      next: (response) => {
        this.organizations = response.body;
      },
      error: (error) => {
        console.error('Error fetching organizations:', error);
      },
    });
  }

  handleShowToast(status: number, message: string) {
    this.extendCampaignShowToast = true;
    this.extendCampaignStatus = status;
    this.extendCampaignMessage = message;
    setTimeout(() => {
      this.extendCampaignShowToast = false;
    }, 3000);
  }

  onSubmit(form: NgForm) {
    if (form.valid) {
      if (!this.campaign) {
        console.error('Campaign is not defined');
        return;
      }

      const selectedOrganizationIds = this.selectedOrganizations.map(
        (org) => org.id
      );

      const updateRequest: ExtendCampaignRequest = {
        limitation: form.value.limitation,
        endDate: form.value.endDate,
        organizationIds: selectedOrganizationIds,
      };

      this.campaignService
        .extendCampaign(this.campaign.id, updateRequest)
        .subscribe({
          next: (response) => {
            this.handleShowToast(1, 'Chiến dịch được cập nhật thành công!');
            this.onCampaignUpdated.emit();
          },
          error: (error) => {            
            this.handleShowToast(2, error.error.error);
          },
        });
    }
  }
}
