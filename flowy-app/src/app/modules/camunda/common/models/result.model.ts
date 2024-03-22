import { Request } from "./request.model";

export class Result<T> {
  public request?: Request;
  public total?: number;
  public items?: Array<T>;
  public sortValues?: Array<any>;
}