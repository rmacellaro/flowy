import { Component, OnInit, ViewChild } from '@angular/core';
import { CamundaModellingDraftsListComponent } from '../../modules/camunda/modelling/components/drafts/list.component';
import { Draft } from '../../modules/camunda/modelling/models/draft.model';
import { AppScopeHelper } from './scope.helper';

@Component({
  selector: 'app-camunda-scope-drafts',
  template: `
    <div class="d-flex flex-md-row flex-column w-100 h-100">

      <div class="flex-grow-0 w-40 overflow-auto border-end">

        <div class="p-3 px-4">
          <div>
            <i icon class="bi bi-bezier f-s-1-6 text-primary-gradient pe-2"></i>
            <span>Bozze</span>
          </div>
          <span subtitle>Lista delle bozze di processo bpmn per questo ambito</span>
        </div>

        <div class="p-4" *ngIf="helper.scope && helper.scope.id">
          <camunda-modelling-drafts-list #draftList [idScope]="helper.scope.id" (onSelect)="draft = $event"></camunda-modelling-drafts-list>
        </div>
      </div>

      <div class="flex-grow-1 bg-body-tertiary" *ngIf="!draft">

      </div>

      <div class="flex-grow-1 overflow-auto" *ngIf="draft && draft.id">

        <div class="p-3 px-4">
          <div>
          <i icon class="bi bi-bezier2 f-s-1-6 text-primary-gradient me-2"></i>
          <span>{{draft.name}}</span>
          </div>
          <span subtitle>{{draft.description}}</span>
        </div>

        <div class="border-bottom p-4">
          <div class="row m-0">
            <div class="col-md-6 mb-3">
              <camunda-modelling-draft-command-clone [idDraft]="draft.id" (onCloned)="onCloned($event)"></camunda-modelling-draft-command-clone>
            </div>
            <div class="col-md-6 mb-3">
              <camunda-modelling-draft-command-deploy [idDraft]="draft.id"></camunda-modelling-draft-command-deploy>
            </div>
            <div class="col-md-6 mb-3">
              <a class="btn btn-secondary w-100" href="javascript:;"
                  [routerLink]="['..','drafts', draft.id]">
                  Modifica schema
              </a>
            </div>
          </div>
        </div>
        <camunda-modelling-draft-tracks [idDraft]="draft.id"></camunda-modelling-draft-tracks>

      </div>

    </div>
  `
})
export class AppCamundaScopeDraftsComponent implements OnInit {

  @ViewChild('draftList', { static: false}) draftList?: CamundaModellingDraftsListComponent;
  public draft?: Draft;

  constructor(
    public helper: AppScopeHelper
  ) { }

  ngOnInit(): void { }

  onCloned(draft: Draft): void {
    if (!this.helper.scope || !this.helper.scope.id) { return; }
    this.draftList?.loadData(this.helper.scope.id);
    this.draft = draft;
  }
}
