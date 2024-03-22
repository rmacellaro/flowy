import { Component, Input, OnInit } from '@angular/core';
import { Distribution } from '../models/distribution.model';

@Component({
  selector: 'engine-modelling-distribution-version',
  template: `
  <span class="" *ngIf="distribution">
    <span class="rounded px-3 py-1"
      [ngStyle]="{
        'background-color': distribution.state == 'TEST' ? 'var(--bs-yellow)'  : ''
      }"
    >{{distribution.version}}</span>
    <span class="ms-2">{{distribution.state}}</span>
  </span>
  `
})
export class EngineModellingDistributionVersionComponent implements OnInit {

  @Input() distribution?: Distribution;

  constructor() { }

  ngOnInit(): void { }
}
