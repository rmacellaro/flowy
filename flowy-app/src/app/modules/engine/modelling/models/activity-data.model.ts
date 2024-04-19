import { Activity } from './activity.model';
import { Data as Data } from '../../datas/models/data.model';

export class ActivityData extends Data{
  public idActivity?: number;
  public activity?: Activity;
}
