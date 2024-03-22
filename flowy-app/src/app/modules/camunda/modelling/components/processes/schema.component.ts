import { Component, Input, OnInit, ViewChild } from '@angular/core';

import { CamundaModellingBpmnViewerComponent } from '../bpmn-viewer/viewer.component';
import { CamundaModellingProcessesService } from '../../services/processes.service';

@Component({
  selector: 'camunda-modelling-process-schema',
  template: `
    <div style="height: 400px;">
      <camunda-modelling-bpmn-viewer
        #viewer
        [showDeploymentsLink]="true"
        [schema]="schema">
      </camunda-modelling-bpmn-viewer>
    </div>
  `
})
export class CamundaModellingProcessSchemaComponent implements OnInit {

  @ViewChild('viewer', { static: false}) viewer?: CamundaModellingBpmnViewerComponent;

  @Input() set idProcess(value: number){
    this.loadSchema(value);
  }
  public schema?: string;
  public isInLoading: boolean = false;

  constructor(
    private processesService: CamundaModellingProcessesService
  ) { }

  ngOnInit(): void { }


  loadSchema(idProcess: number): void {
    this.isInLoading = true;
    this.processesService.GetSchemaByIdProcess(idProcess).subscribe({
      next: (result) => {
        this.isInLoading = false;
        this.schema = result;
      },
      error: (err) => {
        this.isInLoading = false;
        console.error(err);
      }
    });
  }

  addBadge(
    type: 'active' | 'canceled' | 'incidents' | 'completed',
    elementId: string,
    tot: number
  ): void {
    console.log('addBadge', type, elementId, tot);
    var color = '';
    if ( type == 'active') { color = 'success'; }
    else if ( type == 'canceled') { color = 'danger'; }
    else if ( type == 'incidents') { color = 'warning'; }
    else if ( type == 'completed') { color = 'info'; }

    var html = '<div class="border border-2 border-' + color + ' text-' + color + ' rounded p-0 px-1 bg-body-tertiary f-s-07 fw-bold">' + tot + '</div>';
    this.viewer?.addOverlay(elementId, html);
  }

  clear(): void {
    this.viewer?.clearOverlay();
  }
}
