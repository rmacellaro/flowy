import { Component, Input, OnInit, Output } from '@angular/core';

import { DataMap } from '../models/data-map.model';

@Component({
  selector: 'data-type-base',
  template: `...`
})
export class DataTypeBaseComponent implements OnInit {

  @Output() size: 'sm' | 'md' | 'lg' | 'xl' = 'md';

  @Input() dataMap?: DataMap;
  OnSave?: (value?: string) => void;

  ngOnInit(): void { this.OnInit();}
  OnInit():void {}

  Save(value?: string): void {
    this.OnSave?.call(this, value);
  }
}
