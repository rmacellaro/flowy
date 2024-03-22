import { Component, OnInit } from '@angular/core';
import { EngineModellingProcessLoadComponent } from '../../modules/engine/modelling/components/process-load.component';

@Component({
  selector: 'app-engine-instances',
  template: `
    <div class="p-4">

      <layout-title class="mb-3">
        <i icon class="bi bi-list-task f-s-1-5"></i>
        <span>Istanze</span>
        <span subtitle>lista delle istanze in lavorazione per il processo</span>
        <a commands class="btn btn-primary" [routerLink]="['..','instance',0]">Nuova</a>
      </layout-title>

      <engine-processing-instances-list [idProcess]="idProcess" [wireTemplate]="tmpWire"></engine-processing-instances-list>

      <ng-template #tmpWire let-id="id" let-state="state" let-node="node">
        <div>
          <span class="fw-semibold me-2">{{node.title}}</span>
          <span class="me-2">[{{state}}]</span>
          <a class="btn btn-secondary" [routerLink]="['..','instance', id]">continua</a>
        </div>
      </ng-template>

    </div>
  `
})
export class AppEngineInstancesComponent implements OnInit {
  public idProcess: number;

  constructor(
    private pc: EngineModellingProcessLoadComponent
  ) {
    if (!pc.idProcess){ throw new Error('no process');}
    this.idProcess = pc.idProcess;
  }

  ngOnInit(): void { }
}
