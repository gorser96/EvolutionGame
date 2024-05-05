import { Component, Input } from '@angular/core';
import { Observable } from 'rxjs';
import { RoomApiService } from 'src/app/api-services/room-api.service';
import { AuthService } from 'src/app/auth/services/auth.service';
import { IRoomViewModel } from 'src/app/models/game-list.model';

@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
  styleUrls: ['./room.component.less'],
})
export class RoomComponent {
  @Input() room: IRoomViewModel | undefined;
  userName$: Observable<string | null>;

  hasInRoom: boolean = false;
  isHost: boolean = false;

  constructor(private auth: AuthService, private roomApi: RoomApiService) {
    this.userName$ = auth.getUserName$();
  }

  ngOnInit(): void {
    this.userName$.subscribe((userName) => {
      if (!this.room) {
        this.hasInRoom = false;
        return;
      }

      if (userName === null) {
        this.hasInRoom = false;
        return;
      }

      const user = this.room.inGameUsers.find(
        (p) => p.user.userName === userName
      );

      this.hasInRoom = !!user;
      this.isHost = user?.isHost ?? false;
    });
  }

  removeRoom() {
    if (!this.room) {
      return;
    }

    this.roomApi.remove(this.room.uid).subscribe();
  }
}
