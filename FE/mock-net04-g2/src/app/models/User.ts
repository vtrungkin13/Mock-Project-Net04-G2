import { RoleEnum } from "./enum/RoleEnum";

export interface User {
    id: number;
    name: string;
    email: string;
    phone: string;
    dob: Date;
    role: RoleEnum;
}