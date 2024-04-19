import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { EngineProcessingService } from './services/processing.service';
import { EngineProcessingInstanceComponent } from './components/instance.component';
import { ActivitiesModule } from '../activities/activities.module';
import { EngineProcessingInstancesListComponent } from './components/instances-list.component';
import { UiModule } from '../../ui/ui.module';


@NgModule({
  declarations: [
    EngineProcessingInstanceComponent,
    EngineProcessingInstancesListComponent
  ],
  imports: [
    CommonModule,
    ActivitiesModule,
    ReactiveFormsModule,
    FormsModule,
    UiModule
  ],
  exports: [
    EngineProcessingInstanceComponent,
    EngineProcessingInstancesListComponent
  ],
  providers: [
    EngineProcessingService
  ],
})
export class EngineProcessingModule {}
