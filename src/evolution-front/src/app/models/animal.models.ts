import { IPropertyViewModel } from './addition.models';

export interface IAnimalViewModel {
  uid: string;
  foodCurrent: number;
  foodMax: number;
  properties: IInAnimalPropertyViewModel[];
}

export interface IInAnimalPropertyViewModel {
  propertyUid: string;
  property: IPropertyViewModel;
  isActive: boolean;
}
