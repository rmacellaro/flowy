import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AppScopeHelper } from './scope.helper';

@Component({
  selector: 'app-camunda-scope-interaction',
  template: `
    <div class="w-100 h-100 d-flex flex-column" *ngIf="idInteraction">
      <div class="flex-grow-0 border-bottom">
        <div class="row m-3" *ngIf="interactionDetail.interaction">
          <div class="col">
            <div>
              <camunda-modelling-interaction-type *ngIf="interactionDetail.interaction.type" [type]="interactionDetail.interaction.type"></camunda-modelling-interaction-type>
              <span>{{interactionDetail.interaction.name}}</span>
            </div>
            <span subtitle>{{interactionDetail.interaction.description}}</span>
          </div>

          <div class="col-auto">
            <div commands class="row m-0 align-items-center">
              <div class="col"></div>
              <div class="col-auto">
                <a class="btn btn-secondary p-2 text-primary" href="javascript:;" (click)="interactionDetail.save()">
                  <i class="bi bi-save me-2"></i>
                  <span>Salva</span>
                </a>
              </div>
              <div class="col-auto">
                <a class="btn btn-secondary p-2" href="javascript:;" [routerLink]="['..']">
                  <i class="bi bi-chevron-left me-2"></i>
                  <span class="">Interazioni</span>
                </a>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="flex-grow-1 overflow-auto">
        <camunda-modelling-interaction-detail #interactionDetail [idInteraction]="idInteraction"></camunda-modelling-interaction-detail>
      </div>
    </div>
    <div *ngIf="!idInteraction">nessun interazione specificata</div>
  `
})
export class AppCamundaScopeInteractionComponent implements OnInit {

  public idInteraction?: number;

  constructor(
    private route: ActivatedRoute,
    private helper: AppScopeHelper
  ) {
    this.route.params.subscribe(params => {
      var idInteractionParm = params['idInteraction'];
      console.log('idInteraction', idInteractionParm);
      if (idInteractionParm) {
        this.idInteraction = parseInt(idInteractionParm);
      }
    });
  }

  ngOnInit(): void { }
}
