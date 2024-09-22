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
import { UpdateCampaignRequest } from '../../../models/UpdateCampaignRequest';
import { Cooperate } from '../../../models/Cooperate';
import { MultipleSelectComponent } from '../../shared/multiple-select/multiple-select.component';
import { Organization } from '../../../models/Organization';

@Component({
  selector: 'app-extend-campaign',
  standalone: true,
  imports: [CommonModule, FormsModule, MultipleSelectComponent],
  templateUrl: './extend-campaign.component.html',
  styleUrl: './extend-campaign.component.scss',
})
export class ExtendCampaignComponent implements OnInit, OnChanges {
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
        console.error('Error fetching organizations:', error);
      },
    });
  }

  onSubmit(form: NgForm) {
    if (!this.campaign) {
      console.error('Campaign is not defined');
      return;
    }

    // Trích xuất chỉ các ID từ selectedOrganizations để gửi lên backend
    const selectedOrganizationIds = this.selectedOrganizations.map(
      (org) => org.id
    );

    const updateRequest: UpdateCampaignRequest = {
      limitation: form.value.limitation,
      endDate: form.value.endDate,
      organizationIds: selectedOrganizationIds,
      // Thêm các trường khác nếu cần
    };

    this.campaignService
      .extendCampaign(this.campaign.id, updateRequest)
      .subscribe({
        next: (response) => {
          console.log('Chiến dịch được cập nhật thành công:', response);
          this.onCampaignUpdated.emit();
          // Có thể đóng modal hoặc thực hiện các hành động khác
        },
        error: (error) => {
          console.error('Chiến dịch cập nhật thất bại:', error);
          if (error.status === 401) {
            console.error('Vui lòng đăng nhập lại.');
            // Có thể chuyển hướng người dùng đến trang đăng nhập
          } else {
            // Xử lý các lỗi khác
          }
        },
      });
  }
}
