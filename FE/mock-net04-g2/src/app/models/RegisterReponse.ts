import { User } from './User';

export interface RegisterResponse {
    status: number;
    body: {
        token: string;
        expireAt: Date;
        user: User;
  };
}