import { CampaignStatusEnum } from './enum/CampaignStatusEnum';

export interface UpdateCampaignRequest {
  title: string;
  description: string;
  content: string;
  image: string;
  startDate: Date;
  endDate: Date;
  limitation: number;
  status: CampaignStatusEnum; //
  organizationIds: number[];
}
