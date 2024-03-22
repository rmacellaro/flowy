import { Component, Input, OnInit } from '@angular/core';
import { Process } from '../../../modelling/models/process.model';
import { CamundaProcessingService } from '../../services/processing.service';


@Component({
  selector: 'camunda-processing-console-main',
  template: `
    <div>Console main</div>
    <div *ngIf="isInLoading">Loading...</div>
    <div *ngIf="idScope">
      <div class="row mb-2">
        <div class="col-md-6">
          <camunda-modelling-processes-choose [idScope]="idScope" [idProcess]="idProcess" (onSelect)="process = $event"></camunda-modelling-processes-choose>
        </div>
      </div>

      <div class="row">
        <div class="col-md-6">
          <a class="btn btn-primary" (click)="start()" href="javascript:;">Avvia</a>
        </div>
      </div>
    </div>
  `
})
export class CamundaProcessingConsoleMainComponent implements OnInit {

  @Input() idScope?: number;
  @Input() idProcess?: number;

  public isInLoading: boolean = false;
  public process?: Process;

  constructor(
    private processingService: CamundaProcessingService
  ) { }

  ngOnInit(): void { }

  start(): void {
    if (!this.process || !this.process.id) { return; }
    this.isInLoading = true;
    this.processingService.Start(this.process.id).subscribe({
      next: (result) => {
        this.isInLoading = false;
        console.log('start', result);
      },
      error: (err) => {
        this.isInLoading = false;
        console.error(err);
      }
    });
  }
}
