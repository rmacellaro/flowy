import { AfterViewInit, Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';

import { EngineModeler } from '../core/engine.modeler';
import { Distribution } from '../models/distribution.model';
import { Node } from '../models/node.model';
import { DataHelper } from '../../datas/helpers/data.helper';
import { NodeData } from '../models/node-data.model';
import { EngineModellingService } from '../services/modelling.service';
import { Activity } from '../models/activity.model';
import { NodeDataType } from '../models/node-data-type.model';

@Component({
  selector: 'engine-modelling-distribution-editor',
  template: `
    <div class="w-100 h-100 d-flex flex-row position-relative">
      <ui-spinner [isInLoading]="isInLoading"></ui-spinner>
      <div class="flex-grow-1 d-flex flex-column bg-body-tertiary">
        <div class="p-3 pb-1 flex-grow-0">
          <div class="row">
            <div class="col">
              <a (click)="modeler?.AddActivityToNode({ idNode: 1, id: 23, key: ''})">Test</a>
            </div>
            <div class="col-auto">
              <div class="btn-group" role="group" aria-label="Basic example">
                <a class="btn btn-secondary" (click)="save()"><i class="bi bi-floppy"></i></a>
              </div>
            </div>
            <div class="col-auto">
              <div class="btn-group" role="group" aria-label="Basic example">
                <a class="btn btn-secondary" (click)="modeler?.Fit()"><i class="bi bi-aspect-ratio"></i></a>
                <a class="btn btn-secondary" (click)="modeler?.ZoomIn()"><i class="bi bi-zoom-in"></i></a>
                <a class="btn btn-secondary" (click)="modeler?.ZoomOut()"><i class="bi bi-zoom-out"></i></a>
                <a class="btn btn-secondary" (click)="modeler?.ZoomReset()"><i class="bi bi-search"></i></a>
              </div>
            </div>
          </div>
        </div>
        <div class="overflow-auto flex-grow-1 p-2" #diagramDiv></div>
      </div>
      <div class="flex-grow-0 border border-left overflow-auto p-3 w-25">
        <div *ngIf="currentNode">
          <engine-modelling-node-form
            [node]="currentNode"
            (onUpdated)="updatedNode()">
          </engine-modelling-node-form>

          <!--<div class="mb-3" *ngIf="currentNode.id">
            <modelling-node-data-list [node]="currentNode" [editMode]="true" (onSaved)="updatedNode()">
            </modelling-node-data-list>
          </div>-->

          <div *ngIf="currentNode.id">
            <engine-modelling-activities-list
              (onGoToActivity)="goToActivity($event)"
              [node]="currentNode"
            ></engine-modelling-activities-list>
          </div>

          <div *ngIf="currentNode.id">
            <engine-modelling-links-list
              [node]="currentNode"
              (onGoToNode)="goToNode($event)"
            ></engine-modelling-links-list>
          </div>
        </div>

        <div *ngIf="currentActivity">
          <modelling-activity-form
            [activity]="currentActivity"
            (onUpdated)="updateActivity()">
          </modelling-activity-form>

          <div class="mb-3" *ngIf="currentActivity.id">
            <!--<datas-list
              [datas]="currentActivity.datas"
              (onUpdated)="updateActivity()"
            ></datas-list>-->
          </div>

        </div>
      </div>
    </div>
  `
})
export class EngineModellingDistributionEditorComponent implements OnInit, AfterViewInit {

  @ViewChild('diagramDiv', { static: true }) public diagramDiv?: ElementRef;
  @Input() distribution?: Distribution;

  public modeler?: EngineModeler;
  public currentNode?: Node;
  public currentActivity?: Activity;
  public isInLoading: boolean = true;

  constructor(
    private modellingService: EngineModellingService,
    private dataHelper: DataHelper
  ) { }

  ngOnInit(): void { }

  ngAfterViewInit(): void {
    setTimeout(()=>{
      if (!this.diagramDiv) { return; }
      this.modeler = new EngineModeler(this.diagramDiv.nativeElement);
      this.modeler.onSelectNode = (idNode) => {
        this.currentActivity = undefined;
        if (!idNode) { this.currentNode = undefined; }
        this.currentNode = this.distribution?.nodes?.find(n => n.id == idNode);
      };
      this.modeler.onSelectActivity = (idNode, idActivity) => {
        this.currentNode = undefined;
        if (!idNode || !idActivity) { this.currentActivity = undefined; }
        var node = this.distribution?.nodes?.find(n => n.id == idNode);
        this.currentActivity = node?.activities?.find(a => a.id == idActivity);
      }
      this.modeler.onRenderDone = () => {
        if (this.isInLoading) {
          this.isInLoading = false;
          this.modeler?.Fit();
        }
      };
      this.drawDistribution();
    }, 500);
  }


  drawDistribution(): void {
    console.log('drawDistribution');
    if (!this.distribution) { return; }
    this.distribution.nodes?.forEach((node) => {
      // aggiungo il nodo allo schema
      //console.log('ADD NODE', node);
      this.modeler?.AddNode(node);
      // aggiunco le activity al nodo
      node.activities?.forEach(activity => {
        this.modeler?.AddActivityToNode(activity);
      });
      // aggiungo gli output per il nodo
      node.outputLinks?.forEach(link => {
        this.modeler?.AddOutPortToNode(node.id, link.key);
      });
    });

    // creo i link tra i nodi
    this.distribution.nodes?.forEach((node) => {
      node.outputLinks?.forEach(link => {
        this.modeler?.AddLink(link);
      });
    });


    //this.modeler?.Positioning();
  }

  save(): void {
    this.isInLoading = true;
    var nodeDatasPoints: Array<NodeData> = [];

    this.distribution?.nodes?.forEach((node) => {
      if (!node.id || !node.datas) { return; }
      var point = this.modeler?.GetNodeElement(node.id)?.GetPosition();
      if (point) {
        var dataPoin: NodeData | undefined = node.datas.find(d => d.name == 'Schema.Position.Point');
        if (!dataPoin) { dataPoin = { idNode : node.id, name: 'Schema.Position.Point' }; }
        dataPoin.value = JSON.stringify(point);
        nodeDatasPoints.push(dataPoin);
      }
    });
    console.log(nodeDatasPoints);
    this.modellingService.SaveNodeDatas(nodeDatasPoints).subscribe({
      next: (res) => {
        console.log(res);
        this.isInLoading = false;
      },
      error: (err) => {
        console.error(err);
        this.isInLoading = false;
      }
    });
  }

  updatedNode(): void {
    if(!this.currentNode) { return; }
    this.modeler?.UpdateNode(this.currentNode);
  }

  updateActivity(): void {
    if(!this.currentActivity) { return; }
    this.modeler?.UpdateActivity(this.currentActivity);
  }

  goToNode(id: number): void {
    this.modeler?.GoToNode(id);
  }

  goToActivity(activity: Activity): void {
    if (!activity.idNode || !activity.id) { return;}
    this.modeler?.GoToActivity(activity.idNode, activity.id);
  }
}
