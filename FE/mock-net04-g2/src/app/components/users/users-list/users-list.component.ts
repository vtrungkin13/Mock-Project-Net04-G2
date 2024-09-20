import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../../../models/User';
import { UserService } from '../../../services/user-service/user.service';
import { FormsModule, NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-users-list',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './users-list.component.html',
  styleUrl: './users-list.component.scss'
})
export class UsersListComponent {
  users: User[] = []; // Store fetched users
  searchQuery: string = ''; // Store search input
  page: number = 1; 
  pageSize: number = 9;
  totalPage: number = 0;
  totalCount: number = 0;
  loading: boolean = false; // To track the loading state

  constructor(private userService: UserService) {
    this.getAllUsers();
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
    this.userService.findUser(this.pageSize, this.page, this.searchQuery).subscribe({
      next: (response: any) => {
        console.log(response);
             
          if(response && response.status === 200) {
            this.users = response.body;
            this.getAllUserCount();
            this.loading = false;
          }
      },
      error: (error) => {
        console.log(error.message);        
        console.error('Failed to load users:', error);
        this.loading = false;
      }
    });
  }

  getAllUserCount() {
    this.userService.filterUserCount(this.searchQuery).subscribe({
      next: (response: any) => {     
          if(response && response.status === 200) {
            this.totalCount = response.body
            this.totalPage = Math.ceil(this.totalCount/this.pageSize)
          }
      },
      error: (error) => {
        console.error('Failed to load users:', error);
      }
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
}
