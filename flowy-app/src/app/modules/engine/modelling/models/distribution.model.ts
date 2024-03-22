import { Node } from './node.model';

export class Distribution {
  public id?: number;
  public idProcess?: number;
  public version?: number;
  public isEnabled?: boolean;
  public state?: string;
  public createdDateTime?: Date;
  public nodes?: Array<Node>;
}
