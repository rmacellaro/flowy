import { Node } from '../../modelling/models/node.model';

export class Wire {
  public id?: number;
  public idInstance?: number;
  public state?: string;
  public reason?: string;
  public idNode?: number;
  public node?: Node;
  public createdDateTime?: Date;
  public updatedDateTime?: Date;
}
