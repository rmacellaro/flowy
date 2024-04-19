import { Component, Input, OnInit, Output } from '@angular/core';
import { Process } from '../models/process.model';
import { EngineModellingService } from '../services/modelling.service';

@Component({
  selector: 'engine-modelling-process-load',
  template: `
  <div *ngIf="isInLoading" class="p-4">
    loading...
  </div>
  <div *ngIf="!process && !isInLoading && !error" class="p-4 text-center">
    <span class="gi text-8xl">partner_reports</span>
    <div class="text-lg">Nessun risultato trovato ...</div>
  </div>
  <div *ngIf="error" class="p-4 text-center">
    <span class="gi text-8xl text-red-800">report</span>
    <div class="text-lg text-red-800">Errore</div>
    <div>{{error}}</div>
  </div>
  <ng-content></ng-content>
  `
})
export class EngineModellingProcessLoadComponent implements OnInit {

  @Input() idProcess?: number;
  @Output() process?: Process;

  public isInLoading: boolean = false;
  public error?: string;

  constructor(
    private modellingService: EngineModellingService
  ) {
    console.log('EngineModellingProcessLoadComponent');
  }

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    if (!this.idProcess) { return; }
    this.isInLoading = true;
    this.error = undefined;
    this.modellingService.GetProcessById(this.idProcess).subscribe({
      next: (result) => {
        this.isInLoading = false;
        this.process = result;
        console.log('processes-load', this.process);
      },
      error: (err) => {
        this.isInLoading = false;
        this.error = err.message;
      }
    });
  }
}
