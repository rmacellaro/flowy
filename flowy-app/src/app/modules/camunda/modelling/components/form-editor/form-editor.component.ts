import { AfterContentInit, Component, ElementRef, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';

import { FormEditor } from '@bpmn-io/form-js';

@Component({
  selector: 'camunda-modelling-form-editor',
  template: `<div class="w-100 h-100" #divEditor></div>`,
  styleUrls: [ '../../common/form.scss' ]
})
export class CamundaModellingFormEditorComponent implements OnInit, OnDestroy, AfterContentInit {

  @ViewChild('divEditor', { static: true }) private divEditor?: ElementRef;
  @Input() schema?: any;
  public formEditor?: FormEditor;

  constructor(
  ) {
    this.formEditor = new FormEditor({
      container: undefined
    });
  }

  ngAfterContentInit(): void {
    this.formEditor?.attachTo(this.divEditor?.nativeElement);
  }

  ngOnInit(): void {
    this.formEditor?.importSchema(this.schema).then((result) => {
      console.log('Resul import schema', result);
    }).catch((err) => {
      console.log('Errore import schema', err);
    });
  }

  ngOnDestroy(): void {
    this.formEditor?.destroy();
  }

  getSchema(): any {
    return this.formEditor?.saveSchema();
  }
}
