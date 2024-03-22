import { ModuleWithProviders, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LayoutTopbarComponent } from './components/topbar.component';
import { LayoutSidebarComponent } from './components/sidebar.component';
import { RouterModule } from '@angular/router';
import { LayoutLeftbarComponent } from './components/leftbar.component';
import { LayoutTitleComponent } from './components/title.component';

@NgModule({
  declarations: [
    LayoutTopbarComponent,
    LayoutSidebarComponent,
    LayoutLeftbarComponent,
    LayoutTitleComponent
  ],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports: [
    LayoutTopbarComponent,
    LayoutSidebarComponent,
    LayoutLeftbarComponent,
    LayoutTitleComponent
  ],
  providers: [
  ]
})
export class LayoutModule {
  /*public static forRoot(): ModuleWithProviders<LayoutModule> {
    return {
        ngModule: LayoutModule,
        providers: [
          LayoutService,
        ],
    };
  }*/
}
