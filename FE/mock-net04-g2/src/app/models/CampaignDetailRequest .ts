export interface CampaignDetailRequest {
  title: string;
  description: string;
  image: string;
  content: string;
  startDate: string; // Use ISO date string format
  endDate: string; // Use ISO date string format
  limitation: number;
  organizationIds: number[];
}
