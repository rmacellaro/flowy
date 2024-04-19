import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { DataMapsListComponent } from './components/data-maps-list.component';
import { DataHelper } from './helpers/data.helper';
import { DataTypeIconEditComponent, DataTypeIconShowComponent } from './components/data-type-icon.component';
import { DataTypeTextEditComponent, DataTypeTextShowComponent } from './components/data-type-text.component';

@NgModule({
  declarations: [
    DataMapsListComponent,

    DataTypeIconEditComponent,
    DataTypeIconShowComponent,
    DataTypeTextEditComponent,
    DataTypeTextShowComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule
  ],
  exports: [
    DataMapsListComponent
  ],
  providers: [
    DataHelper
  ],
})
export class DatasModule {}
