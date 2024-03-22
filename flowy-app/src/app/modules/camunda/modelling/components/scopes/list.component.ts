import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

import { CamundaModellingScopesService } from '../../services/scopes.service';
import { Scope } from '../../models/scope.model';
import { Request } from '../../../common/models/request.model';
import { Sort } from '../../../common/models/sort.model';

@Component({
  selector: 'camunda-modelling-scopes-list',
  template: `
    <div class="position-relative">
      <div *ngIf="isInLoading">Loading...</div>

      <div *ngIf="!scopes || !scopes.length">
        Nessuno scope creato per questo Tenant
      </div>

      <div class="card mb-3" *ngIf="scopes && scopes.length">
        <div class="list-group list-group-flush">
          <a class="list-group-item list-group-item-action"
            *ngFor="let item of scopes"
            href="javascript:;"
            (click)="select(item)"
            [ngClass]="{'active': item.id == scope?.id}"
            >
            <div class="row m-0 align-items-center">
              <div class="col-auto">
                <i class="bi bi-diagram-3 f-s-1-6"></i>
              </div>
              <div class="col">
                <div>{{item.name}}</div>
                <div class="fst-italic f-s-09 text-muted">{{item.description}}</div>
              </div>
            </div>
          </a>
        </div>
      </div>
    </div>
  `
})
export class CamundaModellingScopesListComponent implements OnInit {

  @Output() onSelect: EventEmitter<Scope> = new EventEmitter();

  public isInLoading: boolean = false;
  public scopes?: Array<Scope>;
  public scope?: Scope;
  private request: Request;

  constructor(
    private scopesService: CamundaModellingScopesService
  ) {
    this.request = new Request();
    this.request.offset = 0;
    this.request.size = 100;
    this.request.sort = new Sort();
    this.request.sort.column = 'Id';
    this.request.sort.method = 'DESC';
    this.request.queries = [];
  }

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.isInLoading = true;
    this.scopesService.Search(this.request).subscribe({
      next: (result) => {
        console.log('list scopes', result);
        this.scopes = result.items;
        this.isInLoading = false;
      },
      error: (err) => {
        console.error(err);
        this.isInLoading = false;
      }
    });
  }

  select(item: Scope): void {
    this.scope = item;
    this.onSelect.emit(item);
  }
}
