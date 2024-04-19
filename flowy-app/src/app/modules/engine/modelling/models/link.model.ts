import { Node } from './node.model';

export class Link {
  public id?: number;
  public key?: string;
  public idSourceNode?: number;
  public sourceNode?: Node;
  public idTargetNode?: number;
  public targetNode?: Node;
}
