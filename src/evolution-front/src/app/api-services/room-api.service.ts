import { Injectable } from '@angular/core';
import { AppConfigService } from '../core/app-config.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IRoomEditModel, IRoomViewModel } from '../models/game-list.model';

@Injectable({
  providedIn: 'root',
})
export class RoomApiService {
  baseApi: string;
  constructor(private http: HttpClient, private config: AppConfigService) {
    this.baseApi = this.config.appSettings.baseApiUrl + 'api/room';
  }

  create(name: string): Observable<IRoomViewModel> {
    return this.http.post<IRoomViewModel>(this.baseApi + '/create', { name });
  }

  get(uid: string): Observable<IRoomViewModel> {
    return this.http.get<IRoomViewModel>(this.baseApi + '/' + uid);
  }

  getRooms(): Observable<IRoomViewModel[]> {
    return this.http.get<IRoomViewModel[]>(this.baseApi + '/list');
  }

  getRoomWithUser() {
    return this.http.get<IRoomViewModel[]>(this.baseApi + '/user');
  }

  enterRoom(uid: string): Observable<IRoomViewModel> {
    return this.http.post<IRoomViewModel>(
      this.baseApi + '/' + uid + '/enter',
      {}
    );
  }

  update(uid: string, editModel: IRoomEditModel): Observable<IRoomViewModel> {
    return this.http.post<IRoomViewModel>(
      this.baseApi + '/' + uid + '/update',
      editModel
    );
  }

  leave(uid: string): Observable<void> {
    return this.http.post<void>(this.baseApi + '/' + uid + '/leave', {});
  }

  kick(uid: string, userUid: string): Observable<void> {
    return this.http.post<void>(
      this.baseApi + '/' + uid + '/kick/' + userUid,
      {}
    );
  }

  start(uid: string): Observable<void> {
    return this.http.post<void>(this.baseApi + '/' + uid + '/start', {});
  }

  pause(uid: string): Observable<void> {
    return this.http.post<void>(this.baseApi + '/' + uid + '/pause', {});
  }

  resume(uid: string): Observable<void> {
    return this.http.post<void>(this.baseApi + '/' + uid + '/resume', {});
  }

  end(uid: string): Observable<void> {
    return this.http.post<void>(this.baseApi + '/' + uid + '/end', {});
  }

  remove(uid: string): Observable<void> {
    return this.http.post<void>(this.baseApi + '/' + uid + '/remove', {});
  }
}
