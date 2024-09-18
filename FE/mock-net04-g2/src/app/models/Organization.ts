import { Cooperate } from "./Cooperate";

export interface Organization {
    id: number;
    name: string;
    phone: string;
    logo: string;
    cooperations : Cooperate;
}