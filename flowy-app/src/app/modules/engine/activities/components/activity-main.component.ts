import { Component, EventEmitter, Input, OnInit, Output, Type, ViewChild, ViewContainerRef } from '@angular/core';

import { Activity } from '../../modelling/models/activity.model';
import { ActivityData } from '../../modelling/models/activity-data.model';
import { ActivityDefinitionsService } from '../services/activity-definitions.service';
import { ActivityDefinition } from '../models/activity-definition.model';
import { ActivityDefinitionDataType } from '../models/activity-definition-data-type.model';

import { ActivityTakeQueueComponent } from './takes/queue.component';
import { ActivityTakeManualComponent } from './takes/manual.component';
import { ActivityDecisionStandardComponent } from './decisions/standard.component';
import { ActivityFormsFormJSComponent } from './forms/form-js.component';

@Component({
  selector: 'activity-main',
  template: `
    <div class="position-relative">
      <ui-spinner [isInLoading]="isInLoading"></ui-spinner>

      <div class="rounded bg-body-tertiary py-2 px-4 mb-3" *ngIf="currentActivityDefinition">
        <div class="row align-items-center">
          <div class="col-auto">
            <i class="{{icon}} fs-1"></i>
          </div>
          <div class="col">
            <div class="fw-semibold f-s-1-2">{{title}}</div>
            <div class="fw-light">{{subtitle}}</div>
          </div>
          <div class="col-auto ">
            <span>Vers. </span>
            <span>{{version}}</span>
          </div>
        </div>
      </div>

      <div class="alert alert-danger mb-3" role="alert" *ngIf="error">
        <div class="fw-bold">Errore</div>
        <div>{{error}}</div>
      </div>

      <ng-container #container></ng-container>
    </div>
  `
})
export class ActivityMainComponent implements OnInit {

  @ViewChild('container', {read: ViewContainerRef}) container?: ViewContainerRef
  @Output() onContinue: EventEmitter<any> = new EventEmitter();

  public isInLoading: boolean = false;
  public activityDefinitions?: Array<ActivityDefinition>;

  public currentActivity?: Activity;
  public currentActivityDefinition?: ActivityDefinition;

  public icon?: string = '';
  public title?: string = '';
  public subtitle?: string = '';
  public version?: string;
  public error?: string;

  constructor(
    //private registryService: ActivitiesRegistryService
    private activityDefinitionsService: ActivityDefinitionsService
  ) {
    //console.log('registry service', registryService);

  }

  ngOnInit(): void { }

  setActivity(value: Activity): void {
    if (this.activityDefinitions) { this.drawActivity(value);}
    else {
      this.loadActivityDefinitions(() => {
        this.drawActivity(value);
      });
    }
  }

  loadActivityDefinitions(callback?: () => void): void {
    this.isInLoading = true;
    this.activityDefinitionsService.GetActivityDefinitions().subscribe({
      next: (result) => {
        this.isInLoading = false;
        this.activityDefinitions = result;
        callback?.call(this);
      },
      error: (err) => {
        this.isInLoading = false;
        console.error(err);
      }
    });
  }

  drawActivity(value: Activity): void {
    this.clear();
    this.currentActivity = value;
    //console.log('SET ACTIVITY', value, this.activityDefinitions);

    this.currentActivityDefinition = this.activityDefinitions?.find(a => a.id == value.idActivityDefinition);
    //console.log('???DEF???',this.currentActivityDefinition);
    if (!this.currentActivityDefinition) {
      console.error('^^^^^^^^^^^^^^','config', value, this.activityDefinitions);
      this.error = "Nessuna configurazione disponibile per l'activity";
      return;
    }

    var component = this.getActivityComponentByActivityDefinition(this.currentActivityDefinition);
    console.log('???TTT???', (component as any).ActivityDefinition);
    if (!component){
      console.error('^^^^^^^^^^^^^^','config', value, component);
      this.error = "Nessun componente disponibile per l'activity";
      return;
    }

    const compRef = this.container?.createComponent(component);

    this.icon = this.getActivityDefinitionDataTypeByName('Activity.Base.Icon')?.defaultValue;
    this.title = this.getActivityDefinitionDataTypeByName('Activity.Base.Title')?.defaultValue;
    this.subtitle = this.getActivityDefinitionDataTypeByName('Activity.Base.Subtitle')?.defaultValue;
    this.version = (component as any).ActivityDecorator.version;

    var iconData = this.getActivityDataByName('Activity.Base.Icon')?.value;
    if (iconData) { this.icon = iconData;}
    var titleData = this.getActivityDataByName('Activity.Base.Title')?.value;
    if (titleData) { this.title = titleData;}
    var subtitleData = this.getActivityDataByName('Activity.Base.Subtitle')?.value;
    if (subtitleData) { this.icon = subtitleData;}
  }

  clear(): void {
    console.log('Clear');
    this.container?.clear();
    this.currentActivityDefinition = undefined;
    this.error = undefined;
    this.currentActivity = undefined;
    this.version = undefined;
    this.title = undefined;
    this.subtitle = undefined;
    this.icon = undefined;
  }

  continue(data: any): void {
    this.onContinue.emit({
      idActivity: this.currentActivity?.id,
      ...data
    });
  }

  getActivityComponentByActivityDefinition(ad: ActivityDefinition): Type<any> | undefined {

    if(ad.group == 'Takes'){
      if(ad.name == 'TakeQueue'){ return ActivityTakeQueueComponent; }
      else if(ad.name == 'TakeManual'){ return ActivityTakeManualComponent; }
    } else if(ad.group == 'Decisions'){
      if(ad.name == 'DecisionStandard'){ return ActivityDecisionStandardComponent; }
    } else if(ad.group == 'Forms'){
      if(ad.name == 'FormJS'){ return ActivityFormsFormJSComponent; }
    }
    return undefined;
  }

  getActivityDataByName(name: string): ActivityData | undefined {
    return this.currentActivity?.datas?.find(c => c.name == name);
  }

  getActivityDefinitionDataTypeByName(name: string): ActivityDefinitionDataType | undefined{
    return this.currentActivityDefinition?.dataTypes?.find(c => c.name == name);
  }
}
