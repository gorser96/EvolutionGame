// register.component.ts
import { Component } from '@angular/core';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  username: string | undefined;
  password: string | undefined;
  confirmPassword: string | undefined;
  error: string | undefined;

  constructor(private authService: AuthService) {}

  onSubmit(): void {
    if (this.password !== this.confirmPassword) {
      this.error = 'Passwords do not match';
      return;
    }

    if (!this.username || !this.password || !this.confirmPassword) {
        return;
    }

    this.authService.register(this.username, this.password)
      .subscribe(
        response => {
          // Успешная регистрация
          console.log('Registered successfully:', response);
          // Редирект или другие действия при успешной регистрации
        },
        error => {
          // Ошибка регистрации
          console.error('Registration error:', error);
          this.error = 'Registration failed';
        }
      );
  }
}
