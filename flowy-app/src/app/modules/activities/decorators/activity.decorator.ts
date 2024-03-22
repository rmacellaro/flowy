import { ActivityDefinition } from '../models/activity-definition.interface';

export function Activity(params?: ActivityDefinition) {

  return function (target: any) {
    console.log('[°] decorator ActivityDefinition:', params);
    target.ActivityDefinition = params;
    //Object.assign(target, { flowy: params}, params);
  }
}
