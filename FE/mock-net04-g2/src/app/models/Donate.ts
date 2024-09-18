import { Campaign } from "./Campaign";
import { User } from "./User";

export interface Donate{
    id : number;
    userId : number;
    user : User;
    campaignId : number;
    campaign: Campaign;
    amount : number;
    date: Date;
}