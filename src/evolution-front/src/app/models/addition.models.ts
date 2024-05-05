export interface IAdditionViewModel {
  uid: string;
  name: string;
  isBase: boolean;
  iconName?: string;
  icon?: number[];
  cards: ICardViewModel[];
}

export interface ICardViewModel {
  uid: string;
  firstProperty: IPropertyViewModel;
  secondProperty?: IPropertyViewModel;
}

export interface IPropertyViewModel {
  uid: string;
  name: string;
  isPair: boolean;
  isOnEnemy: boolean;
  feedIncreasing?: number;
}
