import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  OnChanges,
  SimpleChanges,
} from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { MultipleSelectComponent } from '../../../shared/multiple-select/multiple-select.component';
import { Campaign } from '../../../../models/Campaign';
import { Organization } from '../../../../models/Organization';
import { CampaignService } from '../../../../services/campaign-service/campaign.service';
import { OrganizationService } from '../../../../services/organization-service/organization.service';
import { Cooperate } from '../../../../models/Cooperate';
import { UpdateCampaignRequest } from '../../../../models/UpdateCampaignRequest';

@Component({
  selector: 'app-update-campaign',
  standalone: true,
  imports: [FormsModule, MultipleSelectComponent],
  templateUrl: './update-campaign.component.html',
  styleUrls: ['./update-campaign.component.scss'],
})
export class UpdateCampaignComponent implements OnInit, OnChanges {
  @Input() campaign?: Campaign;
  @Output() onCampaignUpdated = new EventEmitter<void>();

  selectedOrganizations: any[] = [];
  organizations: Organization[] = [];

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
        console.log(error);
      },
    });
  }

  onSubmit(form: NgForm) {
    if (!this.campaign) {
      console.error('Campaign is not defined');
      return;
    }

    const startDate = new Date(form.value.startDate);
    const endDate = new Date(form.value.endDate);

    if (isNaN(startDate.getTime()) || isNaN(endDate.getTime())) {
      console.error('Định dạng ngày sai!');
      return;
    }

    const selectedOrganizationIds = this.selectedOrganizations.map(
      (org) => org.id
    );

    const updateRequest: UpdateCampaignRequest = {
      title: form.value.title,
      description: form.value.description,
      content: form.value.content,
      image: form.value.image,
      startDate: form.value.startDate,
      endDate: form.value.endDate,
      limitation: form.value.limitation,
      status: form.value.status,
      organizationIds: selectedOrganizationIds,
    };

    this.campaignService
      .updateCampaign(this.campaign.id, updateRequest)
      .subscribe({
        next: (response) => {
          console.log('Chiến dịch được cập nhật thành công:', response);
          this.onCampaignUpdated.emit();
        },
        error: (error) => {
          console.error('Chiến dịch cập nhật thất bại:', error);
          if (error.status === 401) {
            console.error('Vui lòng đăng nhập lại.');
          }
        },
      });
  }
}
