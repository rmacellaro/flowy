import { ActivityDefinitionDataType } from './activity-definition-data-type.model';

export class ActivityDefinition {
  public id?: number;
  public group?: string;
  public name?: string;
  public hasFrontEnd?: boolean;

  public dataTypes?: Array<ActivityDefinitionDataType>;
}
