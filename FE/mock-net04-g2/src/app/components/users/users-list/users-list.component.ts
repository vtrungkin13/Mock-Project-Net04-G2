import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../../../models/User';
import { UserService } from '../../../services/user-service/user.service';
import { FormsModule, NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ApiResponse } from '../../../models/ApiResponse';
import { RoleEnum } from '../../../models/enum/RoleEnum';
import { AuthService } from '../../../services/auth-service/auth.service';

@Component({
  selector: 'app-users-list',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './users-list.component.html',
  styleUrl: './users-list.component.scss',
})
export class UsersListComponent {
  users: User[] = []; // Store fetched users
  searchQuery: string = ''; // Store search input
  page: number = 1;
  pageSize: number = 9;
  totalPage: number = 0;
  totalCount: number = 0;
  loading: boolean = false; // To track the loading state
  thisUser!: User;

  RoleEnum = RoleEnum;

  constructor(private userService: UserService, private authService: AuthService) {
    this.getAllUsers();
    this.thisUser = this.authService.getUser();
  }

  // Handle form submission
  onSubmit(form: NgForm): void {
    if (form.valid) {
      this.searchQuery = form.value.searchQuery; // Capture search query from form input
      this.getAllUsers(); // Fetch users based on search query
    }
  }

  // Fetch all users or users based on the search query
  getAllUsers(): void {
    this.loading = true;
    this.userService
      .findUser(this.pageSize, this.page, this.searchQuery)
      .subscribe({
        next: (response: any) => {
          console.log(response);

          if (response && response.status === 200) {
            this.users = response.body;
            this.getAllUserCount();
            this.loading = false;
          }
        },
        error: (error) => {
          console.log(error.message);
          console.error('Failed to load users:', error);
          this.loading = false;
        },
      });
  }

  getAllUserCount() {
    this.userService.filterUserCount(this.searchQuery).subscribe({
      next: (response: any) => {
        if (response && response.status === 200) {
          this.totalCount = response.body;
          this.totalPage = Math.ceil(this.totalCount / this.pageSize);
        }
      },
      error: (error) => {
        console.error('Failed to load users:', error);
      },
    });
  }

  onInputSearch() {
    this.page = 1;
    this.getAllUsers();
  }

  previousPage() {
    // sang trang tiếp
    if (this.page > 1) {
      this.page -= 1;
      this.getAllUsers();
    }
  }

  changePage(i: number) {
    if (i >= 1 && i <= this.totalPage) {
      this.page = i;
      this.getAllUsers();
    }
  }
  nextPage() {
    if (this.page < this.totalPage) {
      this.page += 1;
      this.getAllUsers();
    }
  }

  onChangeRole(user: User, newRole: RoleEnum): void {
    if (
      !confirm(`Bạn có chắc chắn muốn thay đổi vai trò của ${user.name} không?`)
    ) {
      return;
    }

    if (user.role === newRole) {
      console.log('Vai trò không thay đổi.');
      return;
    }

    this.userService.changeUserRole(user.id, newRole).subscribe({
      next: (response: ApiResponse<string, string>) => {
        if (response.status === 200 && response.body) {
          console.log('Thành công:', response.body);
          user.role = newRole;
        } else {
          console.error('Lỗi:', response.error || 'Thay đổi vai trò thất bại.');
        }
      },
      error: (error: any) => {
        console.error('Đã xảy ra lỗi khi thay đổi vai trò:', error);
        if (error.error && error.error.errors) {
          console.error('Chi tiết lỗi:', error.error.errors);
        } else {
          console.error('Chi tiết lỗi:', error.error);
        }
      },
    });
  }
}
