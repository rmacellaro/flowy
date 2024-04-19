import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

import { Activity } from '../models/activity.model';
import { EngineModellingService } from '../services/modelling.service';
import { ActivityDefinition } from '../../activities/models/activity-definition.model';
import { ActivityDefinitionsService } from '../../activities/services/activity-definitions.service';

@Component({
  selector: 'modelling-activity-form',
  template: `
    <div class="position-relative" *ngIf="currentActivity">
      <ui-spinner [isInLoading]="isInLoading"></ui-spinner>

      <div class="mb-3">
        <div class="row align-items-center">
          <div class="col">
            <span class="fw-semibold me-2">Activity Id:</span>
            <span class="fw-light fst-italic f-s-09">
              <span *ngIf="!currentActivity.id">nuovo</span>
              <span *ngIf="currentActivity.id">{{currentActivity.id}}</span>
            </span>
          </div>
          <div class="col-auto">
            <div class="btn-group" role="group" aria-label="Basic example" title="Elimina nodo">
              <a class="btn btn-secondary"><i class="bi bi-trash"></i></a>
            </div>
          </div>
        </div>
      </div>
      <form class="mb-3">
        <div class="form-floating mb-2">
          <input type="text" class="form-control" id="key" placeholder="unique key" value="{{currentActivity.key}}">
          <label for="key">Key</label>
        </div>
        <div class="form-floating mb-2">
          <select class="form-select" id="activityDefinition" aria-label="Floating label select example" [value]="currentActivity.idActivityDefinition">

            <ng-container *ngFor="let item of activityDefinitions">
              <option [value]="item.id">{{item.group}} - {{item.name}}</option>
            </ng-container>
          </select>
          <label for="activityDefinition">Activity da eseguire</label>
        </div>
        <div class="text-end">
          <button class="btn btn-primary" type="submit">Salva</button>
        </div>
      </form>
      <!--<div class="list-group mb-3">
        <div class="list-group-item">
          <div class="f-s-08 fst-italic opacity-60">Key</div>
          <input class="w-100 border-0 m-0 p-1 px-0" [value]="currentActivity.key" />
        </div>
        <div class="list-group-item">
          <div class="f-s-08 fst-italic opacity-60">Activity Definition</div>
          <select class="w-100 border-0 m-0 p-1 px-0" id="activityDefinition" aria-label="Floating label select example" [value]="currentActivity.idActivityDefinition">
            <ng-container *ngFor="let item of activityDefinitions">
              <option [value]="item.id">{{item.group}} - {{item.name}}</option>
            </ng-container>
          </select>
        </div>
        <div class="list-group-item text-end">
          <button class="btn btn-primary" type="submit">Salva</button>
        </div>
      </div>-->

      <div class="mb-3" *ngIf="currentActivity.id">
        <modelling-activity-data-list [activity]="currentActivity" [editMode]="true" (onSaved)="onUpdated.emit()">
        </modelling-activity-data-list>
      </div>
    </div>
  `
})
export class ModellingActivityFormComponent implements OnInit {

  @Input() set activity(value: Activity){
    this.currentActivity = value;
  }

  @Output() onUpdated: EventEmitter<boolean> = new EventEmitter();

  public currentActivity?: Activity;
  public activityDefinitions?: Array<ActivityDefinition>;
  public isInLoading: boolean = false;

  constructor(
    private activityDefinitionsService: ActivityDefinitionsService
  ) { }

  ngOnInit(): void {
    this.loadActivityDefinitions();
  }

  loadActivityDefinitions(): void {
    this.isInLoading = true;
    this.activityDefinitionsService.GetActivityDefinitions().subscribe({
      next: (result) => {
        this.activityDefinitions = result;
        this.isInLoading = false;
      },
      error: (err) => {
        console.error(err);
        this.isInLoading = false;
      }
    });
  }

}
