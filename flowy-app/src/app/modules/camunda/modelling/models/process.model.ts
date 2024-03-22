import { Scope } from './scope.model';

export class Process {
  public id?: number;
  public idScope?: number;
  public scope?: Scope;
  public key?: number;
  public name?: string;
  public version?: number;
  public bpmnProcessId?: string;
  public tenantId?: string;  
}
