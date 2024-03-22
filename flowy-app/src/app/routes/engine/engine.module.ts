import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

import { LayoutModule } from '../../modules/layout/layout.module';
import { AppEngineProcessesComponent } from './processes.component';
import { AppEngineProcessComponent } from './process.component';
import { AppEngineGeneralComponent } from './general.component';
import { EngineModellingModule } from '../../modules/engine/modelling/modelling.module';
import { AppEngineInstancesComponent } from './instances.component';
import { EngineProcessingModule } from '../../modules/engine/processing/processing.module';
import { AppEngineInstanceComponent } from './instance.component';
import { AppEngineDistributionComponent } from './distribution.component';

const routes: Routes = [
  { path: '', component: AppEngineProcessesComponent, title: 'Processi' },
  {
    path: ':id',
    component: AppEngineProcessComponent,
    children: [
      { path: '', redirectTo: 'general', pathMatch: 'full' },
      { path: 'general', component: AppEngineGeneralComponent },
      { path: 'instances', component: AppEngineInstancesComponent},
      { path: 'instance/:idWire', component: AppEngineInstanceComponent},
      { path: 'distribution/:idDistribution', component: AppEngineDistributionComponent}
    ]
  }
];

@NgModule({
  declarations: [
    AppEngineProcessesComponent,
    AppEngineProcessComponent,
    AppEngineGeneralComponent,
    AppEngineInstancesComponent,
    AppEngineInstanceComponent,
    AppEngineDistributionComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    LayoutModule,
    EngineModellingModule,
    EngineProcessingModule
  ],
  providers: [],
})
export class AppEngineModule {}
