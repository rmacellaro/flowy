import { NodeData } from './node-data.model';
import { Distribution } from './distribution.model';
import { Link } from './link.model';
import { Activity } from './activity.model';

export class Node {
  public id?: number;
  public idDistribution?: number;
  public distribution?: Distribution;
  //public title?: string;
  public key?: string;
  //public description?: string;

  public activities?: Array<Activity>;
  public datas?: Array<NodeData>;
  public outputLinks?: Array<Link>;
  public inputLinks?: Array<Link>;

  public isExpanded: boolean = false;
}
