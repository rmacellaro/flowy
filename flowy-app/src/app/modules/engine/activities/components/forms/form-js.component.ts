import { AfterContentInit, Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';

import { Activity } from '../../decorators/activity.decorator';
import { ActivityMainComponent } from '../activity-main.component';
import { Form } from '@bpmn-io/form-js';

@Activity({
  version: '1.0.0'
})
@Component({
  selector: 'activity-forms-form-js',
  template: `
    <div class="w-100 h-100" #divForm></div>
    <div>
      <a href="javascript:;" class="btn btn-primary" (click)="continue()">Prova continue</a>
    </div>
  `,
  styles: [`
    :host ::ng-deep {
      .fjs-powered-by{ display: none !important;}
    }
    `
  ]
})
export class ActivityFormsFormJSComponent implements OnInit, OnDestroy, AfterContentInit {

  @ViewChild('divForm', { static: true }) private divForm?: ElementRef;
  public formViewer?: Form;

  constructor(
    private main: ActivityMainComponent
  ) {
    console.log('Constructor, ActivityFormsFormJSComponent', main);
    this.formViewer = new Form({
      container: undefined
    });
  }

  ngOnInit(): void {
    var config = this.main.getActivityDataByName('Activity.Form.Schema');

    console.log('SONO Q', config?.value);
    if (!config?.value){ return; }
    var schema: any = JSON.parse(config?.value);
    var data: any = {
      Field_03cn0tx: [
        { id: 34235, name: 'pippo palla', value: 'ma che ne so'},
        { id: 44356, name: 'ma tu ti rendi conto', value: 'caspita'}
      ]
    };
    this.formViewer?.importSchema(schema, data).then((result) => {
      console.log('Resul import schema', result);
    }).catch((err) => {
      console.log('Errore import schema', err);
    });


  }

  ngAfterContentInit(): void {
    this.formViewer?.attachTo(this.divForm?.nativeElement);
  }

  continue(): void {
    var result = this.formViewer?.submit();
    if (!result) { return; }
    //if (result.errors.)
    console.log(result);
    this.main.continue(result?.data);
  }

  ngOnDestroy(): void {
    this.formViewer?.destroy();
  }
}
