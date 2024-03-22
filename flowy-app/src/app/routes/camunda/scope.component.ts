import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { AppScopeHelper } from './scope.helper';
import { CamundaModellingScopesService } from '../../modules/camunda/modelling/services/scopes.service';

@Component({
  selector: 'app-scope',
  template: `
  <div *ngIf="isInLoading">Loading ...</div>
  <div class="d-flex h-100 flex-column flex-md-row" *ngIf="helper.scope">
    <layout-sidebar>
      <div layout-sidebar>
        <div class="m-3 border-1 border rounded p-2">
          <div class="row align-items-center m-0">
            <div class="col-auto">
              <a [routerLink]="['..']">
                <i class="bi bi-chevron-left"></i>
              </a>
            </div>
            <div class="col">
              {{helper.scope.name}}
            </div>
          </div>
        </div>

        <nav>
          <ol class="layout-menu">
            <li>
              <a [routerLink]="['dashboard']" [routerLinkActive]="'active'">
                <span class="menu-icon">
                  <i class="bi bi-house me-2"></i>
                </span>
                <span class="">Dashboard</span>
              </a>
            </li>

            <li>
              <span class="menu-category">Lavorazione</span>

              <a [routerLink]="['instances']" [routerLinkActive]="'active'">
                <span class="menu-icon">
                  <i class="bi bi-list-columns me-2"></i>
                </span>
                <span class="">Istanze</span>
              </a>
              <a [routerLink]="['working']" [routerLinkActive]="'active'">
                <span class="menu-icon">
                  <i class="bi bi-list-task me-2"></i>
                </span>
                <span class="">Tasks</span>
              </a>
              <a [routerLink]="['start']" [routerLinkActive]="'active'">
                <span class="menu-icon">
                  <i class="bi bi-play me-2"></i>
                </span>
                <span class="">Start</span>
              </a>
            </li>

            <li>
              <span class="menu-category">Modellazione</span>

              <a [routerLink]="['drafts']" [routerLinkActive]="'active'">
                <span class="menu-icon">
                  <i class="bi bi-bezier me-2"></i>
                </span>
                <span class="">Bozze</span>
              </a>
              <a [routerLink]="['processes']" [routerLinkActive]="'active'">
                <span class="menu-icon">
                  <i class="bi bi-diagram-3 me-2"></i>
                </span>
                <span class="">Processi</span>
              </a>
              <a [routerLink]="['interactions']" [routerLinkActive]="'active'">
                <span class="menu-icon">
                  <i class="bi bi-window me-2"></i>
                </span>
                <span class="">Interazioni</span>
              </a>
            </li>

          </ol>
        </nav>
      </div>
      </layout-sidebar>
      <div class="overflow-auto flex-grow-1">
        <router-outlet />
      </div>
    </div>
  `
})
export class AppCamundaScopeComponent implements OnInit {

  public isInLoading: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    public helper: AppScopeHelper,
    private scopesService: CamundaModellingScopesService
  ) {
    this.route.params.subscribe(params => {
      var identifier = params['id'];
      if (!identifier) { return; }
      this.loadScope(parseInt(identifier));
    });
  }

  ngOnInit(): void { }

  loadScope(id: number): void {
    if (!id) { return; }
    console.log('.........', id);
    this.isInLoading = true;
    this.scopesService.GetScopeById(id).subscribe({
      next: (scope) => {
        this.isInLoading = false;
        console.log('scope', scope);
        if (scope) {
          this.helper.setScope(scope);
        } else {
          this.router.navigate(['/']);
        }
      },
      error: (err) => {
        console.error(err);
        this.isInLoading = false;
      }
    });
  }
}
