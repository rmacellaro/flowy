import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Process } from '../../models/process.model';
import { CamundaModellingProcessesService } from '../../services/processes.service';

@Component({
  selector: 'camunda-modelling-processes-choose',
  template: `
    <div class="dropdown w-100">
      <div class="border rounded px-3 py-2 position-relative w-100"
        [ngClass]="{'disabled': isInLoading}"
        type="button"
        data-bs-toggle="dropdown"
        aria-expanded="false">
        <div *ngIf="isInLoading">Loading...</div>
        <div class="row m-0 align-items-center">
          <div class="col">
            <span *ngIf="process">
              <ng-container *ngTemplateOutlet="tmpDeployment; context: {dep:process}"></ng-container>
            </span>
            <span *ngIf="!process" class="text-muted opacity-50 f-s-09">Seleziona un processo</span>
          </div>
          <div class="col-auto">
            <i class="bi bi-chevron-down"></i>
          </div>
        </div>
      </div>
      <ul class="dropdown-menu">
        <li *ngFor="let item of processes">
          <a class="dropdown-item" href="javascript:;" (click)="select(item)">
            <ng-container *ngTemplateOutlet="tmpDeployment; context: {dep:item}"></ng-container>
          </a>
        </li>
        <li class="dropdown-divider"></li>
        <li>
          <a class="dropdown-item text-muted" href="javascript:;" (click)="select(undefined)">Nessuno</a>
        </li>
      </ul>
    </div>

    <ng-template #tmpDeployment let-dep="dep">
      <div>{{dep.name}}</div>
      <div class="text-muted">
        <span class="fw-medium">(V.{{dep.version}})</span>
        <span class="ms-2 text-muted fst-italic">{{dep.bpmnProcessId}}</span>
      </div>
    </ng-template>

  `
})
export class CamundaModellingProcessessChooseComponent implements OnInit {

  public isInLoading: boolean = false;
  public processes?: Array<Process>;
  public process?: Process;

  @Output()
  public onSelect: EventEmitter<Process> = new EventEmitter();

  @Input() idProcess?: number;
  @Input() set idScope(value: number) {
    this.loadData(value);
  };

  constructor(
    private processesService: CamundaModellingProcessesService
  ) { }

  ngOnInit(): void { }

  loadData(idScope: number): void {
    this.isInLoading = true;
    this.processesService.GetProcessesByIdScope(idScope).subscribe({
      next: (result) => {
        this.isInLoading = false;
        this.processes = result;
        var select = this.processes.find(d => d.id == this.idProcess);
        if (select) { this.select(select); }
      },
      error: (err) => {
        this.isInLoading = false;
        console.error(err);
      }
    });
  }

  select(item?: Process): void {
    this.process = item;
    this.onSelect.emit(item);
  }
}
