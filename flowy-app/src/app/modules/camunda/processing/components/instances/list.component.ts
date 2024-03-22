import { Component, EventEmitter, Input, OnInit, Output, TemplateRef } from '@angular/core';
import { Process } from '../../../modelling/models/process.model';
import { Instance } from '../../models/instance.model';
import { Result } from '../../../common/models/result.model';
import { Request } from '../../../common/models/request.model';
import { CamundaInstancesService } from '../../services/instances.service';
import { Query } from '../../../common/models/query.model';



@Component({
  selector: 'camunda-processing-instances-list',
  template: `
    <div class="position-relative" *ngIf="idScope">
      <div *ngIf="isInLoading"> Loading ...  </div>
      <div class="p-4 text-muted opacity-50 text-center"
        *ngIf="!instances || !instances.length">
        Nessun dato
      </div>

      <div class="list-group" *ngIf="instances && instances.length">
        <div class="list-group-item" *ngFor="let item of instances">

          <ng-container *ngIf="itemTemplate">
            <ng-container *ngTemplateOutlet="itemTemplate; context: { $implicit: item  }">
            </ng-container>
          </ng-container>

          <ng-container *ngIf="!itemTemplate">
            <div class="row">
              <div class="col-auto">
                <div *ngIf="!item.state">
                  <i class="bi bi-hourglass text-success me-2"></i>
                  <span>Avvio</span>
                </div>
                <div *ngIf="item.state == 'ACTIVE'">
                  <i class="bi bi-circle-fill text-success me-2"></i>
                  <span>Attiva</span>
                </div>
                <div *ngIf="item.state == 'COMPLETED'">
                  <i class="bi bi-circle-fill text-dark me-2"></i>
                  <span>Completa</span>
                </div>
                <div *ngIf="item.state == 'CANCELED'">
                  <i class="bi bi-circle-fill text-warning me-2"></i>
                  <span>Cancellata</span>
                </div>
                <div class="f-s-08 text-muted">{{item.createdAt | date}}</div>
              </div>

              <div class="col">
                <div>{{item.reference}}</div>
                <div class="f-s-08 text-muted">[{{item.id}}] - {{item.key}}</div>
              </div>

              <div class="col-auto" *ngIf="itemCommandsTemplate">
                <ng-container *ngTemplateOutlet="itemCommandsTemplate; context: { $implicit: item }">
                </ng-container>
              </div>

              <div class="col-auto" *ngIf="!itemCommandsTemplate">
                <a class="btn btn-secondary" (click)="select(item)">
                  Seleziona
                </a>
              </div>

            </div>
          </ng-container>
        </div>
      </div>

      <div class="my-3">
        <div class="row m-0" *ngIf="lastResult">
          <div class="col">
            <span class="">Visualizzati</span>
            <span class="mx-2 fw-bold">{{lastResult.items?.length}}</span>
            <span class="">di</span>
            <span class="mx-2 fw-bold">{{lastResult.total}}</span>
          </div>
          <div class="col-auto">
          </div>
        </div>
      </div>

    </div>
  `
})
export class CamundaProcessingInstancesListComponent implements OnInit {

  @Input() idScope?: number;
  @Input() pageSize: number = 10;
  @Input() itemTemplate?: TemplateRef<any>;
  @Input() itemCommandsTemplate?: TemplateRef<any>;

  @Input() public set process(value: Process){
    this.currentProcess = value;
    this.loadData();
  }

  @Input() public set state(value: string | undefined) {
    if (!value) { return;}
    this.currentState = value;
    this.loadData();
  }

  @Output() public instances?: Array<Instance>;
  @Output() public selected?: Instance;
  @Output() public onSelect: EventEmitter<Instance> = new EventEmitter();

  public isInLoading: boolean = false;

  public currentProcess?: Process;
  public currentState?: string;
  public lastResult?: Result<Instance>;

  constructor(
    private instancesService: CamundaInstancesService
  ) { }

  ngOnInit(): void { }

  loadData(): void {
    this.instances = undefined;
    this.lastResult = undefined;
    if (!this.currentProcess) { return; }
    const request: Request = new Request();
    request.size = this.pageSize;
    request.queries = new Array<Query>();
    const queryProcess = new Query();
    queryProcess.column = "IdProcess";
    queryProcess.method = "Equals";
    queryProcess.value = this.currentProcess?.id;
    request.queries.push(queryProcess);
    if(this.state) {
      const qs = new Query();
      qs.column = "IdProcess";
      qs.method = "Equals";
      qs.value = this.state;
      request.queries.push(qs);
    }

    this.isInLoading = true;
    this.instancesService.GetInstancesByIdProcess(request).subscribe({
      next: (result) => {
        this.isInLoading = false;
        console.log('RESULT', result);
        this.lastResult = result;
        this.instances = result.items;
      },
      error: (err) => {
        this.isInLoading = false;
        console.error(err);
      }
    });
  }

  select(item: Instance): void {
    this.onSelect.emit(item);
    this.selected = item;
  }
}
