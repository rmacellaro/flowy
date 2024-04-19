import { Component, OnInit } from '@angular/core';

import { Activity } from '../../decorators/activity.decorator';
import { ActivityMainComponent } from '../activity-main.component';

@Activity({
  version: '1.0.0'
})
@Component({
  selector: 'activity-take-queue',
  template: `
    <div>
      Activity, takes, queues
    </div>
    <div>
      <a href="javascript:;" class="btn btn-primary" (click)="continue()">Prova continue</a>
    </div>
  `
})
export class ActivityTakeQueueComponent implements OnInit {

  constructor(
    private main: ActivityMainComponent
  ) {
    console.log('Costruttore, ActivityTakeFromQueueComponent', main);
  }

  ngOnInit(): void { }

  continue(): void {
    this.main.continue({});
  }
}
