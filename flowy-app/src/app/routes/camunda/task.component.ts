import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AppScopeHelper } from './scope.helper';

@Component({
  selector: 'app-camunda-scope-task',
  template: `
    <div>Task</div>
    <div *ngIf="idTask">
      <camunda-processing-task [idTask]="idTask"></camunda-processing-task>
    </div>
  `
})
export class AppCamundaScopeTaskComponent implements OnInit {

  public idTask?: number;

  constructor(
    public helper: AppScopeHelper,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      var idTask = params["idTask"];
      console.log('idTask', idTask);
      if (idTask) {
        this.idTask = parseInt(idTask);
      }
    });
  }
}
