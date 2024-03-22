import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Instance } from '../../modules/camunda/processing/models/instance.model';
import { AppScopeHelper } from './scope.helper';
import { InstanceTask } from '../../modules/camunda/processing/models/instance-task.model';

@Component({
  selector: 'app-scope-instance',
  template: `
    <div class="w-100 h-100 overflow-auto" *ngIf="idInstance">

      <div class="row">
        <div class="col">
          <div>
            <i icon class="bi bi-list-columns f-s-1-6 text-primary-gradient me-2"></i>
            <div>
              <span>Istanza:</span>
              <span *ngIf="instance" class="mx-2 fw-bold">{{instance.reference}}</span>
            </div>
          </div>
          <span subtitle>Dettaglio istanza</span>
        </div>
        <div class="col-auto" *ngIf="instance">
          <camunda-processing-instance-state [state]="instance.state"></camunda-processing-instance-state>
        </div>
      </div>

      <div>
        <camunda-processing-instance-detail
          [idInstance]="idInstance"
          (onInstance)="instance = $event"
          (onRunTask)="onRunTask($event)">
        </camunda-processing-instance-detail>
      </div>

    </div>
  `
})
export class AppCamundaScopeInstanceComponent implements OnInit {

  public idInstance?: number;
  public instance?: Instance;

  constructor(
    public helper: AppScopeHelper,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      var idInstance = params["idInstance"];
      console.log(idInstance);
      if (idInstance) {
        this.idInstance = parseInt(idInstance);
      }
    });
  }

  onRunTask(task: InstanceTask): void {
    this.router.navigate(['../..','tasks',task.id], { relativeTo: this.route})
  }
}
