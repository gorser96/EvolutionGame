import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { IAuthUser, IUserToken } from '../auth.models';
import { AppConfigService } from 'src/app/core/app-config.service';

@Injectable()
export class AuthService {
  private token_key: string = 'auth-token';
  private userToken: IUserToken | null = null;

  private isAuthenticatedSubject: BehaviorSubject<boolean> =
    new BehaviorSubject<boolean>(false);
  private userNameSubject: BehaviorSubject<string | null> = new BehaviorSubject<
    string | null
  >(null);

  constructor(private http: HttpClient, private config: AppConfigService) {
    var userTokenStr = localStorage.getItem(this.token_key);

    if (userTokenStr) {
      this.userToken = JSON.parse(userTokenStr);
      this.sendObservableData(this.userToken);
    }
  }

  login(user: IAuthUser): Observable<{ token: string }> {
    return this.http
      .post<IUserToken>(this.config.appSettings.baseApiUrl + 'api/user/login', user)
      .pipe(
        tap((userToken) => {
          this.setToken(userToken);
        })
      );
  }

  setToken(userToken: IUserToken | null): void {
    this.userToken = userToken;

    if (this.userToken) {
      localStorage.setItem(this.token_key, JSON.stringify(this.userToken));
    } else {
      localStorage.removeItem(this.token_key);
    }

    this.sendObservableData(this.userToken);
  }

  getToken(): string | null {
    return this.userToken?.token ?? null;
  }

  isAuthenticated(): boolean {
    return !!this.userToken;
  }

  isAuthenticated$(): Observable<boolean> {
    return this.isAuthenticatedSubject.asObservable();
  }

  getUserName(): string | null {
    return this.userToken?.userName ?? null;
  }

  getUserName$(): Observable<string | null> {
    return this.userNameSubject.asObservable();
  }

  logout(): void {
    this.setToken(null);
  }

  register(user: IAuthUser): Observable<{ token: string }> {
    return this.http
      .post<IUserToken>(
        this.config.appSettings.baseApiUrl + 'api/user/register',
        user
      )
      .pipe(
        tap((userToken) => {
          this.setToken(userToken);
        })
      );
  }

  private sendObservableData(userToken: IUserToken | null) {
    this.isAuthenticatedSubject.next(!!userToken);
    this.userNameSubject.next(this.userToken?.userName ?? null);
  }
}
