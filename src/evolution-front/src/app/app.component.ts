import { Component } from '@angular/core';
import { TranslateHelperService } from './core/translate-helper.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less'],
})
export class AppComponent {
  title = 'Evolution';

  constructor(private _: TranslateHelperService) {}
}
