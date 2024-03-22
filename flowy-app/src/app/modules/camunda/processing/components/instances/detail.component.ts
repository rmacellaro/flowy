import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { Instance } from '../../models/instance.model';
import { InstanceData } from '../../models/instance-data.model';
import { InstanceTask } from '../../models/instance-task.model';
import { InstanceTrack } from '../../models/instance-track.model';
import { CamundaInstancesService } from '../../services/instances.service';
import { CamundaModellingProcessSchemaComponent } from '../../../modelling/components/processes/schema.component';

@Component({
  selector: 'camunda-processing-instance-detail',
  template: `

    <div class="position-relative" *ngIf="instance && instance.id">
      <div *ngIf="isInLoading">Loading...</div>

      <div class="border-bottom">
        <!--<process-schema
          #processSchema
          *ngIf="instance && instance.idProcess"
          [idProcess]="instance.idProcess">
        </process-schema>-->
      </div>

      <div class="border-bottom p-2">
        <ul class="nav nav-pills">
          <li class="nav-item">
            <a class="nav-link"
              [ngClass]="{'active': tab == 'DATAS'}"
              aria-current="page"
              href="javascript:;"
              (click)="switch('DATAS')">
              <i class="bi bi-database me-1"></i>
              <span>Dati</span>
            </a>
          </li>
          <li class="nav-item">
            <a class="nav-link"
              [ngClass]="{'active': tab == 'TASKS'}"
              aria-current="page"
              href="javascript:;"
              (click)="switch('TASKS')">
              <i class="bi bi-list-task me-1"></i>
              <span>Task</span>
            </a>
          </li>
          <li class="nav-item">
            <a class="nav-link"
              [ngClass]="{'active': tab == 'TRACKS'}"
              href="javascript:;"
              (click)="switch('TRACKS')">
              <i class="bi bi-list-check me-1"></i>
              <span>Tracciatura</span>
            </a>
          </li>
        </ul>
      </div>

      <div *ngIf="tab == 'DATAS'" class="p-3">
        <div class="row">
          <div class="col-md-6">
            <div class="card pt-2">
              <table class="table table-sm" *ngIf="instance">
                <thead>
                  <tr class="f-s-08 fw-light fst-italic text-muted">
                    <td class="text-end px-3">Nome</td>
                    <td class="text-start">Valore</td>
                  </tr>
                </thead>
                <tbody>
                  <tr class="align-middle">
                    <td class="bg-body-tertiary fit text-end f-s-09 text-muted px-3 border-end">Id</td>
                    <td>{{instance.id}}</td>
                  </tr>
                  <tr class="align-middle">
                    <td class="bg-body-tertiary fit text-end f-s-09 text-muted px-3 border-end">Key (camunda)</td>
                    <td>{{instance.key}}</td>
                  </tr>
                  <tr class="align-middle">
                    <td class="bg-body-tertiary fit text-end f-s-09 text-muted px-3 border-end">Riferimento</td>
                    <td>{{instance.reference}}</td>
                  </tr>
                  <tr class="align-middle">
                    <td class="bg-body-tertiary fit text-end f-s-09 text-muted px-3 border-end">Data Inizio</td>
                    <td>{{instance.startDate | date: 'medium'}}</td>
                  </tr>
                  <tr class="align-middle">
                    <td class="bg-body-tertiary fit text-end f-s-09 text-muted px-3 border-end">Data Fine</td>
                    <td>
                      <span *ngIf="instance.endDate">{{instance.endDate | date: 'medium'}}</span>
                      <span *ngIf="!instance.endDate">...</span>
                    </td>
                  </tr>
                  <tr class="align-middle">
                    <td class="bg-body-tertiary fit text-end f-s-09 text-muted px-3 border-end">Stato</td>
                    <td>
                      <camunda-processing-instance-state [state]="instance.state"></camunda-processing-instance-state>
                    </td>
                  </tr>
                  <tr class="align-middle">
                    <td class="bg-body-tertiary fit text-end f-s-09 text-muted px-3 border-end">Tenant</td>
                    <td>{{instance.tenantId}}</td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
          <div class="col-md-6">
            <div class="card pt-2">
              <table class="table table-sm" *ngIf="datas">
                <thead>
                  <tr class="f-s-08 fw-light fst-italic text-muted">
                    <td class="text-end px-3">Nome</td>
                    <td class="text-start">Valore</td>
                    <td class="text-start">Valore(C)</td>
                    <td></td>
                  </tr>
                </thead>
                <tbody>
                  <tr *ngFor="let item of datas" class="align-middle">
                    <td class="bg-body-tertiary fit text-end f-s-09 text-muted px-3 border-end">{{item.name}}</td>
                    <td class="border-end">{{item.value}}</td>
                    <td class="border-end">{{item.valueVariable}}</td>
                    <td class="fit">
                      <i *ngIf="item.keyVariable">

                      </i>
                    </td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
        </div>
      </div>

      <div *ngIf="tab == 'TASKS'">
        <table *ngIf="tasks" class="table table-sm">
          <!--<thead>
            <tr class="f-s-08 fst-italic text-muted">
              <th></th>
              <th class="fw-light">
                <div>Stato</div>
                <div class="text-muted">Tenant</div>
              </th>
              <th class="fw-light">
                <div>Nome Task</div>
                <div class="text-muted">Identificativo</div>
              </th>
              <th class="fw-light">
                <div>Data creazione</div>
                <div>Data completamento</div>
              </th>
              <th class="fw-light">
                <div>Data scadenza</div>
                <div>Data aggiornamento</div>
              </th>
              <th class="fw-light">
                <div>Assegnatario</div>
                <div class="text-muted">Candidati</div>
              </th>
            </tr>
          </thead>-->
          <tbody>
            <tr *ngFor="let item of tasks" class="align-middle">
              <td class="border-end fit px-3">
                <a class="btn btn-secondary"
                  [ngClass]="{'disabled': item.taskState != 'CREATED'}"
                  (click)="runTask(item)"
                  href="javascript:;">
                  <i class="bi bi-play"></i>
                </a>
              </td>
              <td class="border-end">
                <div>{{item.taskState}}</div>
                <div class="text-muted f-s-09">{{item.formKey}}</div>
              </td>
              <td class="border-end">
                <div>{{item.name}}</div>
                <div class="text-muted f-s-09">{{item.taskDefinitionId}}</div>
              </td>
              <td class="border-end">
                <div>{{item.creationDate | date : 'medium'}}</div>
                <div>{{item.completionDate | date : 'medium'}}</div>
              </td>
              <td class="border-end">
                <div>{{item.dueDate | date : 'medium'}}</div>
                <div>{{item.followUpDate | date : 'medium'}}</div>
              </td>
              <td class="border-end">
                <div>{{item.assignee}}</div>
                <div class="text-muted f-s-09">
                  <span *ngIf="!item.candidateGroups">...</span>
                  <span *ngIf="item.candidateGroups">{{item.candidateGroups}}</span>
                  <span class="px-2"></span>
                  <span *ngIf="!item.candidateUsers">...</span>
                  <span *ngIf="item.candidateUsers">{{item.candidateUsers}}</span>
                </div>
              </td>
            </tr>
          </tbody>
        </table>
      </div>

      <div *ngIf="tab == 'TRACKS'">
        <table *ngIf="tracks" class="table table-sm">
            <thead>
            </thead>
            <tbody>
              <tr *ngFor="let item of tracks" class="align-middle">
                <td class="fit px-3 border-end">
                  <span [ngSwitch]="item.state" *ngIf="!item.id">
                    <span *ngSwitchCase="'COMPLETED'">
                      <i class="bi bi-circle-fill text-dark"></i>
                    </span>
                    <span *ngSwitchCase="'ACTIVE'">
                      <i class="bi bi-play-circle-fill text-success"></i>
                    </span>
                    <span *ngSwitchDefault>
                      <i class="bi bi-x text-muted"></i>
                    </span>
                  </span>
                </td>
                <td class="border-end">
                  <div>{{item.state}}</div>
                  <div class="text-muted f-s-09">{{item.eventAt | date : 'medium'}}</div>
                </td>
                <td class="border-end">
                  <div>{{item.flowNodeName}}</div>
                  <div class="text-muted f-s-09">{{item.flowNodeId}}</div>
                </td>
                <td class="border-end fit">
                  <div class="text-muted f-s-09">{{item.type}}</div>
                  <div class="text-muted f-s-09">incident: {{item.incident}}</div>
                </td>
                <td class="border-end">
                  <div>{{item.startDate | date : 'medium'}}</div>
                  <div>{{item.endDate | date : 'medium'}}</div>
                </td>
              </tr>
            </tbody>
        </table>
        <!--{{tracks | json}}-->
      </div>

    </div>

  `
})
export class CamundaProcessingInstanceDetsilComponent implements OnInit {

  public isInLoading: boolean = false;
  public instance?: Instance;
  public datas?: Array<InstanceData>;
  public tasks?: Array<InstanceTask>;
  public tracks?: Array<InstanceTrack>;

  public tab: 'NONE' | 'DATAS' | 'TASKS' | 'TRACKS' = 'NONE';

  @Input() set idInstance(value: number) {
    this.loadInstance(value);
  }
  @Output() onInstance: EventEmitter<Instance> = new EventEmitter();
  @Output() onRunTask: EventEmitter<InstanceTask> = new EventEmitter();
  @ViewChild('processSchema', { static: false}) processSchema?: CamundaModellingProcessSchemaComponent;

  constructor(
    private instancesService: CamundaInstancesService
  ) { }

  ngOnInit(): void {
  }

  switch(toTab: 'DATAS' | 'TASKS' | 'TRACKS'): void {
    this.tab = toTab;
    if (toTab == 'DATAS') { this.loadDatas(); }
    if (toTab == 'TASKS') { this.loadTasks(); }
    if (toTab == 'TRACKS') { this.loadTracks(); }
  }

  loadInstance(id: number): void {
    this.isInLoading = true;
    this.instancesService.GetInstanceById(id).subscribe({
      next: (result) => {
        console.log('Instance', result);
        this.instance = result;
        this.isInLoading = false;
        this.switch('DATAS');
        this.onInstance.emit(this.instance);
      },
      error: (err) => {
        console.error(err);
        this.isInLoading = false;
      }
    });
  }

  loadDatas(): void {
    if (this.datas) {
      this.processSchema?.clear();
      return;
    }
    if (!this.instance || !this.instance.id) { return;}
    this.isInLoading = true;
    this.instancesService.GetInstanceDatasByIdInstance(this.instance.id).subscribe({
      next: (result) => {
        console.log('dati', result);
        this.datas = result;
        this.isInLoading = false;
        this.processSchema?.clear();
      },
      error: (err) => {
        console.error(err);
        this.isInLoading = false;
      }
    });
  }

  loadTasks(): void {
    if (this.tasks) {
      this.showTasks();
      return;
    }
    if (!this.instance || !this.instance.id) { return;}
    this.isInLoading = true;
    this.instancesService.GetInstanceTasksByIdInstance(this.instance.id).subscribe({
      next: (result) => {
        console.log('tasks', result);
        this.tasks = result;
        this.isInLoading = false;
        this.showTasks();
      },
      error: (err) => {
        console.error(err);
        this.isInLoading = false;
      }
    });
  }

  showTasks(): void {
    this.processSchema?.clear();
    if (this.tasks) {
      this.tasks.forEach(item => {
        if (!item.taskDefinitionId){ return; }
        this.processSchema?.addBadge("active", item.taskDefinitionId, 1);
      });
    }
  }

  loadTracks(): void {
    if (this.tracks) {
      this.showTracks();
      return;
    }
    if (!this.instance || !this.instance.id) { return;}
    this.isInLoading = true;
    this.instancesService.GetInstanceTracksByIdInstance(this.instance.id).subscribe({
      next: (result) => {
        console.log('tracks', result);
        this.tracks = result;
        this.isInLoading = false;
        this.showTracks();
      },
      error: (err) => {
        console.error(err);
        this.isInLoading = false;
      }
    });
  }

  showTracks(): void {
    this.processSchema?.clear();
    if (this.tracks) {
      this.tracks.forEach(item => {
        if (!item.flowNodeId){ return; }
        this.processSchema?.addBadge("active", item.flowNodeId, 1);
      });
    }
  }

  runTask(task: InstanceTask): void {
    this.onRunTask.emit(task);
  }
}
