import { AfterContentInit, Component, ElementRef, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';

import { Playground } from '@bpmn-io/form-js-playground';

@Component({
  selector: 'camunda-modelling-form-playground',
  template: `<div class="w-100 h-100" #divEditor></div>`,
  styleUrls: [ '../../common/form.scss' ]
})
export class CamundaModellingFormPlaygroundComponent implements OnInit, OnDestroy, AfterContentInit {

  @ViewChild('divEditor', { static: true }) private divEditor?: ElementRef;
  @Input() schema?: any;
  public playground?: Playground;

  constructor() {
  }

  ngAfterContentInit(): void {
    this.playground = new Playground({
      data: undefined,
      schema: this.schema,
      container: this.divEditor?.nativeElement
    });
  }

  ngOnInit(): void {
    //this.playground?.setSchema(schema);
  }

  ngOnDestroy(): void {
    this.playground?.destroy();
  }


  getSchema(): any {
    return this.playground?.saveSchema();
  }
}
