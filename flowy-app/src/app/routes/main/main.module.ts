import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

import { AppMainHomeComponent } from './home.component';

const routes: Routes = [
  {
    path: '',
    component: AppMainHomeComponent,
    title: 'Titolo della home'
  }
];

@NgModule({
  declarations: [
    AppMainHomeComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports: [],
  providers: [],
})
export class AppMainModule {}
