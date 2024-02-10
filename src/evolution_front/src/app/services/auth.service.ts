// auth.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { AuthUser } from '../models/user.model';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    constructor(private http: HttpClient) { }

    login(username: string, password: string): Observable<AuthUser | null> {
        return this.http.post<AuthUser | null>('/api/login', { username, password })
            .pipe(
                tap(user => {
                    if (user) {
                        console.log(user);
                        localStorage.setItem('currentUser', JSON.stringify({ user }));
                    }

                    return user;
                }));
    }

    register(username: string, password: string): Observable<any> {
        return this.http.post('/api/register', { username, password });
    }

    logout(): Observable<any> {
        return this.http.post('/api/logout', {}).pipe(result => {
            localStorage.removeItem('currentUser');
            return result;
        });
    }

    getCurrentUser(): AuthUser | null {
        // Получаем данные пользователя из localStorage
        const currentUser = localStorage.getItem('currentUser');
        return currentUser ? JSON.parse(currentUser) : null;
    }
}
