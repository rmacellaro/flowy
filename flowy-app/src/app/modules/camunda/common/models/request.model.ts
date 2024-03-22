import { Query } from './query.model';
import { Sort } from './sort.model';

export class Request {
  public offset?: number;
  public size?: number;
  public sort?: Sort;
  public queries?: Array<Query>;
  public searchAfter?: Array<any>;
}