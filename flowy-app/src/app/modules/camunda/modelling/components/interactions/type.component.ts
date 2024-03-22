import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'camunda-modelling-interaction-type',
  template: `
    <span *ngIf="type == 'none'">No</span>
    <span *ngIf="type == 'camunda-form'">
      <i class="bi bi-input-cursor-text me-2"></i>
      <span>Form</span>
    </span>
  `
})
export class CamundaModellingInteractionTypeComponent implements OnInit {

  @Input() type: string = 'none';

  constructor() { }

  ngOnInit(): void { }
}
