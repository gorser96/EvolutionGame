import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/auth/services/auth.service';
import { TranslateHelperService } from 'src/app/core/translate-helper.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.less'],
})
export class MenuComponent {
  isAuthenticated$: Observable<boolean>;
  locales: string[];
  selectedLocale: string;

  constructor(
    private auth: AuthService,
    private translateHelper: TranslateHelperService
  ) {
    this.isAuthenticated$ = auth.isAuthenticated$();
    this.locales = this.translateHelper.getLocales();
    this.selectedLocale = this.translateHelper.getCurrentLanguage();
  }

  onExit() {
    this.auth.logout();
  }

  onLocaleChange(e: any) {
    this.translateHelper.setLanguage(e.target.value);
    this.selectedLocale = e.target.value;
  }
}
