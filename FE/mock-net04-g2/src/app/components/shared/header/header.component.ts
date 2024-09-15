import { Component, Input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { RoleEnum } from '../../../models/enum/RoleEnum';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  @Input() userRole?: RoleEnum;
}
