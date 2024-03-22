import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'camunda-processing-instance-state',
  template: `
    <div *ngIf="state == 'ACTIVE'">
      <i class="bi bi-circle-fill text-success me-2"></i>
      <span>Attiva</span>
    </div>
    <div *ngIf="state == 'COMPLETED'">
      <i class="bi bi-circle-fill text-dark me-2"></i>
      <span>Completata</span>
    </div>
    <div *ngIf="state == 'CANCELED'">
      <i class="bi bi-circle-fill text-warning me-2"></i>
      <span>Cancellata</span>
    </div>
  `
})
export class CamundaProcessingInstanceStateComponent implements OnInit {

  @Input() state?: string;

  constructor() { }

  ngOnInit(): void { }
}
