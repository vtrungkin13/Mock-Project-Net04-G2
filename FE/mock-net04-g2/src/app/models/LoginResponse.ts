import { User } from './User';

export interface LoginResponse {
  status: number;
  body: {
    token: string;
    expireAt: Date;
    user: User;
  };
}
