import { Component, Input, OnInit } from '@angular/core';

import { CamundaProcessingService } from '../../services/processing.service';
import { InstanceTask } from '../../models/instance-task.model';
import { Interaction } from '../../../modelling/models/interaction.model';

@Component({
  selector: 'camunda-processing-task',
  template: `
    <div class="position-relative">
      <div *ngIf="isInLoading">Loading...</div>
      <div>Processing Task</div>
    </div>
  `
})
export class CamundaProcessingTaskComponent implements OnInit {

  @Input() idTask?: number;

  public isInLoading: boolean = false;
  public task?: InstanceTask;
  public interaction?: Interaction;

  constructor(
    private processingService: CamundaProcessingService
  ) { }

  ngOnInit(): void {
    this.loadTask();
  }

  loadTask(): void {
    if (!this.idTask){ return; }
    this.isInLoading = true;
    this.processingService.GetInstanceTaskById(this.idTask).subscribe({
      next: (result) => {
        console.log('task', result);
        this.task = result;
        this.isInLoading = false;
        this.loadInteraction();
      },
      error: (err) => {
        console.error(err);
        this.isInLoading = false;
      }
    });
  }

  loadInteraction(): void {
    if (!this.idTask){ return; }
    this.isInLoading = true;
    this.processingService.GetInteractionByIdTask(this.idTask).subscribe({
      next: (result) => {
        console.log('interaction', result);
        this.interaction = result;
        this.isInLoading = false;
      },
      error: (err) => {
        console.error(err);
        this.isInLoading = false;
      }
    });
  }

}
