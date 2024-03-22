import { Component, OnInit } from '@angular/core';
import { AppScopeHelper } from './scope.helper';


@Component({
  selector: 'app-camunda-scope-dashboard',
  template: `
    <div>
      <i icon class="bi bi-house f-s-1-6"></i>
      <span>Dashboard</span>
      <span subtitle>Dettaglio dello scopo</span>
    </div>

    <div class="p-4" *ngIf="helper.scope">
      <div class="display-6">{{helper.scope.name}}</div>
      <div class="fw-light">{{helper.scope.description}}</div>
    </div>
  `
})
export class AppCamundaScopeDashboardComponent implements OnInit {

  constructor(
    public helper: AppScopeHelper
  ) { }

  ngOnInit(): void { }
}
