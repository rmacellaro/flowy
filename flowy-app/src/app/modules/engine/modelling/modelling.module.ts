import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EngineModellingProcessesService } from './services/processes.service';
import { EngineModellingProcessesListComponent } from './components/processes-list.component';
import { EngineModellingProcessLoadComponent } from './components/process-load.component';
import { EngineModellingDistributionVersionComponent } from './components/distribution-version.component';
import { EngineModellingDistributionEditorComponent } from './components/distribution-editor.component';
import { EngineModellingDistributionLoadComponent } from './components/distribution-load.component';
import { EngineModellingDistributionsService } from './services/distributions.service';

@NgModule({
  declarations: [
    EngineModellingProcessesListComponent,
    EngineModellingProcessLoadComponent,
    EngineModellingDistributionVersionComponent,
    EngineModellingDistributionEditorComponent,
    EngineModellingDistributionLoadComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    EngineModellingProcessesListComponent,
    EngineModellingProcessLoadComponent,
    EngineModellingDistributionVersionComponent,
    EngineModellingDistributionEditorComponent,
    EngineModellingDistributionLoadComponent
  ],
  providers: [
    EngineModellingProcessesService,
    EngineModellingDistributionsService
  ],
})
export class EngineModellingModule {}
