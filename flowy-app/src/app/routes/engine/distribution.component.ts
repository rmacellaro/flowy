import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EngineModellingProcessLoadComponent } from '../../modules/engine/modelling/components/process-load.component';

@Component({
  selector: 'app-engine-distribution',
  template: `
    <engine-modelling-distribution-load [idDistribution]="idDistribution">
      <engine-modelling-distribution-editor></engine-modelling-distribution-editor>
    </engine-modelling-distribution-load>
  `
})
export class AppEngineDistributionComponent implements OnInit {

  public idDistribution: number;

  constructor(
    private route: ActivatedRoute,
    private pc: EngineModellingProcessLoadComponent
  ) {
    const id = this.route.snapshot.paramMap.get('idDistribution');
    if (!id) { throw new Error('no Id');}
    this.idDistribution = parseInt(id);
  }

  ngOnInit(): void { }
}
