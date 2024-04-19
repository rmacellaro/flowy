import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Node } from '../models/node.model';
import { Activity } from '../models/activity.model';

@Component({
  selector: 'engine-modelling-activities-list',
  template: `
    <div class="row m-0 mb-2 align-items-center">
      <div class="col">
        Activities
      </div>
      <div class="col-auto">
        <a class="btn btn-link" href="javascript:;"><i class="bi bi-plus"></i></a>
      </div>
    </div>
    <div class="bg-body-tertiary rounded p-2 mb-2" *ngFor="let item of node?.activities">
      <div class="row m-0">
        <div class="col-auto opacity-50">{{item.index}}</div>
        <!--<div class="col-auto">
          <i class="bi bi-tag-fill fw-bold text-primary" *ngIf="item.isDefault"></i>
          <i class="bi bi-tag" *ngIf="!item.isDefault"></i>
        </div>-->
        <div class="col">
          <a href="javascript:;" (click)="goToActivity(item)">
            {{item.key}}
          </a>
        </div>
        <div class="col-auto"></div>
      </div>
    </div>
  `
})
export class EngineModellingActivitiesListComponent implements OnInit {

  @Input() node?: Node;
  @Output() onGoToActivity: EventEmitter<Activity> = new EventEmitter();

  constructor() { }

  ngOnInit(): void { }

  goToActivity(item: Activity): void {
    this.onGoToActivity.emit(item);
  }
}
