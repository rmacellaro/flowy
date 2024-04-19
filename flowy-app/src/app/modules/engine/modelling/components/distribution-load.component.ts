import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Distribution } from '../models/distribution.model';
import { EngineModellingService } from '../services/modelling.service';

@Component({
  selector: 'engine-modelling-distribution-load',
  template: `
  <div *ngIf="isInLoading" class="p-4">
    loading...
  </div>
  <div *ngIf="!distribution && !isInLoading && !error" class="p-4 text-center">
    <span class="gi text-8xl">partner_reports</span>
    <div class="text-lg">Nessun risultato trovato ...</div>
  </div>
  <div *ngIf="error" class="p-4 text-center">
    <span class="gi text-8xl text-red-800">report</span>
    <div class="text-lg text-red-800">Errore</div>
    <div>{{error}}</div>
  </div>
  <ng-content *ngIf="distribution"></ng-content>
  `
})
export class EngineModellingDistributionLoadComponent implements OnInit {

  @Input() idDistribution?: number;
  @Output() distribution?: Distribution;
  @Output() onLoadedDistribution: EventEmitter<Distribution> = new EventEmitter();

  public isInLoading: boolean = false;
  public error?: string;

  constructor(
    private modellingService: EngineModellingService
  ) {
    console.log('EngineModellingDistributionLoadComponent');
  }

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    if (!this.idDistribution) { return; }
    this.isInLoading = true;
    this.error = undefined;
    this.modellingService.GetDistributionById(this.idDistribution).subscribe({
      next: (result) => {
        this.isInLoading = false;
        this.distribution = result;
        this.onLoadedDistribution.emit(this.distribution);
        console.log('distribution-load', this.distribution);
      },
      error: (err) => {
        this.isInLoading = false;
        this.error = err.message;
      }
    });
  }
}
