import { Process } from "../../modelling/models/process.model";

export class Instance {
  public id?: number;
  public idProcess?: number;
  public process?: Process;
  public key?: number;
  public createdAt?: Date;
  public reference?: string;

  public tracks?: Array<any>;
  public datas?: Array<any>;

  public parentKey?: number;
  public parentFlowNodeInstanceKey?: number;
  public startDate?: string;
  public endDate?: string;
  public state?: string;
  public processDefinitionKey?: number;
  public tenantId?: string;
  public parentProcessInstanceKey?: any;
}
