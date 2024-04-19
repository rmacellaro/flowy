import { Component, OnInit, Input } from '@angular/core';
import { Data } from '../models/data.model';
import { DataTypeBaseComponent } from './data-type-base.component';
import bootstrapIcons from 'bootstrap-icons/font/bootstrap-icons.json';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'data-type-icon-show',
  template: `
    <span class="me-2">Icona:</span>
    <i *ngIf="dataMap?.data && dataMap?.data?.value" class="{{dataMap?.data?.value}}"></i>
  `
})
export class DataTypeIconShowComponent extends DataTypeBaseComponent {}

@Component({
  selector: 'data-type-icon-edit',
  template: `
    <input class="form-control form-control-lg mb-3" type="text" [(ngModel)]="term" placeholder="cerca" />
    <div class="row row-cols-6 m-0">
      <div class="col text-center mb-2" *ngFor="let item of icons">
        <a class="btn btn-link w-100 text-center" href="javascript:;" (click)="select(item)">
          <i class="bi bi-{{item}} fs-1"></i>
          <div class="f-s-08 fst-italic opacity-70">{{item}}</div>
        </a>
      </div>
    </div>
  `
})
export class DataTypeIconEditComponent extends DataTypeBaseComponent {

  public icons?: Array<string> = [];
  public iconsAll: Array<string> = Object.keys(bootstrapIcons);

  public form: FormGroup;
  public searchControl: FormControl = new FormControl();

  public set term(value: string) {
    console.log(value);
      setTimeout(() => {
      if (value == '') {
        this.icons = this.iconsAll;
      } else {
        this.icons = this.iconsAll.filter( i => i.includes(value));
      }
    }, 200);
  }

  constructor(formBuilder: FormBuilder) {
    super();
    this.form = formBuilder.group({
      search: this.searchControl
    });
    this.size = 'xl';
    this.icons = this.iconsAll;
    //this.searchControl.valueChanges
  }

  select(icon: string): void {
    this.Save('bi bi-' + icon);
  }

}
