import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { UiButtonInfoComponent } from './components/button-info.component';
import { UiEmptyComponent } from './components/empty.component';
import { UiSpinnerComponent } from './components/spinner.component';

@NgModule({
  declarations: [
    UiSpinnerComponent,
    UiEmptyComponent,
    UiButtonInfoComponent
  ],
  imports: [ 
    CommonModule 
  ],
  exports: [
    UiSpinnerComponent,
    UiEmptyComponent,
    UiButtonInfoComponent
  ]
})
export class UiModule {}