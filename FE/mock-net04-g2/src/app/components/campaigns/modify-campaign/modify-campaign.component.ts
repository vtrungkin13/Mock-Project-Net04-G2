import { Component, Input } from '@angular/core';
import { Campaign } from '../../../models/Campaign';
import { FormsModule, NgForm } from '@angular/forms';
import { MultipleSelectComponent } from '../../shared/multiple-select/multiple-select.component';

@Component({
  selector: 'app-modify-campaign',
  standalone: true,
  imports: [FormsModule, MultipleSelectComponent],
  templateUrl: './modify-campaign.component.html',
  styleUrl: './modify-campaign.component.scss',
})
export class ModifyCampaignComponent {
  @Input() campaign?: Campaign;

  organizationIds: number[] = [];

  onSubmit(form: NgForm) {
    const modifyCampaignData = {
      ...form.value,
      organizationIds: this.organizationIds,
    };
    console.log(modifyCampaignData);
  }

  getOrganizationIds(organizationIds: number[]) {
    this.organizationIds = organizationIds;
  }
}
