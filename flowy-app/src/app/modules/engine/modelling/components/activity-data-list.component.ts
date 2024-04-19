import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

import { DataMap } from '../../datas/models/data-map.model';
import { Activity } from '../models/activity.model';
import { EngineModellingService } from '../services/modelling.service';
import { ActivityDefinition } from '../../activities/models/activity-definition.model';
import { ActivityDefinitionsService } from '../../activities/services/activity-definitions.service';


@Component({
  selector: 'modelling-activity-data-list',
  template: `
    <div class="position-relative">
      <ui-spinner [isInLoading]="isInLoading"></ui-spinner>

      <data-maps-list
        *ngIf="currentActivityDefinitionDataMaps"
        [dataMaps]="currentActivityDefinitionDataMaps"
        [editMode]="editMode"
        (onUpdateDataMap)="onUpdateDataMap($event)"
      ></data-maps-list>
    </div>
  `
})
export class ModellingActivityDataListComponent implements OnInit {

  @Input() set activity(value: Activity){
    console.log('SET ACTIVITY', value);
    this.currentActivity = value;
    this.loadActivityDefinition();
  };
  @Input() public editMode: boolean = false;

  @Output() public onSaved: EventEmitter<boolean> = new EventEmitter();

  public isInLoading: boolean = false;
  public currentActivity?: Activity;
  public currentActivityDefinition?: ActivityDefinition;
  public currentActivityDefinitionDataMaps?: Array<DataMap>;

  constructor(
    private activityDefinitionsService: ActivityDefinitionsService
  ) { }

  ngOnInit(): void { }

  loadActivityDefinition(): void {
    if (!this.currentActivity || !this.currentActivity.idActivityDefinition){ return; }
    this.isInLoading = true;
    this.activityDefinitionsService.GetActivityDefinitionById(this.currentActivity.idActivityDefinition).subscribe({
      next: (ad) => {
        console.log('AD', ad);
        this.isInLoading = false;
        this.currentActivityDefinition = ad;
        this.mapping();
      },
      error: (err) => {
        this.isInLoading = false;
        console.error(err);
      }
    });
  }

  /**
   * fa il mapping del datatype con data
   */
  mapping(): void {
    //console.log('NNNNNNNNNNDDDDDD', this.currentNode?.datas);
    if (!this.currentActivityDefinition || !this.currentActivityDefinition.dataTypes) { return; }
    this.activity?.datas ?? [];
    this.currentActivityDefinitionDataMaps = [];
    this.currentActivityDefinition.dataTypes.sort((a, b) => {
      if (a.index == undefined || b.index == undefined) { return 0;}
      if (a.index < b.index) { return -1; }
      if (a.index > b.index) { return 1; }
      return 0;
    });
    this.currentActivityDefinition.dataTypes.forEach(dataType => {
      console.log('dt', dataType);
      this.currentActivityDefinitionDataMaps?.push({
        dataType : dataType,
        data : this.currentActivity?.datas?.find(d => d.name == dataType.name),
        state : 'NONE'
      });
    });
  }


  onUpdateDataMap(dataMap: DataMap): void {
    /*if (!this.editMode) { return; }
    if (!this.currentNode || !this.currentNode.id){ return; }
    if (!dataMap.data) { return; }
    var data = dataMap.data as NodeData;
    if (dataMap.state == 'NEW') {
      data.idNode = this.currentNode.id;
      data.idNodeDataType = dataMap.dataType?.id;
    }

    this.isInLoading = true;
    console.log('To save', data);
    this.modellingService.SaveNodeData(data).subscribe({
      next: (nd) => {
        dataMap.data = nd;
        dataMap.state = 'SAVED';
        this.marge(nd);
        this.isInLoading = false;
        this.onSaved.emit();
        console.log('saved', nd);
      },
      error: (err) => {
        console.error(err);
        this.isInLoading = false;
      }
    });*/
  }
}
