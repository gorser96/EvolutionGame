import { Component } from '@angular/core';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.less'],
})
export class AuthComponent {
  isLogin: boolean = true;
  linkText: string = 'register';

  onLinkClick() {
    this.isLogin = !this.isLogin;
    if (this.isLogin) {
      this.linkText = 'register';
    } else {
      this.linkText = 'login';
    }
  }
}
