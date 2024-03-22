import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CamundaInstancesService } from './services/instances.service';
import { CamundaProcessingService } from './services/processing.service';
import { CamundaProcessingTaskComponent } from './components/tasks/task.component';
import { CamundaProcessingInstanceStateComponent } from './components/instances/state.component';
import { CamundaProcessingInstancesListComponent } from './components/instances/list.component';
import { CamundaProcessingInstanceDetsilComponent } from './components/instances/detail.component';
import { CamundaProcessingInstanceChooseStateComponent } from './components/instances/choose-state.component';
import { CamundaProcessingConsoleMainComponent } from './components/console/main.component';
import { CamundaModellingModule } from '../modelling/modelling.module';

@NgModule({
  declarations: [
    CamundaProcessingTaskComponent,
    CamundaProcessingInstanceStateComponent,
    CamundaProcessingInstancesListComponent,
    CamundaProcessingInstanceDetsilComponent,
    CamundaProcessingInstanceChooseStateComponent,
    CamundaProcessingConsoleMainComponent
  ],
  imports: [
    CommonModule,
    CamundaModellingModule
  ],
  exports: [
    CamundaProcessingTaskComponent,
    CamundaProcessingInstanceStateComponent,
    CamundaProcessingInstancesListComponent,
    CamundaProcessingInstanceDetsilComponent,
    CamundaProcessingInstanceChooseStateComponent,
    CamundaProcessingConsoleMainComponent
  ],
  providers: [
    CamundaInstancesService,
    CamundaProcessingService
  ],
})
export class CamundaProcessingModule {}
