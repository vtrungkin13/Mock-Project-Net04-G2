import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Campaign } from '../../../models/Campaign';
import { FormsModule, NgForm } from '@angular/forms';
import { MultipleSelectComponent } from '../../shared/multiple-select/multiple-select.component';
import { CampaignService } from '../../../services/campaign-service/campaign.service';
import { OrganizationService } from '../../../services/organization-service/organization.service';
import { ToastComponent } from '../../shared/toast/toast.component';

@Component({
  selector: 'app-modify-campaign',
  standalone: true,
  imports: [FormsModule, MultipleSelectComponent, ToastComponent],
  templateUrl: './modify-campaign.component.html',
  styleUrl: './modify-campaign.component.scss',
})
export class ModifyCampaignComponent implements OnInit {
  @Input() campaign?: Campaign;
  @Output() onCampaignAdded = new EventEmitter();

  organizationIds: number[] = [];
  organizations: any[] = [];

  // 1: success, 2: fail, 3: loading
  modifyCampaignStatus: number = 0;
  modifyCampaignMessage: string = '';
  modifyCampaignShowToast: boolean = false;

  constructor(
    private campaignService: CampaignService,
    private organizationService: OrganizationService
  ) {}

  ngOnInit(): void {
    this.getOrganizations();
  }

  onSubmit(form: NgForm) {
    if (form.valid) {
      const modifyCampaignData = {
        ...form.value,
        organizationIds: this.organizationIds,
      };

      this.campaignService.addCampaign(modifyCampaignData).subscribe({
        next: (response) => {
          this.handleShowToast(1, 'Tạo chiến dịch thành công!');
          this.onCampaignAdded.emit();
        },
        error: (error) => {
          this.handleShowToast(2, error.message);
        },
      });
    }
  }

  handleShowToast(status: number, message: string) {
    this.modifyCampaignShowToast = true;
    this.modifyCampaignStatus = status;
    this.modifyCampaignMessage = message;
    setTimeout(() => {
      this.modifyCampaignShowToast = false;
    }, 3000);
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
