import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-main-home',
  template: `
    <div class="p-5 container">
      <div class="row mb-3 align-items-center">
        <div class="col-auto">
          <img src="/assets/img/logo.svg" width="100ß" />
        </div>
        <div class="col">
          <h1 class="m-0">Flowy</h1>
          <div class="text-muted opacity-50">Business Process Management</div>
        </div>
      </div>
      <div class="row row-cols-md-3">
        <div class="col">
          <div class="card shadow-sm">
            <div class="card-body">
              <h3 class="card-title">
                <i class="bi bi-diagram-3 me-3"></i>
                <span>Motore Flowy</span>
              </h3>
              <div class="card-subtitle text-muted">Gestione del motore di workflow interno, basato su logica di lavorazione a stati per istanza di flusso di processo.</div>
            </div>
            <div class="card-footer text-end">
              <a class="btn btn-primary" [routerLink]="['/','engine']">Gestisci</a>
            </div>
          </div>
        </div>
        <div class="col">
          <div class="card shadow-sm h-100">
            <div class="card-body">
              <h3 class="card-title">
                <i class="bi bi-c-square me-3"></i>
                <span>Motore Camunda</span>
              </h3>
              <div class="card-subtitle text-muted">Gestisci flussi di lavorazione con il motore di Workflow Camunda, basato sullo schema BPMN</div>
            </div>
            <div class="card-footer text-end">
              <a class="btn btn-primary" [routerLink]="['/','camunda']">Gestisci</a>
            </div>
          </div>
        </div>
      </div>
    </div>
  `
})
export class AppMainHomeComponent implements OnInit {


  constructor() { }

  ngOnInit(): void { }
}
