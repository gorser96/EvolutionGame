import { Component } from '@angular/core';
import { AuthService } from 'src/app/auth/services/auth.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.less'],
})
export class ProfileComponent {
  username: string | null;
  constructor(private auth: AuthService) {
    this.username = this.auth.getUserName();
  }
}
