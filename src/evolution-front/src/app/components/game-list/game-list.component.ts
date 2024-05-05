import { Component } from '@angular/core';
import { faReply, faPlus } from '@fortawesome/free-solid-svg-icons';
import { EMPTY, Observable } from 'rxjs';
import { RoomApiService } from 'src/app/api-services/room-api.service';
import { IRoomViewModel } from 'src/app/models/game-list.model';

@Component({
  selector: 'app-game-list',
  templateUrl: './game-list.component.html',
  styleUrls: ['./game-list.component.less'],
})
export class GameListComponent {
  faReply = faReply;
  faPlus = faPlus;

  rooms: Observable<IRoomViewModel[]> = EMPTY;
  selectedRoom: IRoomViewModel | null = null;

  constructor(private roomApi: RoomApiService) {}

  ngOnInit(): void {
    this.rooms = this.roomApi.getRooms();
  }

  onGameSelected(e: any) {
    if (e.options && e.options.length > 0) {
      this.selectedRoom = e.options[0].value;
    }
  }

  createRoom(name: string, roomDialog: HTMLDialogElement) {
    if (!name) {
      return;
    }

    this.roomApi.create(name).subscribe((room) => {
      this.selectedRoom = room;
      this.rooms = this.roomApi.getRooms();
    });

    roomDialog.close();
  }
}
