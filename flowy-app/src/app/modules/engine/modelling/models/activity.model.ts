import { ActivityData } from './activity-data.model';
import { Node } from './node.model';

export class Activity {
  public id?: number;
  public idNode?: number;
  public idActivityDefinition?: number;
  //public node?: Node;
  public key?: string;
  public index?: number;
  //public isDefault?: boolean;
  public datas?: Array<ActivityData>;
}
