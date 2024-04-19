import { NgModule, Type } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ActivityMainComponent } from './components/activity-main.component';
import { ActivityTakeQueueComponent } from './components/takes/queue.component';
import { ActivityTakeManualComponent } from './components/takes/manual.component';
import { ActivitySystemNotFoundComponent } from './components/system/not-found.component';
import { ActivityDecisionStandardComponent } from './components/decisions/standard.component';
import { ActivityFormsFormJSComponent } from './components/forms/form-js.component';
import { ActivityDefinitionsService } from './services/activity-definitions.service';
import { UiModule } from '../../ui/ui.module';

const activities: Array<Type<any>> = [
  ActivitySystemNotFoundComponent,
  ActivityTakeQueueComponent,
  ActivityTakeManualComponent,
  ActivityDecisionStandardComponent,
  ActivityFormsFormJSComponent
];

@NgModule({
  declarations: [
    ActivityMainComponent,
    ...activities
  ],
  imports: [
    CommonModule,
    UiModule
  ],
  exports: [
    ActivityMainComponent
  ],
  providers: [
    //{ provide: 'activities_registry', useValue: activities},
    //ActivitiesRegistryService
    ActivityDefinitionsService
  ],
})
export class ActivitiesModule {
}
