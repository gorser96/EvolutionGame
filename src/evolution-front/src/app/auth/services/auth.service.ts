import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { IAuthUser } from '../auth.models';
import { AppConfigService } from 'src/app/core/app-config.service';

@Injectable()
export class AuthService {
  private token_key: string = 'auth-token';
  private token: string | null = null;
  private isAuthenticatedSubject: BehaviorSubject<boolean> =
    new BehaviorSubject<boolean>(false);

  constructor(private http: HttpClient, private config: AppConfigService) {
    var token = localStorage.getItem(this.token_key);
    if (token) {
      this.setToken(token);
    }
  }

  login(user: IAuthUser): Observable<{ token: string }> {
    return this.http
      .post<{ token: string }>(
        this.config.appSettings.baseApiUrl + 'user/login',
        user
      )
      .pipe(
        tap(({ token }) => {
          this.setToken(token);
        })
      );
  }

  setToken(token: string | null): void {
    this.token = token;

    if (this.token) {
      localStorage.setItem(this.token_key, this.token);
      this.isAuthenticatedSubject.next(true);
    } else {
      localStorage.removeItem(this.token_key);
      this.isAuthenticatedSubject.next(false);
    }
  }

  getToken(): string | null {
    return this.token;
  }

  isAuthenticated(): boolean {
    return !!this.token;
  }

  isAuthenticated$(): Observable<boolean> {
    return this.isAuthenticatedSubject.asObservable();
  }

  logout(): void {
    this.setToken(null);
  }

  register(user: IAuthUser): Observable<{ token: string }> {
    return this.http
      .post<{ token: string }>(
        this.config.appSettings.baseApiUrl + 'user/register',
        user
      )
      .pipe(
        tap(({ token }) => {
          this.setToken(token);
        })
      );
  }
}
