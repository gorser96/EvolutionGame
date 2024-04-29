import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Injectable({
  providedIn: 'root',
})
export class TranslateHelperService {
  private _localeKey: string = 'user_locale';

  constructor(private translate: TranslateService) {
    const userLocaleKey = localStorage.getItem(this._localeKey);
    if (userLocaleKey) {
      translate.use(userLocaleKey);
    } else {
      translate.setDefaultLang('en');
    }
  }

  setLanguage(lang: string) {
    this.translate.use(lang);
    localStorage.setItem(this._localeKey, lang);
  }

  getTranslation(key: string) {
    return this.translate.instant(key);
  }

  getCurrentLanguage(): string {
    const userLocaleKey = localStorage.getItem(this._localeKey);
    return userLocaleKey || 'en';
  }

  getLocales(): string[] {
    return ['en', 'ru'];
  }
}
