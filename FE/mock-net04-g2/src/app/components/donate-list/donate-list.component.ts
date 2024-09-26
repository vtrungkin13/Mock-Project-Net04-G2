import { Component, OnInit } from '@angular/core';
import { Donate } from '../../models/Donate';
import { User } from '../../models/User';
import { CommonModule } from '@angular/common';
import { DonateService } from '../../services/donate-service/donate.service';
import { UserService } from '../../services/user-service/user.service';
import { AuthService } from '../../services/auth-service/auth.service';

@Component({
  selector: 'app-donate-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './donate-list.component.html',
  styleUrl: './donate-list.component.scss'
})
export class DonateListComponent implements OnInit {
  donations:Donate[] = [];
  userId? :number;
  loading:boolean = false;

  constructor(private donateService:DonateService, private authService: AuthService) {
    this.userId = authService.getUser().id;
  }

  ngOnInit(): void {
    if (this.userId && this.userId >= 1) {
      this.loading = true;
      this.donateService.GetDonationHistory(this.userId).subscribe({
        next: (response) => {
          this.donations = response.body;
          this.loading = false;
        },
        error: (error) => {
          console.log(error.message);
          this.loading = false; // Set loading to false after data is received
        }
      })
    }
  }

}
