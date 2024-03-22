import { Interaction } from './interaction.model';
import { Distribution } from './distribution.model';

export class Node {
  public id?: number;
  public idRelease?: number;
  public release?: Distribution;
  public title?: string;
  public key?: string;
  public description?: string;
  public color?: string;
  public percentage?: number;
  public interactions?: Array<Interaction>;

  public isExpanded: boolean = false;
}
