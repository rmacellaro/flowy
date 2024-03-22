import { Distribution } from '../../modelling/models/distribution.model';
import { Data } from './data.model';
import { Wire } from './wire.model';

export class Instance {
  public id?: number;
  public idDistribution?: number;
  public distribution?: Distribution;
  public key?: string;
  public createdDateTime?: Date;

  public wires?: Array<Wire>;
  public datas?: Array<Data>;
}
