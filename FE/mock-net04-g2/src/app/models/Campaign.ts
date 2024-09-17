import { CampaignStatusEnum } from "./enum/CampaignStatusEnum";
import { Organization } from "./Organization";
import { User } from "./User";

export interface Campaign {
    id: number;
    title: string;
    description: string;
    image: string;
    content: string;
    startDate: Date;
    endDate: Date;
    limitation: number;
    status: CampaignStatusEnum;
    code: string;
    createdAt: Date;
    donators: User[]
    organizations: Organization[]
}
