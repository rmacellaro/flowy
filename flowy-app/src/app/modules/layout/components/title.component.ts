import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'layout-title',
  template: `
    <div class="d-flex flex-row">
      <div class="flex-grow-0 p-2" *ngIf="showIcon">
        <ng-content select="[icon]"></ng-content>
      </div>
      <div class="flex-grow-1 px-2">
        <div class="fw-semibold fs-2"><ng-content></ng-content></div>
        <div class="fst-italic f-s-09 opacity-50"><ng-content select="[subtitle]"></ng-content></div>
      </div>
      <div class="flex-grow-0 p-2">
        <ng-content select="[commands]"></ng-content>
      </div>
    </div>
  `,
  host: {
    class: 'd-block bg-body-tertiary rounded p-2 px-3'
  }
})
export class LayoutTitleComponent implements OnInit {

  @Input() showIcon: boolean = true;

  constructor() { }

  ngOnInit(): void { }
}
