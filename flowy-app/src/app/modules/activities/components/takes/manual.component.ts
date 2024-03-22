import { Component, OnInit } from '@angular/core';

import { Activity } from '../../decorators/activity.decorator';
import { ActivityMainComponent } from '../activity-main.component';

@Activity({
  group: 'Takes',
  key: 'TakeManual'
})
@Component({
  selector: 'activity-take-manual',
  template: `
    <div>
      Activity, take, manual
    </div>
    <div>
      <a href="javascript:;" class="btn btn-primary" (click)="continue()">Prova continue</a>
    </div>
  `
})
export class ActivityTakeManualComponent implements OnInit {

  constructor(
    private main: ActivityMainComponent
  ) {
    console.log('Costruttore, ActivityTakeManualComponent', main);
  }

  ngOnInit(): void { }

  continue(): void {
    this.main.continue({});
  }
}
