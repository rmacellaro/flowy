import { DataType } from './data-type.model';
import { Data } from './data.model';

export class DataMap {
  public data?: Data;
  public dataType?: DataType;
  public state: 'NONE' | 'NEW' | 'EDITED' | 'SAVED' = 'NONE';
}
