import { Component, Input } from '@angular/core';
import { Campaign } from '../../models/Campaign';
import { FormsModule, NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { PaymentService } from '../../services/payment-service/payment.service';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth-service/auth.service';
import { User } from '../../models/User';

@Component({
  selector: 'app-donate-form',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './donate-form.component.html',
  styleUrl: './donate-form.component.scss',
})
export class DonateFormComponent {
  baseAmount: number = 10000;
  selectedAmount: number = 0;
  selectedIndex: number | null = null;
  multipliers: number[] = [0.5, 1, 2, 3, 4, 5];

  @Input() campaign!: Campaign;

  constructor(
    private router: Router,
    private authService: AuthService,
    private paymentService: PaymentService
  ) {}

  onSubmit(form: NgForm) {
    if (form.valid) {
      const currentUrl = `http://localhost:4200${this.router.url}`;
      const amount = form.value.amount;

      const user: User = this.authService.getUser();
      const userId = user ? user.id : 1;
      const campaignId = this.campaign.id;

      const paymentRequest = {
        userId: userId,
        campaignId: campaignId,
        amount: amount,
      };

      this.paymentService.createPayment(currentUrl, paymentRequest).subscribe({
        next: (response) => {
          const responseBody = JSON.parse(response.body);
          const payUrl = responseBody.payUrl;
          window.location.href = payUrl;
        },
      });
    }
  }

  setAmount(amount: number, index: number) {
    this.selectedAmount = amount;
    this.selectedIndex = index;
  }

  getClass(index: number): string {
    return this.selectedIndex === index ? 'money-selected' : '';
  }

  unSelectAmount() {
    this.selectedIndex = null;
  }
}
