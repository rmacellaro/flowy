import { Component, OnInit } from '@angular/core';

import { Activity } from '../../decorators/activity.decorator';
import { ActivityMainComponent } from '../activity-main.component';

@Activity({
  version: '1.0.0'
})
@Component({
  selector: 'activity-system-not-found',
  template: `
    <div>Activity Not Found</div>
  `
})
export class ActivitySystemNotFoundComponent implements OnInit {

  public static prova: string;

  constructor(
    private main: ActivityMainComponent
  ) {
    console.log('Costruttore, ActivitySystemNotFoundComponent',
      (ActivitySystemNotFoundComponent as any).ActivityDefinition
    );
  }

  ngOnInit(): void { }
}

