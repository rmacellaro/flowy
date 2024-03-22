import { Component, OnInit, ViewChild } from '@angular/core';
import { Interaction } from '../../modules/camunda/modelling/models/interaction.model';
import { CamundaModellingInteractionsListComponent } from '../../modules/camunda/modelling/components/interactions/list.component';
import { AppScopeHelper } from './scope.helper';
@Component({
  selector: 'app-camunda-scope-interactions',
  template: `
    <div class="d-flex flex-md-row flex-column w-100 h-100">

      <div class="flex-grow-0 w-40 overflow-auto border-end">

        <div class="p-3">
          <div class="">
            <i icon class="bi bi-window f-s-1-6 text-primary-gradient me-2"></i>
            <span>Interazioni</span>
          </div>
          <span subtitle>Lista delle interazioni processo-utente per questo ambito</span>
        </div>

        <div class="p-4" *ngIf="helper.scope && helper.scope.id">
          <camunda-modelling-interactions-list #interactionsList
            [idScope]="helper.scope.id"
            (onSelect)="interaction = $event">
          </camunda-modelling-interactions-list>
        </div>
      </div>

      <div class="flex-grow-1 bg-body-tertiary" *ngIf="!interaction">

      </div>

      <div class="flex-grow-1 overflow-auto" *ngIf="interaction && interaction.id">

        <div>
          <div>
            <camunda-modelling-interaction-type icon *ngIf="interaction.type" [type]="interaction.type"></camunda-modelling-interaction-type>
            <span>{{interaction.name}}</span>
          </div>
          <span subtitle>{{interaction.description}}</span>
        </div>

        <div class="border-bottom p-4">
          <div class="row m-0">
            <!--<div class="col-md-6 mb-3">
              <draft-command-clone [idDraft]="draft.id" (onCloned)="onCloned($event)"></draft-command-clone>
            </div>
            <div class="col-md-6 mb-3">
              <draft-command-deploy [idDraft]="draft.id"></draft-command-deploy>
            </div>-->
            <div class="col-md-6 mb-3">
              <a class="btn btn-secondary w-100" href="javascript:;"
                  [routerLink]="['..','interactions', interaction.id]">
                  Modifica
              </a>
            </div>
          </div>
        </div>
        <!--<draft-tracks [idDraft]="draft.id"></draft-tracks>-->

      </div>

    </div>
  `
})
export class AppCamundaScopeInteractionsComponent implements OnInit {

  @ViewChild('interactionsList', { static: false}) draftList?: CamundaModellingInteractionsListComponent;
  public interaction?: Interaction;

  constructor(
    public helper: AppScopeHelper
  ) { }

  ngOnInit(): void { }
}
