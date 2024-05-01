import { Component } from '@angular/core';
import { faReply } from '@fortawesome/free-solid-svg-icons';
import { IGameHeader } from 'src/app/models/game-list.model';

@Component({
  selector: 'app-game-list',
  templateUrl: './game-list.component.html',
  styleUrls: ['./game-list.component.less'],
})
export class GameListComponent {
  faReply = faReply;
  gameList: IGameHeader[] = [
    { id: '1', name: 'test game 1' },
    { id: '2', name: 'test game 2' },
    { id: '3', name: 'test game 3' },
    { id: '4', name: 'test game 4' },
    { id: '5', name: 'test game 5' },
    { id: '6', name: 'test game 6' },
    { id: '7', name: 'test game 7' },
    { id: '8', name: 'test game 8' },
    { id: '9', name: 'test game 9' },
  ];
  selectedGame: IGameHeader | null = null;

  onGameSelected(e: any) {
    if (e.options && e.options.length > 0) {
      this.selectedGame = e.options[0].value;
    }
  }
}
