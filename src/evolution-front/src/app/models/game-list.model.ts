import { Time } from '@angular/common';
import { IAdditionViewModel } from './addition.models';
import { IUserViewModel } from './user.models';
import { IAnimalViewModel } from './animal.models';

export interface IRoomViewModel {
  uid: string;
  name: string;
  createdDateTime: Date;
  startDateTime?: Date;
  finishedDateTime?: Date;
  maxTimeLeft?: Time;
  stepNumber: number;
  isStarted: boolean;
  isPaused: boolean;
  isPrivate: boolean;
  pauseStartedTime?: Date;
  numOfCards: number;
  maxUsersCount: number;
  inGameUsers: IInGameUser[];
  additions: IAdditionViewModel[];
}

export interface IInGameUser {
  user: IUserViewModel;
  animals: IAnimalViewModel[];
  isCurrent: boolean;
  startStepTime?: Date;
  order: number;
  isHost: boolean;
}

export interface IRoomEditModel {
  name: string;
  maxTimeLeft?: Time;
  additions?: string[];
  isPrivate: boolean;
  numOfCards: number;
}
