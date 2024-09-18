import { Campaign } from "./Campaign";
import { Organization } from "./Organization";

export interface Cooperate {
    id: number;
    organizationId: number;
    organization: Organization;
    campaignId: number;
    campaign: Campaign;
}