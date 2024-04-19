import { Data } from '../../datas/models/data.model';
import { NodeDataType } from './node-data-type.model';
import { Node } from './node.model';

export class NodeData extends Data {
  public idNode?: number;
  //public node?: Node;
  public idNodeDataType?: number;
  //public nodeDataType?: NodeDataType;
}
