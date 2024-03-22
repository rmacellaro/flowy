import { AfterContentInit, Component, ElementRef, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';

import { Form } from '@bpmn-io/form-js';

@Component({
  selector: 'camunda-modelling-form-viewer',
  template: `<div class="w-100 h-100" #divForm></div>`,
  styleUrls: [ '../../common/form.scss' ]
})
export class CamundaModellingFormViewerComponent implements OnInit, OnDestroy, AfterContentInit {

  @ViewChild('divForm', { static: true }) private divForm?: ElementRef;
  @Input() schema?: any;
  public formViewer?: Form;

  constructor() {
    this.formViewer = new Form({
      container: undefined
    });
  }

  ngAfterContentInit(): void {
    this.formViewer?.attachTo(this.divForm?.nativeElement);
  }

  ngOnInit(): void {
    this.formViewer?.importSchema(this.schema).then((result) => {
      console.log('Resul import schema', result);
    }).catch((err) => {
      console.log('Errore import schema', err);
    });
  }

  ngOnDestroy(): void {
    this.formViewer?.destroy();
  }

  submit(): any {
    var result = this.formViewer?.submit();
    console.log(result);
    /*if (result?.errors) {
      alert('controllare la form ci sono degli errori');
      throw new Error('errori nella form');
    }*/
    return result?.data;
  }
}
