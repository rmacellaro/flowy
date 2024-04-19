import { Component, OnInit } from '@angular/core';
import { Activity } from '../../decorators/activity.decorator';
import { ActivityMainComponent } from '../activity-main.component';
import { CheckItem, Option } from './models';

@Activity({
  version: '1.0.0'
})
@Component({
  selector: 'activity-decision-standard',
  template: `
    <div *ngIf="checkItems && checkItems.length" class="list-group mb-3">
      <div class="list-group-item" *ngFor="let item of checkItems">
        <div class="row align-items-center">
          <div class="col-auto" *ngIf="item.icon">
            <i class="{{item.icon}}"></i>
          </div>
          <div class="col">
            <div class="fw-semibold">{{item.title}}</div>
            <div class="fst-italic f-s-09 text-muted">{{item.subtitle}}</div>
          </div>
        </div>
      </div>
    </div>
    <div class="row">
      <div class="col-auto mb-3" *ngFor="let item of options">
        <a href="javascript:;" class="btn {{item.cssClass}}"
        [ngClass]="{'btn-secondary': !item.cssClass}"
        (click)="choose(item)"
        [attr.title]="item.info ? item.info : null">{{item.label}}</a>
      </div>
    </div>
  `
})
export class ActivityDecisionStandardComponent implements OnInit {

  public checkItems?: Array<CheckItem>;
  public options?: Array<Option>;

  constructor(
    private main: ActivityMainComponent
  ) {
    console.log('Costruttore, ActivityDecisionStandardComponent', main);
    var optionsConfig = main.getActivityDataByName("Activity.Decision.Options");
    //console.log(optionsConfig?.value);
    if (optionsConfig && optionsConfig.value){
      this.options = JSON.parse(optionsConfig.value);
    }
    var checkItems = main.getActivityDataByName("Activity.Decision.CheckItems");
    if (checkItems && checkItems.value){
      this.checkItems = JSON.parse(checkItems.value);
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
