import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-toast',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './toast.component.html',
  styleUrl: './toast.component.scss',
})
export class ToastComponent {
  // 1: success, 2: fail, 3: loading
  @Input() toastStatus: number = 1;

  @Input() message: string = '';
  @Input() showToast: boolean = false;
}
