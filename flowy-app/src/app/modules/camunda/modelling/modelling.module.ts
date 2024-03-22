import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

import { CamundaModellingBpmnModelerComponent } from './components/bpmn-modeler/modeler.component';
import { CamundaModellingBpmnViewerComponent } from './components/bpmn-viewer/viewer.component';
import { CamundaModellingFormEditorComponent } from './components/form-editor/form-editor.component';
import { CamundaModellingFormPlaygroundComponent } from './components/form-playground/form-playground.component';
import { CamundaModellingFormViewerComponent } from './components/form-viewer/form-viewer.component';
import { CamundaModellingProcessSchemaComponent } from './components/processes/schema.component';

import { CamundaModellingDraftsService } from './services/drafts.service';
import { CamundaModellingInteractionsService } from './services/interactions.service';
import { CamundaModellingProcessesService } from './services/processes.service';
import { CamundaModellingScopesService } from './services/scopes.service';
import { CamundaModellingScopesListComponent } from './components/scopes/list.component';
import { CamundaModellingDraftCommandCloneComponent } from './components/drafts/command-clone.component';
import { CamundaModellingDeployCommandCloneComponent } from './components/drafts/command-deploy.component';
import { CamundaModellingDraftsListComponent } from './components/drafts/list.component';
import { CamundaModellingDraftTracksComponent } from './components/drafts/tracks.component';
import { CamundaModellingInteractionDetailComponent } from './components/interactions/detail.component';
import { CamundaModellingInteractionsListComponent } from './components/interactions/list.component';
import { CamundaModellingInteractionTypeComponent } from './components/interactions/type.component';
import { CamundaModellingProcessessChooseComponent } from './components/processes/choose.component';
import { CamundaModellingProcessDetailComponent } from './components/processes/detail.component';
import { CamundaModellingProcessesListComponent } from './components/processes/list.component';

@NgModule({
  declarations: [
    CamundaModellingBpmnModelerComponent,
    CamundaModellingBpmnViewerComponent,
    CamundaModellingFormEditorComponent,
    CamundaModellingFormViewerComponent,
    CamundaModellingFormPlaygroundComponent,

    CamundaModellingScopesListComponent,
    CamundaModellingProcessSchemaComponent,
    CamundaModellingDraftCommandCloneComponent,
    CamundaModellingDeployCommandCloneComponent,
    CamundaModellingDraftsListComponent,
    CamundaModellingDraftTracksComponent,
    CamundaModellingInteractionDetailComponent,
    CamundaModellingInteractionsListComponent,
    CamundaModellingInteractionTypeComponent,
    CamundaModellingProcessessChooseComponent,
    CamundaModellingProcessDetailComponent,
    CamundaModellingProcessesListComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  exports: [
    CamundaModellingBpmnModelerComponent,
    CamundaModellingBpmnViewerComponent,
    CamundaModellingFormEditorComponent,
    CamundaModellingFormViewerComponent,
    CamundaModellingFormPlaygroundComponent,

    CamundaModellingScopesListComponent,
    CamundaModellingProcessSchemaComponent,
    CamundaModellingDraftCommandCloneComponent,
    CamundaModellingDeployCommandCloneComponent,
    CamundaModellingDraftsListComponent,
    CamundaModellingDraftTracksComponent,
    CamundaModellingInteractionDetailComponent,
    CamundaModellingInteractionsListComponent,
    CamundaModellingInteractionTypeComponent,
    CamundaModellingProcessessChooseComponent,
    CamundaModellingProcessDetailComponent,
    CamundaModellingProcessesListComponent
  ],
  providers: [
    CamundaModellingDraftsService,
    CamundaModellingInteractionsService,
    CamundaModellingProcessesService,
    CamundaModellingScopesService
  ],
})
export class CamundaModellingModule {}
