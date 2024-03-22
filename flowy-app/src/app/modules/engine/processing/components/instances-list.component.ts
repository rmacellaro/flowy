import { Component, Input, OnInit, Output, TemplateRef } from '@angular/core';
import { EngineProcessingService } from '../services/processing.service';
import { Process } from '../../modelling/models/process.model';
import { Instance } from '../models/instance.model';
import { Wire } from '../models/wire.model';

@Component({
  selector: 'engine-processing-instances-list',
  template: `
    <div *ngIf="isInLoading" class="p-4">
      loading...
    </div>
    <div *ngIf="!instances && !isInLoading && !error" class="p-4 text-center">
      <span class="gi text-8xl">partner_reports</span>
      <div class="text-lg">Nessun risultato trovato ...</div>
    </div>
    <div *ngIf="error" class="p-4 text-center">
      <span class="gi text-8xl text-red-800">report</span>
      <div class="text-lg text-red-800">Errore</div>
      <div>{{error}}</div>
    </div>

    <div class="card mb-3" *ngIf="instances">
      <ul class="list-group list-group-flush">
        <li class="list-group-item list-group-item-action" *ngFor="let item of instances">
          <div class="row align-items-center m-0">
            <div class="col-auto px-3">
              <i class="bi bi-list-task"></i>
            </div>
            <div class="col">
              <div class="font-semibold">{{item.key}}</div>
              <div>{{item.createdDateTime}}</div>
            </div>
            <div class="col-auto px-3">
              <span *ngFor="let item of item.wires">
                <ng-container *ngIf="wireTemplate" [ngTemplateOutlet]="wireTemplate" [ngTemplateOutletContext]="item"></ng-container>
                <span *ngIf="!wireTemplate">{{item.state}}</span>
              </span>
            </div>
            <div class="col-auto px-2" *ngIf="commandsTemplate">
              <ng-container
                [ngTemplateOutlet]="commandsTemplate"
                [ngTemplateOutletContext]="item">
              </ng-container>
            </div>
          </div>
        </li>
      </ul>
    </div>
  `
})
export class EngineProcessingInstancesListComponent implements OnInit {

  public isInLoading: boolean = false;
  public error?: string;

  @Input() idProcess?: number;
  @Input() commandsTemplate?: TemplateRef<Instance>;
  @Input() wireTemplate?: TemplateRef<Wire>;
  @Output() instances?: Array<Instance>;

  constructor(
    private processingService: EngineProcessingService
  ) { }

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    if (!this.idProcess){ return;}
    this.isInLoading = true;
    this.error = undefined;
    this.processingService.GetInstancesByIdProcess(this.idProcess).subscribe({
      next: (result) => {
        this.isInLoading = false;
        this.instances = result;
        console.log('instances-list', this.instances);
      },
      error: (err) => {
        this.isInLoading = false;
        this.error = err.message;
      }
    });
  }
}
