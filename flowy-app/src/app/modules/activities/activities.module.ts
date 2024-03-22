import { NgModule, Type } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ActivitiesRegistryService } from './services/registry.service';
import { ActivityMainComponent } from './components/activity-main.component';
import { ActivityTakeQueueComponent } from './components/takes/queue.component';
import { ActivityTakeManualComponent } from './components/takes/manual.component';
import { ActivitySystemNotFoundComponent } from './components/system/not-found.component';
import { ActivityDecisionStandardComponent } from './components/decisions/standard.component';

const activities: Array<Type<any>> = [
  ActivitySystemNotFoundComponent,
  ActivityTakeQueueComponent,
  ActivityTakeManualComponent,
  ActivityDecisionStandardComponent
];

@NgModule({
  declarations: [
    ActivityMainComponent,
    ...activities
  ],
  imports: [
    CommonModule
  ],
  exports: [
    ActivityMainComponent
  ],
  providers: [
    { provide: 'activities_registry', useValue: activities},
    ActivitiesRegistryService
  ],
})
export class ActivitiesModule {
}
