import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Process } from '../../modules/camunda/modelling/models/process.model';
import { AppScopeHelper } from './scope.helper';

@Component({
  selector: 'app-camunda-scope-instances',
  template: `
    <div class="d-flex flex-md-row flex-column w-100 h-100" *ngIf="helper.scope && helper.scope.id">

    <div class="flex-grow-0 w-30 overflow-auto border-end">
      <div class="p-3">
        <div>
          <i icon class="bi bi-search f-s-1-6 text-primary-gradient me-2"></i>
          <span>Ricerca</span>
        </div>
        <span subtitle>Ricerca istanze</span>
      </div>

      <div class="p-3">
        <div class="mb-3">
          <camunda-modelling-processes-choose
            [idProcess]="idProcessPreselect"
            [idScope]="helper.scope.id"
            (onSelect)="selectProcess($event)">
          </camunda-modelling-processes-choose>
        </div>

        <div class="mb-2">
          <camunda-processing-instance-choose-state
            [state]="statePreselect"
            (OnChoose)="selectState($event)">
          </camunda-processing-instance-choose-state>
        </div>
      </div>
    </div>

    <div class="flex-grow-1 overflow-auto">
      <div class="p-3">
        <div>
          <i icon class="bi bi-list-columns f-s-1-6 text-primary-gradient me-2"></i>
          <span>Istanze</span>
        </div>
        <span subtitle>Lista delle istanze di processo per questo ambito</span>
      </div>

      <div class="p-3" *ngIf="process">
        <camunda-processing-instances-list
          [idScope]="helper.scope.id"
          [process]="process"
          [state]="state"
          [itemCommandsTemplate]="itemCommands">

          <ng-template #itemCommands let-item>
            <a *ngIf="item" class="btn btn-secondary" [routerLink]="['.', item.id]">Dettaglio</a>
          </ng-template>

        </camunda-processing-instances-list>

      </div>
    </div>
  </div>
  `
})
export class AppCamundaScopeInstancesComponent implements OnInit {

  // preselezione da quaryString
  public idProcessPreselect?: number;
  public statePreselect?: string;

  // selezioni attuali
  public state?: string;
  public process?: Process;

  constructor(
    public helper: AppScopeHelper,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      var idProcess = params["idProcess"];
      if (idProcess) { this.idProcessPreselect = parseInt(idProcess); }
      var state = params["state"];
      if (state) { this.statePreselect = state; }
    });
  }

  selectProcess(process: Process): void {
    this.process = process;
    this.addQuaryParam({ idProcess: process.id });
  }

  selectState(state: string): void {
    this.state = state;
    this.addQuaryParam({ state });
  }

  private addQuaryParam(param: any): void {
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: param,
      queryParamsHandling: 'merge'
    });
  }

}
