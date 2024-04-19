import { DataType } from '../../datas/models/data-type.model';
import { ActivityDefinition } from './activity-definition.model';

export class ActivityDefinitionDataType extends DataType {
  public idActivityDefinition?: number;
  public activityDefinition?: ActivityDefinition;
}
