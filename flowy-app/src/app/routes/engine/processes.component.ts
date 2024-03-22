import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-engine-processes',
  template: `
    <div class="container py-3">
      <layout-title class="mb-3">
        <span>Processi</span>
        <span subtitle>Lista di tutti i processi gestiti con il motore interno flowy</span>
      </layout-title>

      <engine-modelling-processes-list [commandsTemplate]="cmdTmp"></engine-modelling-processes-list>

      <ng-template #cmdTmp let-id="id">
        <a class="btn btn-link" [routerLink]="[id]">Dettaglio</a>
      </ng-template>
    </div>
  `
})
export class AppEngineProcessesComponent implements OnInit {
  constructor() { }

  ngOnInit(): void { }
}
