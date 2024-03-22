import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

import { CamundaModellingDraftsService } from '../../services/drafts.service';
import { Draft } from '../../models/draft.model';

@Component({
  selector: 'camunda-modelling-draft-command-clone',
  template: `
    <a class="btn btn-secondary w-100 position-relative"
      *ngIf="idDraft"
      href="javascript:;"
      [ngClass]="{'class': true}"
      (click)="cloneDraft()">
      <div *ngIf="isInLoading">Loading...</div>
      Clona Bozza
    </a>
  `
})
export class CamundaModellingDraftCommandCloneComponent implements OnInit {

  @Input() idDraft?: number;
  @Output() onCloned: EventEmitter<Draft> = new EventEmitter();
  public isInLoading: boolean = false;

  constructor(
    private draftsService: CamundaModellingDraftsService
  ) { }

  ngOnInit(): void { }

  cloneDraft(): void {
    if (!this.idDraft) { return; }
    this.isInLoading = true;
    console.log("dddd", this.idDraft);
    this.draftsService.CloneDraft(this.idDraft).subscribe({
      next: (result) => {
        this.isInLoading = false;
        this.onCloned.emit(result);
      },
      error: (err) => {
        this.isInLoading = false;
        console.error(err);
      }
    });
  }
}
