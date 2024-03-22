import { Component, OnInit } from '@angular/core';
import { Process } from '../../modules/camunda/modelling/models/process.model';
import { AppScopeHelper } from './scope.helper';

@Component({
  selector: 'app-camunda-scope-processes',
  template: `
  <div class="d-flex flex-md-row flex-column w-100 h-100">

    <div class="flex-grow-0 w-35 overflow-auto border-end">

      <div>
        <div>
        <i icon class="bi bi-diagram-3 f-s-1-6 text-primary-gradient me-2"></i>
        <span>Processi</span>
        </div>
        <span subtitle>Lista dei processi distribuiti per l'ambito</span>
      </div>

      <div class="p-4" *ngIf="helper.scope">
        <camunda-modelling-processes-list [idScope]="helper.scope.id" (onSelect)="process = $event"></camunda-modelling-processes-list>
      </div>
    </div>


    <div class="flex-grow-1 bg-body-tertiary" *ngIf="!process">
    </div>

    <div class="flex-grow-1 overflow-auto" *ngIf="process && process.id">

      <div>
        <div icon class="f-s-1-6 text-primary-gradient">
          V. {{process.version}}
        </div>
        <span>{{process.name}}</span>
        <span subtitle>{{process.key}}</span>
      </div>

      <camunda-modelling-process-detail [idProcess]="process.id"></camunda-modelling-process-detail>

    </div>
  </div>
  `
})
export class AppCamundaScopeProcessesComponent implements OnInit {

  public process?: Process;

  constructor(
    public helper: AppScopeHelper
  ) { }

  ngOnInit(): void { }
}
