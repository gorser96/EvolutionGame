import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IAuthUser } from '../auth.models';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.less'],
})
export class RegisterComponent {
  registerForm: FormGroup;

  constructor(private auth: AuthService, private formBuilder: FormBuilder) {
    this.registerForm = this.formBuilder.group({
      login: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  onSubmit() {
    if (this.registerForm.valid) {
      const formData = this.registerForm.value;
      this.auth
        .register(<IAuthUser>{
          login: formData.login,
          password: formData.password,
        })
        .subscribe();
    }
  }
}
