import { Distribution } from './distribution.model';

export class Process {
  public id?: number;
  public key?: string;
  public name?: string;
  public description?: string;
  public distributions?: Array<Distribution>;
}
