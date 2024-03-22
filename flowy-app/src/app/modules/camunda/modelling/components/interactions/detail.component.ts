import { Component, Input, OnInit } from '@angular/core';
import { Interaction } from '../../models/interaction.model';
import { CamundaModellingInteractionsService } from '../../services/interactions.service';

@Component({
  selector: 'camunda-modelling-interaction-detail',
  template: `
    <div *ngIf="isInLoading">Loading...</div>
    <camunda-modelling-form-editor *ngIf="schema" [schema]="schema"></camunda-modelling-form-editor>
  `
})
export class CamundaModellingInteractionDetailComponent implements OnInit {

  public isInLoading: boolean = false;

  @Input() set idInteraction(value: number) {
    this.loadData(value);
  }

  public interaction?: Interaction;
  public schema?: any;

  constructor(
    private interactionsService: CamundaModellingInteractionsService
  ) { }

  ngOnInit(): void { }

  loadData(id: number): void {
    this.isInLoading = true;
    this.interactionsService.GetInteractionById(id).subscribe({
      next: (result) => {
        this.isInLoading = false;
        this.interaction = result;
        console.log('interaction:', this.interaction);
        if(this.interaction.data) {
          this.schema = JSON.parse(this.interaction.data);
        }
      },
      error: (err) => {
        this.isInLoading = false;
        console.error(err);
      }
    });
  }

  save(): void {
    console.log("devo salvare");
  }
}
