import { Component, Input, OnInit, Output, ViewChild } from '@angular/core';
import { Instance } from '../models/instance.model';
import { Wire } from '../models/wire.model';
import { EngineProcessingService } from '../services/processing.service';

import { ActivityMainComponent } from '../../activities/components/activity-main.component';
import { Distribution } from '../../modelling/models/distribution.model';
import { Process } from '../../modelling/models/process.model';
import { Activity } from '../../modelling/models/activity.model';

@Component({
  selector: 'engine-processing-instance',
  template: `
    <div class="relative">
      <ui-spinner class="absolute" [isInLoading]="isInLoading"></ui-spinner>
      <div class="row mb-3">
        <div class="col-auto">
          <div class="dropdown" *ngIf="process">
            <a class="btn btn-lg btn-secondary dropdown-toggle"
              href="javascript:;" role="button"
              data-bs-toggle="dropdown" aria-expanded="false">
              <span class="fw-light me-2">Versione:</span>
              <b>{{currentDistribution?.version}}</b>
            </a>
            <ul class="dropdown-menu">
              <li *ngFor="let item of process.distributions">
                <a class="dropdown-item" href="javascript:;" (click)="onChangedDistribution(item)">
                  {{ item.version }}
                </a>
              </li>
            </ul>
          </div>
        </div>
        <div class="col-auto">
          <div class="dropdown" *ngIf="currentInstance">
            <a class="btn btn-lg btn-secondary dropdown-toggle"
              href="javascript:;" role="button"
              data-bs-toggle="dropdown" aria-expanded="false">
              <span class="fw-light me-2">Filo:</span>
              <b>{{currentWire?.node?.key}}</b>
            </a>

            <ul class="dropdown-menu">
              <li *ngFor="let item of currentInstance.wires">
                <a class="dropdown-item" href="javascript:;" (click)="onChangedWire(item)">
                  {{ item.node?.key }}
                </a>
              </li>
            </ul>
          </div>
        </div>
        <div class="col-auto">
          <div class="dropdown" *ngIf="currentInstance">
            <a class="btn btn-lg btn-secondary dropdown-toggle"
              href="javascript:;" role="button"
              data-bs-toggle="dropdown" aria-expanded="false">
              <span class="fw-light me-2">Interazione:</span>
              <b>{{currentActivity?.key}}</b>
            </a>

            <ul class="dropdown-menu">
              <li *ngFor="let item of currentWire?.node?.activities">
                <a class="dropdown-item" href="javascript:;" (click)="onChangedActivity(item)">
                  {{ item.key }}
                </a>
              </li>
            </ul>
          </div>
        </div>
      </div>

      <div class="alert alert-danger" role="alert" *ngIf="currentWire?.state == 'ERROR'">
        <div class="alert-heading"><b>Errore</b></div>
        <div>{{currentWire?.reason}}</div>
      </div>

      <div>
        <activity-main #activityMain (onContinue)="onContinue($event)"></activity-main>
      </div>
    </div>
  `
})
export class EngineProcessingInstanceComponent implements OnInit {

  @ViewChild('activityMain', {read: ActivityMainComponent, static: false}) activityMain?: ActivityMainComponent

  @Input() process?: Process;
  @Input() idWire?: number;

  public isInLoading: boolean = false;
  public currentDistribution?: Distribution;
  public currentInstance?: Instance;
  public currentWire?: Wire;
  public currentActivity?: Activity;

  constructor(
    //private container: ViewContainerRef,
    private processingService: EngineProcessingService
  ) { }

  ngOnInit(): void {
    // presetto la distribuzione
    if (this.process){
      this.currentDistribution = this.process.distributions?.sort((a,b) => {
        if (!b.version || !a.version) { return -1; }
        return b.version - a.version;
      })[0];
    }
    if (this.idWire) { this.loadInstance(this.idWire); }
    else { this.newInstance(); }
  }

  loadInstance(idWire: number): void {
    this.isInLoading = true;
    this.processingService.GetInstanceByIdWire(idWire).subscribe({
      next: (result) => {
        this.currentInstance = result;
        this.isInLoading = false;
        console.log(result);
        var wire = this.currentInstance.wires?.find(w => w.id == idWire);
        if (wire) { this.onChangedWire(wire);}
      },
      error: (err) => {
        console.error(err);
        this.isInLoading = false;
      }
    });
  }

  newInstance(): void {
    if (!this.currentDistribution || !this.currentDistribution.id){ return; }
    this.isInLoading = true;
    this.processingService.GetStartNodeByIdDistribution(this.currentDistribution.id).subscribe({
      next: (result) => {
        console.log('START NODE', result);
        this.currentInstance = new Instance();
        this.currentInstance.idDistribution = this.currentDistribution?.id;
        const wire = new Wire();
        wire.node = result;
        wire.idNode = result.id;
        this.currentInstance.wires = [wire];
        this.isInLoading = false;
        this.onChangedWire(wire);
      },
      error: (err) => {
        console.error(err);
        this.isInLoading = false;
      }
    });
  }

  onChangedDistribution(distribution: Distribution): void {
    this.currentDistribution = distribution;
  }

  onChangedWire(wire: Wire): void {
    this.activityMain?.clear();
    this.currentWire = wire;
    console.log('onChangeWire', wire);
    this.currentWire = wire;
    if (!wire.node || !wire.node.activities) { return;}
    this.onChangedActivity(wire.node.activities[0]);
    //this.activityMain?.setActivity(wire.node);
    /*this.currentInteraction = wire.node?.interactions?.find(i => i.type == 'DEFAULT');
    if (this.currentInteraction){ this.onChangedInteraction(this.currentInteraction); }
    if(wire.state == 'ERROR'){
      this.messageService.add({
        severity: 'error',
        summary: 'Errore',
        detail: 'Il Filo selezionato è in uno stato di errore'
      });
    }*/
  }

  onChangedActivity(activity: Activity): void {
    this.currentActivity = activity;
    console.log('onChangedActivity', activity);
    if (!activity.id) { return; }
    this.activityMain?.setActivity(activity);
    //this.isInLoading = true;

    /*this.processingService.GetInteractionWithConfigurationsById(interaction.id).subscribe({
      next: (result) => {
        console.log('LOADED INTERACTION', result);
        this.interactionMain?.setInteraction(result);
        this.isInLoading = false;
      },
      error: (err) => {
        console.error(err);
        this.isInLoading = false;
      }
    });*/
  }

  onContinue(partianData: any): void {
    var data = {
      idProcess: this.process?.id,
      idDistribution: this.currentInstance?.idDistribution,
      idWire: this.currentWire?.id,
      idInstance: this.currentInstance?.id,
      ...partianData
    };
    console.log('¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶', data);
    this.isInLoading = true;
    if (this.currentInstance?.id){
      this.processingService.Continue(data).subscribe({
        next: (result) => {
          console.log(result);
          this.onChangedInstance(result);
          this.isInLoading = false;
        },
        error: (err) => {
          console.error(err);
          this.isInLoading = false;
        }
      });
    } else {
      this.processingService.Start(data).subscribe({
        next: (result) => {
          console.log(result);
          this.onChangedInstance(result);
          this.isInLoading = false;
        },
        error: (err) => {
          console.error(err);
          this.isInLoading = false;
        }
      });
    }
  }

  onChangedInstance(instance: Instance): void {
    console.log('instance', instance);
    this.currentInstance = instance;
    if (instance.wires){
      this.onChangedWire(instance.wires[0]);
    }
  }
}
