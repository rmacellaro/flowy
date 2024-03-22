import { Component, OnInit } from '@angular/core';
import { Activity } from '../../decorators/activity.decorator';
import { ActivityMainComponent } from '../activity-main.component';
import { Option } from './option.model';

@Activity({
  group: 'Decisions',
  key: 'DecisionStandard',
  version: '1.0.0'
})
@Component({
  selector: 'activity-decision-standard',
  template: `
    <div class="mb-3" *ngFor="let item of options">
      <a href="javascript:;" class="btn btn-primary" (click)="choose(item)">{{item.label}}</a>
    </div>
  `
})
export class ActivityDecisionStandardComponent implements OnInit {

  public options?: Array<Option>;

  constructor(
    private main: ActivityMainComponent
  ) {
    console.log('Costruttore, ActivityDecisionStandardComponent', main);
    var optionsConfig = main.getConfiguration("System.UI","Options");
    if (optionsConfig && optionsConfig.value){
      this.options = JSON.parse(optionsConfig.value);
    }
  }

  ngOnInit(): void { }
  choose(option: Option): void {
    console.log('scelto', option);
    this.main.continue({
      choosed: option.value
    });
  }
}
