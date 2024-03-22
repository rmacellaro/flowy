import { provideHttpClient } from '@angular/common/http';
import { bootstrapApplication } from '@angular/platform-browser';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideRouter, Routes } from '@angular/router';

import { AppComponent } from './app/app.component';

const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./app/routes/main/main.module').then(m => m.AppMainModule),
    title: 'Titolo della home'
  },
  {
    path: 'engine',
    loadChildren: () => import('./app/routes/engine/engine.module').then(m => m.AppEngineModule),
    title: 'Engine'
  },
  {
    path: 'camunda',
    loadChildren: () => import('./app/routes/camunda/camunda.module').then(m => m.AppCamundaModule),
    title: 'Camunda'
  },
  /*{
    path: 'console',
    loadChildren: () => import('./app/routes/console/console.module').then(m => m.AppConsoleModule),
    title: 'Console'
  },
  {
    path: 'system',
    loadChildren: () => import('./app/routes/system/system.module').then(m => m.AppSystemModule),
    title: 'System'
  }*/
];

bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(routes),
    provideAnimations(),
    provideHttpClient()
  ]
});
