import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EngineModellingProcessesListComponent } from './components/processes-list.component';
import { EngineModellingProcessLoadComponent } from './components/process-load.component';
import { EngineModellingDistributionVersionComponent } from './components/distribution-version.component';
import { EngineModellingDistributionEditorComponent } from './components/distribution-editor.component';
import { EngineModellingDistributionLoadComponent } from './components/distribution-load.component';
import { EngineModellingService } from './services/modelling.service';
import { UiModule } from '../../ui/ui.module';
import { EngineModellingNodeFormComponent } from './components/node-form.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EngineModellingActivitiesListComponent } from './components/activities-list.component';
import { EngineModellingListsListComponent } from './components/links-list.component';
import { ModellingActivityFormComponent } from './components/activity-form.component';
import { DatasModule } from '../datas/datas.module';
import { ModellingNodeDataListComponent } from './components/node-data-list.component';
import { ModellingActivityDataListComponent } from './components/activity-data-list.component';

@NgModule({
  declarations: [
    EngineModellingProcessesListComponent,
    EngineModellingProcessLoadComponent,
    EngineModellingDistributionVersionComponent,
    EngineModellingDistributionEditorComponent,
    EngineModellingDistributionLoadComponent,
    EngineModellingNodeFormComponent,
    EngineModellingActivitiesListComponent,
    EngineModellingListsListComponent,
    ModellingActivityFormComponent,
    ModellingNodeDataListComponent,
    ModellingActivityDataListComponent
  ],
  imports: [
    CommonModule,
    UiModule,
    ReactiveFormsModule,
    DatasModule,
    FormsModule
  ],
  exports: [
    EngineModellingProcessesListComponent,
    EngineModellingProcessLoadComponent,
    EngineModellingDistributionVersionComponent,
    EngineModellingDistributionEditorComponent,
    EngineModellingDistributionLoadComponent,
    EngineModellingNodeFormComponent,
    EngineModellingActivitiesListComponent,
    EngineModellingListsListComponent,
    ModellingActivityFormComponent,
    ModellingActivityDataListComponent
  ],
  providers: [
    EngineModellingService
  ],
})
export class EngineModellingModule {}
