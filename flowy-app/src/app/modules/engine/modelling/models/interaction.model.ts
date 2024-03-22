import { Configuration } from './configuration.model';
import { Node } from './node.model';

export class Interaction {
  public id?: number;
  public idNode?: number;
  public node?: Node;
  public type?: string;
  public name?: string;
  public description?: string;
  public order?: number;
  public configurations?: Array<Configuration>;

  public nexts?: Array<string>;
}
