import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CamundaModellingDraftsService } from '../../services/drafts.service';

@Component({
  selector: 'camunda-modelling-draft-command-deploy',
  template: `
    <a class="btn btn-secondary w-100 position-relative"
      *ngIf="idDraft"
      href="javascript:;"
      [ngClass]="{'class': true}"
      (click)="deployDraft()">
      <div *ngIf="isInLoading">Loading...</div>
      Distribuisci Bozza
    </a>
  `
})
export class CamundaModellingDeployCommandCloneComponent implements OnInit {

  @Input() idDraft?: number;
  @Output() onDeployed: EventEmitter<any> = new EventEmitter();
  public isInLoading: boolean = false;

  constructor(
    private draftsService: CamundaModellingDraftsService
  ) { }

  ngOnInit(): void { }

  deployDraft(): void {
    if (!this.idDraft) { return; }
    this.isInLoading = true;
    this.draftsService.DeployDraft(this.idDraft).subscribe({
      next: (result) => {
        this.isInLoading = false;
        this.onDeployed.emit(result);
        if (!result) { alert("Nessun processo distribuito");}
        console.log('rrr', result);
      },
      error: (err) => {
        this.isInLoading = false;
        console.error(err);
      }
    });
  }
}
