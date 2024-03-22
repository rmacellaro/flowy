export class InstanceTrack {
  public id?: number;
  public idInstance?: number;
  public eventAt?: Date;
  public operation?: string;
  public note?: string;
  public data?: string;

  public keyFlowNodeInstance?: number;
  public startDate?: Date;
  public endDate?: Date;
  public flowNodeId?: string;
  public flowNodeName?: string;
  public type?: string;
  public state?: string;
  public incident?: boolean;
  public tenantId?: string;
}