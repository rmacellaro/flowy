import { Component, OnInit, Input } from '@angular/core';
import { Data } from '../models/data.model';
import { DataTypeBaseComponent } from './data-type-base.component';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'data-type-text-show',
  template: `
    <div class="text-ellipsis">
      <span *ngIf="dataMap && dataMap.data && dataMap.data.value">{{dataMap.data.value}}</span>
    </div>
  `
})
export class DataTypeTextShowComponent extends DataTypeBaseComponent {}

@Component({
  selector: 'data-type-text-edit',
  template: `
    <form [formGroup]="form" (ngSubmit)="onSubmit()" autocomplete="off">
      <div class="mb-3">
        <input type="text" class="form-control" id="textControl" [formControl]="textControl">
      </div>
      <div class="text-end">
        <button class="btn btn-primary" type="submit">Ok</button>
      </div>
    </form>
  `
})
export class DataTypeTextEditComponent extends DataTypeBaseComponent {

  public form: FormGroup;
  public textControl: FormControl = new FormControl(null, []);

  constructor(formbuilder: FormBuilder){
    super();
    this.form = formbuilder.group({
      text: this.textControl
    });
    /*this.textControl.valueChanges.subscribe(val => {
      if (!this.config){ return; }
      this.config.value = val;
    });*/
  }

  override OnInit(): void {
    this.textControl.setValue(this.dataMap?.data?.value);
  }

  onSubmit(): void {
    console.log('submit');
    if (!this.dataMap) { return;}
    //this.config.value = this.textControl.value;
    this.Save(this.textControl.value);
  }
}
