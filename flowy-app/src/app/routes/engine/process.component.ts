import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-engine-process',
  template: `
    <engine-modelling-process-load #load [idProcess]="idProcess">
      <div class="d-flex h-100 flex-column flex-md-row" *ngIf="load.process">
        <layout-sidebar>
          <div class="m-3 border-1 border rounded p-2">
            <div class="row align-items-center m-0">
              <div class="col-auto">
                <a [routerLink]="['..']">
                  <i class="bi bi-chevron-left"></i>
                </a>
              </div>
              <div class="col">
                {{load.process.name}}
              </div>
            </div>
          </div>

          <nav>
            <ol class="layout-menu">
              <li>
                <span class="menu-category">Lavorazione</span>
                <a [routerLinkActive]="'active'" [routerLink]="['instances']">
                  <span class="menu-icon">
                    <i class="bi bi-list-task"></i>
                  </span>
                  <span>Istanze Aperte</span>
                </a>
                <a [routerLinkActive]="'active'" [routerLink]="['instance',0]">
                  <span class="menu-icon">
                    <i class="bi bi-plus-lg"></i>
                  </span>
                  <span>Nuova istanza</span>
                </a>
              </li>
              <li>
                <span class="menu-category">Modellazione</span>
                <a [routerLinkActive]="'active'" [routerLink]="['properties']">
                  <span class="menu-icon">
                    <i class="bi bi-list-ol"></i>
                  </span>
                  <span>Proprietà</span>
                </a>
                <a [routerLinkActive]="'active'" href="javascript:;">
                  <span class="menu-icon">
                    <i class="bi bi-repeat"></i>
                  </span>
                  <span>Versioni</span>
                </a>
                <div class="active">
                  <ol>
                    <li *ngFor="let item of load.process.distributions">
                      <a [routerLinkActive]="'active'"
                        [routerLink]="['distribution', item.id]">
                        <engine-modelling-distribution-version [distribution]="item"></engine-modelling-distribution-version>
                      </a>
                    </li>
                    <!--<li>
                      <a [routerLinkActive]="'router-link-active'"
                        [routerLink]="['/','system', 'workspace']">
                        Istanze di lavorazione
                      </a>
                    </li>
                    <li>
                      <a [routerLinkActive]="'router-link-active'"
                        [routerLink]="['/','system', 'locations']">
                        Nuova istanza
                      </a>
                    </li>-->
                  </ol>
                </div>
              </li>
              <li>
                <span class="menu-category">Storico</span>
                <a [routerLinkActive]="'active-menuitem'" [routerLink]="['historical']">
                  <span class="menu-icon">
                    <i class="bi bi-clock-history"></i>
                  </span>
                  <span>Storico chiuse</span>
                </a>
              </li>
            </ol>
          </nav>
        </layout-sidebar>
        <div class="overflow-auto flex-grow-1">
          <router-outlet />
        </div>
      </div>
    </engine-modelling-process-load>
  `
})
export class AppEngineProcessComponent implements OnInit {

  public isInLoading: boolean = false;
  public idProcess?: number;

  constructor(private route: ActivatedRoute) {
    const id = this.route.snapshot.paramMap.get('id');
    if (!id) { return;}
    this.idProcess = parseInt(id);
  }

  ngOnInit(): void { }
}
