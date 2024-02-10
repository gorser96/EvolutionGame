// login.component.ts
import { Component } from '@angular/core';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  username: string | undefined;
  password: string | undefined;
  error: string | undefined;

  constructor(private authService: AuthService) {}

  onSubmit(): void {
    if (!this.username || !this.password) {
        return;
    }
    this.authService.login(this.username, this.password)
      .subscribe(
        response => {
          // Успешная авторизация
          console.log('Logged in successfully:', response);
          // Редирект или другие действия при успешной авторизации
        },
        error => {
          // Ошибка авторизации
          console.error('Login error:', error);
          this.error = 'Invalid username or password';
        }
      );
  }
}
