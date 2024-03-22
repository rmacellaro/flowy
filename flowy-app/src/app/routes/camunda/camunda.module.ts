import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AppCamundaScopeComponent } from './scope.component';
import { AppScopeHelper } from './scope.helper';
import { AppCamundaScopesComponent } from './scopes.component';
import { CamundaModellingModule } from '../../modules/camunda/modelling/modelling.module';
import { LayoutModule } from '../../modules/layout/layout.module';
import { AppCamundaScopeConsoleStartComponent } from './console.component';
import { CamundaProcessingModule } from '../../modules/camunda/processing/processing.module';
import { AppCamundaScopeDraftComponent } from './draft.component';
import { AppCamundaScopeDraftsComponent } from './drafts.component';
import { AppCamundaScopeInstanceComponent } from './instance.component';
import { AppCamundaScopeInstancesComponent } from './instances.component';
import { AppCamundaScopeInteractionComponent } from './interaction.component';
import { AppCamundaScopeInteractionsComponent } from './interactions.component';
import { AppCamundaScopeProcessesComponent } from './processes.component';
import { AppCamundaScopeTaskComponent } from './task.component';
import { AppCamundaScopeTasksComponent } from './tasks.component';
import { AppCamundaScopeDashboardComponent } from './dashboard.component';


const routes: Routes = [
  { path: '', component: AppCamundaScopesComponent, title: 'Processi' },
  {
    path: ':id',
    component: AppCamundaScopeComponent,
    children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      { path: 'dashboard', component: AppCamundaScopeDashboardComponent},
      { path: 'drafts', component: AppCamundaScopeDraftsComponent },
      { path: 'drafts/:idDraft', component: AppCamundaScopeDraftComponent },
      { path: 'instances', component: AppCamundaScopeInstancesComponent },
      { path: 'instances/:idInstance', component: AppCamundaScopeInstancesComponent },
      { path: 'interactions', component: AppCamundaScopeInteractionsComponent },
      { path: 'interactions/:idInteraction', component: AppCamundaScopeInteractionComponent },
      { path: 'processes', component: AppCamundaScopeProcessesComponent },
      { path: 'start', component: AppCamundaScopeConsoleStartComponent},
      { path: 'tasks', component: AppCamundaScopeTasksComponent },
      { path: 'tasks/:idTask', component: AppCamundaScopeTaskComponent }
    ]
  }
];

@NgModule({
  declarations: [
    AppCamundaScopeDashboardComponent,
    AppCamundaScopesComponent,
    AppCamundaScopeComponent,
    AppCamundaScopeConsoleStartComponent,
    AppCamundaScopeDraftComponent,
    AppCamundaScopeDraftsComponent,
    AppCamundaScopeInstanceComponent,
    AppCamundaScopeInstancesComponent,
    AppCamundaScopeInteractionComponent,
    AppCamundaScopeInteractionsComponent,
    AppCamundaScopeProcessesComponent,
    AppCamundaScopeTaskComponent,
    AppCamundaScopeTasksComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    CamundaModellingModule,
    CamundaProcessingModule,
    LayoutModule
  ],
  exports: [

  ],
  providers: [
    AppScopeHelper
  ],
})
export class AppCamundaModule {}
