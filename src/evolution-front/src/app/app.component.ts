import { Component } from '@angular/core';
import { TranslateHelperService } from './core/translate-helper.service';
import { Observable } from 'rxjs';
import { AuthService } from './auth/services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less'],
})
export class AppComponent {
  title = 'Evolution';
  isAuthenticated$: Observable<boolean>;

  constructor(private _: TranslateHelperService, private auth: AuthService) {
    this.isAuthenticated$ = auth.isAuthenticated$();
  }
}
