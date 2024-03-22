import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { CamundaModellingBpmnViewerComponent } from '../bpmn-viewer/viewer.component';
import { CamundaModellingProcessesService } from '../../services/processes.service';

@Component({
  selector: 'camunda-modelling-process-detail',
  template: `

    <div class="position-relative">
      <div *ngIf="isInLoading">
        <div>Loading schema</div>
      </div>
    </div>
    <div style="height: 400px;">
      <camunda-modelling-bpmn-viewer
        #viewer
        [showDeploymentsLink]="true"
        [schema]="schema">
      </camunda-modelling-bpmn-viewer>
    </div>

    <div *ngIf="statistiscs" class="border-top">
      <table class="table table-sm table-striped">
        <thead>
          <tr>
            <th>Activity Id</th>
            <th class="fit text-center px-2">
              <span class="border border-success text-success rounded px-2 f-s-08 p-1 bg-body-tertiary">
                Attive
              </span>
            </th>
            <th class="fit text-center px-2">
              <span class="border border-warning text-warning rounded px-2 f-s-08 p-1 bg-body-tertiary">
                Cancellate
              </span>
            </th>
            <th class="fit text-center px-2 ">
              <span class="border border-danger text-danger rounded px-2 f-s-08 p-1 bg-body-tertiary">
                Error
              </span>
            </th>
            <th class="fit text-center px-2">
              <span class="border border-info text-info rounded px-2 f-s-08 p-1 bg-body-tertiary">
                Complete
              </span>
            </th>
          </tr>
        </thead>
        <tbody>
          <tr *ngFor="let item of statistiscs">
            <td>
              <a class="btn-link" href="javascript:;" (click)="selectNode(item.activityId)">
                {{item.activityId}}
              </a>
            </td>
            <td class="text-center" [ngClass]="{
              'text-success': item.active,
              'text-muted' : !item.active
            }">{{item.active}}</td>
            <td class="text-center" [ngClass]="{
              'text-warning': item.canceled,
              'text-muted' : !item.canceled
            }">{{item.canceled}}</td>
            <td class="text-center" [ngClass]="{
              'text-danger': item.incidents,
              'text-muted' : !item.incidents
            }">{{item.incidents}}</td>
            <td class="text-center" [ngClass]="{
              'text-info': item.completed,
              'text-muted' : !item.completed
            }">{{item.completed}}</td>
          </tr>
        </tbody>
      </table>
    </div>

  `
})
export class CamundaModellingProcessDetailComponent implements OnInit {

  @ViewChild('viewer') viewer?: CamundaModellingBpmnViewerComponent;
  @Input() set idProcess(value: number){
    this.loadSchema(value);
  }
  public schema?: string;
  public isInLoading: boolean = false;
  public statistiscs?: Array<any>;

  constructor(
    private processesService: CamundaModellingProcessesService
  ) { }

  ngOnInit(): void { }

  loadSchema(idProcess: number): void {
    this.isInLoading = true;
    this.processesService.GetSchemaByIdProcess(idProcess).subscribe({
      next: (result) => {
        this.isInLoading = false;
        this.schema = result;
        this.loadData(idProcess);
      },
      error: (err) => {
        this.isInLoading = false;
        console.error(err);
      }
    });
  }

  loadData(idProcess: number): void {
    if (!idProcess) { return; }
    /*this.isInLoading = true;
    this.processesService.GetStatisticsByIdProcess(idProcess).subscribe({
      next: (result) => {
        this.statistiscs = result;
        this.isInLoading = false;
        result.forEach( s => {
          if (!s.activityId) { return; }
          if (s.active) { this.viewer?.addBadge('active', s.activityId, s.active); }
          if (s.canceled) { this.viewer?.addBadge('canceled', s.activityId, s.canceled); }
          if (s.completed) { this.viewer?.addBadge('completed', s.activityId, s.completed); }
          if (s.incidents) { this.viewer?.addBadge('incidents', s.activityId, s.incidents); }
        });
      },
      error: (err) => {
        this.isInLoading = false;
        console.error(err);
      }
    });*/
  }

  selectNode(activityId?: string): void {
    if (!activityId) { return; }
    this.viewer?.centerElement(activityId);
  }
}
