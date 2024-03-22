import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EngineProcessingInstanceComponent } from '../../modules/engine/processing/components/instance.component';
import { EngineModellingProcessLoadComponent } from '../../modules/engine/modelling/components/process-load.component';
import { Process } from '../../modules/engine/modelling/models/process.model';

@Component({
  selector: 'app-engine-instance',
  template: `
    <div class="p-4">
      <engine-processing-instance
        [process]="process"
        [idWire]="idWire">
      </engine-processing-instance>
    </div>
  `
})
export class AppEngineInstanceComponent implements OnInit, AfterViewInit {

  //@ViewChild('instanceProcessing') instanceProcessing?: EngineProcessingInstanceComponent;

  public idWire: number;
  public process: Process;

  constructor(
    private route: ActivatedRoute,
    private pc: EngineModellingProcessLoadComponent
  ) {
    const id = this.route.snapshot.paramMap.get('idWire');
    if (!id) { throw new Error('no Id');}
    this.idWire = parseInt(id);

    if (!pc.process || !pc.process.id){ throw new Error('No process');}
    this.process = pc.process;
  }

  ngOnInit(): void { }

  ngAfterViewInit(): void {

  }
}
