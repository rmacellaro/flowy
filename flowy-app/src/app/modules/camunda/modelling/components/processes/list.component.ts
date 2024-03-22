import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Process } from '../../models/process.model';
import { CamundaModellingProcessesService } from '../../services/processes.service';

@Component({
  selector: 'camunda-modelling-processes-list',
  template: `
    <div class="position-relative">
      <div *ngIf="isInLoading">Loading...</div>

      <div *ngIf="!processes || !processes.length">Nessun Dato torovato.</div>

      <div class="card mb-3" *ngIf="processes && processes.length">
        <div class="list-group list-group-flush">
          <a class="list-group-item list-group-item-action"
            *ngFor="let item of processes"
            href="javascript:;"
            (click)="select(item)"
            >
            <div class="row m-0 align-items-center">
              <div class="col-auto">
                <div icon class="f-s-1-6">
                  V. {{item.version}}
                </div>
              </div>
              <div class="col">
                <div class="fw-bold">{{item.bpmnProcessId}}</div>
                <div class="fw-light f-s-09">{{item.name}}</div>
                <div class="fst-italic f-s-09 text-muted">{{item.key}}</div>
              </div>
            </div>

          </a>
        </div>
      </div>
    </div>
  `
})
export class CamundaModellingProcessesListComponent implements OnInit {

  @Output() onSelect: EventEmitter<Process> = new EventEmitter();
  @Input() idScope?: number;
  public isInLoading: boolean = false;
  public processes?: Array<Process>;
  public process?: Process;

  constructor(
    private processesService: CamundaModellingProcessesService
  ) { }

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    if (!this.idScope) { return; }
    this.isInLoading = true;
    this.processesService.GetProcessesByIdScope(this.idScope).subscribe({
      next: (response) => {
        this.isInLoading = false;
        this.processes = response;
      },
      error: (err) => {
        console.error(err);
        this.isInLoading = false;
      }
    });
  }

  select(item: Process): void {
    this.process = item;
    this.onSelect.emit(item);
  }
}
