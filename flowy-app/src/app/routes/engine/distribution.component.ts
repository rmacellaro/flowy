import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EngineModellingProcessLoadComponent } from '../../modules/engine/modelling/components/process-load.component';
import { LayoutService } from '../../modules/layout/services/layout.service';

@Component({
  selector: 'app-engine-distribution',
  template: `
    <engine-modelling-distribution-load [idDistribution]="idDistribution" #distributionLoad>
      <engine-modelling-distribution-editor *ngIf="distributionLoad.distribution" [distribution]="distributionLoad.distribution"></engine-modelling-distribution-editor>
    </engine-modelling-distribution-load>
  `
})
export class AppEngineDistributionComponent implements OnInit, OnDestroy {

  public idDistribution: number;

  constructor(
    private route: ActivatedRoute,
    public layout: LayoutService,
    private pc: EngineModellingProcessLoadComponent
  ) {
    const id = this.route.snapshot.paramMap.get('idDistribution');
    if (!id) { throw new Error('no Id');}
    this.idDistribution = parseInt(id);
  }

  ngOnDestroy(): void { this.layout.sidebarToggle(true); console.log('????IEIE????'); }
  ngOnInit(): void { this.layout.sidebarToggle(false); }
}
