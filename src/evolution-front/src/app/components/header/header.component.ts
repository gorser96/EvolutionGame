import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/auth/services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.less'],
})
export class HeaderComponent {
  isAuthenticated$: Observable<boolean>;
  constructor(private auth: AuthService) {
    this.isAuthenticated$ = auth.isAuthenticated$();
  }
}
