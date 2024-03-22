import { Component, EventEmitter, Input, OnInit, Output, ViewChild, ViewContainerRef } from '@angular/core';

import { ActivitiesRegistryService } from '../services/registry.service';
import { Interaction } from '../../engine/modelling/models/interaction.model';
import { Configuration } from '../../engine/modelling/models/configuration.model';
import { ActivityDefinition } from '../models/activity-definition.interface';

@Component({
  selector: 'activity-main',
  template: `
    <div class="card">
      <div class="card-header">
        <div class="row align-items-center">
          <div class="col">
            <div class="fw-semibold f-s-1-2">{{title}}</div>
            <div class="fw-light">{{subtitle}}</div>
          </div>
          <div class="col-auto ">
            <span>Vers. </span>
            <span>{{currentActivityDefinition?.version}}</span>
          </div>
        </div>
      </div>
      <div class="card-body">
        <ng-container #container></ng-container>
      </div>
    </div>
  `
})
export class ActivityMainComponent implements OnInit {

  @ViewChild('container', {read: ViewContainerRef}) container?: ViewContainerRef
  @Output() onContinue: EventEmitter<any> = new EventEmitter();

  public currentInteraction?: Interaction;
  public currentActivityDefinition?: ActivityDefinition;

  public title?: string = '';
  public subtitle?: string = '';

  constructor(
    private registryService: ActivitiesRegistryService
  ) {
    console.log('registry service', registryService);
  }

  ngOnInit(): void { }

  setInteraction(value: Interaction): void {
    this.currentInteraction = value;
    this.container?.clear();
    console.log('SET INTERACTION', value);
    var config: Configuration | undefined = value.configurations?.find(c =>
      c.type == 'System.UI' &&
      c.name == 'Activity'
    );
    if (config && config.value) {
      this.currentActivityDefinition = this.registryService.GetActivityByPath(config.value);
      if (this.currentActivityDefinition && this.currentActivityDefinition.component){
        const compRef = this.container?.createComponent(this.currentActivityDefinition.component);
      }
    } else {
      console.error('^^^^^^^^^^^^^^','config', config, this.container);
    }

    this.title = this.getConfiguration('System.UI', 'Title')?.value;
    this.subtitle = this.getConfiguration('System.UI', 'Subtitle')?.value;
  }

  continue(data: any): void {
    this.onContinue.emit({
      idInteraction: this.currentInteraction?.id,
      ...data
    });
  }

  getConfiguration(type: string, name: string): Configuration | undefined {
    return this.currentInteraction?.configurations?.find(c => c.type == type && c.name == name);
  }
}
