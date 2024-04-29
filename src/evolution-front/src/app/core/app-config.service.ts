import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { IAppSettings } from './core.models';

@Injectable({
  providedIn: 'root',
})
export class AppConfigService {
  private appConfig: IAppSettings | undefined;

  constructor(private http: HttpClient) {}

  loadAppConfig() {
    return firstValueFrom(this.http.get('/appsettings.json')).then((data) => {
      this.appConfig = <IAppSettings>{ ...data };
    });
  }

  get appSettings(): IAppSettings {
    if (!this.appConfig) {
      throw Error('Config file not loaded!');
    }

    return this.appConfig;
  }
}
