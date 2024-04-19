import { Component, Input, OnInit, TemplateRef } from '@angular/core';
import { EngineModellingService } from '../services/modelling.service';
import { Process } from '../models/process.model';

@Component({
  selector: 'engine-modelling-processes-list',
  template: `

    <div class="card mb-3" *ngIf="processes">
      <ul class="list-group list-group-flush">
        <li class="list-group-item list-group-item-action" *ngFor="let item of processes">
          <div class="row align-items-center m-0">
            <div class="col-auto px-3">
              <i class="bi bi-diagram-3 f-s-1-5"></i>
            </div>
            <div class="col">
              <div class="fw-bold fs-5">{{item.name}}</div>
              <div>{{item.description}}</div>
            </div>
            <div class="col-auto px-3">
              <span>dis.</span>
              <span *ngIf="item.distributions">{{item.distributions.length}}</span>
              <span *ngIf="!item.distributions">0</span>
            </div>
            <div class="col-auto px-2" *ngIf="commandsTemplate">
              <ng-container
                [ngTemplateOutlet]="commandsTemplate"
                [ngTemplateOutletContext]="item">
              </ng-container>
            </div>
          </div>
        </li>
      </ul>
    </div>
  `
})
export class EngineModellingProcessesListComponent implements OnInit {

  @Input() commandsTemplate?: TemplateRef<Process>;

  public isInLoading: boolean = false;
  public processes?: Array<Process>;

  constructor(
    private modellingService: EngineModellingService
  ) { }

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.isInLoading = true;
    this.modellingService.GetProcesses().subscribe({
      next: (result) => {
        this.isInLoading = false;
        this.processes = result;
      },
      error: (err) => {
        this.isInLoading = false;
        /*this.messageService.add({
          severity: 'error',
          summary: 'Errore',
          detail: err.message
        });*/
      }
    });
  }
}
