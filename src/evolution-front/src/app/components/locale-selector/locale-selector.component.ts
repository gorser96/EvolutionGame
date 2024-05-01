import { Component } from '@angular/core';
import { TranslateHelperService } from 'src/app/core/translate-helper.service';

@Component({
  selector: 'app-locale-selector',
  templateUrl: './locale-selector.component.html',
  styleUrls: ['./locale-selector.component.less'],
})
export class LocaleSelectorComponent {
  locales: string[];
  selectedLocale: string;
  constructor(private translateHelper: TranslateHelperService) {
    this.locales = this.translateHelper.getLocales();
    this.selectedLocale = this.translateHelper.getCurrentLanguage();
  }

  onLocaleChange(e: any) {
    this.translateHelper.setLanguage(e.target.value);
    this.selectedLocale = e.target.value;
  }
}
