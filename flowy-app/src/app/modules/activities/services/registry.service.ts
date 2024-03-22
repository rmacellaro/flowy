import { Inject, Injectable } from '@angular/core';

import { ActivitySystemNotFoundComponent } from '../components/system/not-found.component';
import { ActivityDefinition } from '../models/activity-definition.interface';

@Injectable()
export class ActivitiesRegistryService {

  public interactions: Array<ActivityDefinition> = [];
  public notFoundUi: ActivityDefinition = { key: 'NotFound', group: 'System', component: ActivitySystemNotFoundComponent};

  constructor(@Inject('activities_registry') activitiesComponents: any){
    if (!activitiesComponents) { throw new Error('No activitiesComponents !');}
    console.log('[*]activities[*]', activitiesComponents);
    activitiesComponents.forEach((activityComponent: any) => {
      if (activityComponent.ActivityDefinition) {
        const interacui: ActivityDefinition = activityComponent.ActivityDefinition;
        interacui.component = activityComponent;
        this.interactions.push(interacui);
      }
    });
  }

  public GetActivityByPath(path: string): ActivityDefinition {
    var paths: Array<string> = path.split('.');
    //console.log('GetInteractionUIByPath', path, paths);
    if (paths.length != 2) { return this.notFoundUi; }
    var ui = this.interactions.find(i => i.group == paths[0] && i.key == paths[1]);
    //console.log('uuuuuuuu', this.interactions);
    if (!ui) { return this.notFoundUi;}
    return ui;
  }
}
