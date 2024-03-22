export class InstanceTask {
  public id?: number;
  public name?: string;
  public taskDefinitionId?: string;
  public processName?: string;
  public creationDate?: Date;
  public completionDate?: Date;
  public assignee?: string;
  public taskState?: string;
  public formKey?: string;
  public processDefinitionKey?: number;
  public processInstanceKey?: number;
  public tenantId?: string;
  public dueDate?: Date;
  public followUpDate?: Date;
  public candidateGroups?: Array<string>;
  public candidateUsers?: Array<string>;
}