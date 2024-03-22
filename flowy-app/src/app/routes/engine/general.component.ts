import { Component, OnInit } from '@angular/core';
import { EngineModellingProcessLoadComponent } from '../../modules/engine/modelling/components/process-load.component';
import { Process } from '../../modules/engine/modelling/models/process.model';

@Component({
  selector: 'app-engine-general',
  template: `
    <div class="container py-3" *ngIf="process">

      <layout-title class="mb-3">
        <span>{{process.name}}</span>
        <span subtitle>{{process.description}}</span>
      </layout-title>

      <div class="row">
        <div class="col mb-3">
          <h4>Dettaglio processo</h4>
          <table class="table table-striped table-bordered">
            <tbody>
              <tr>
                <td class="fst-italic opacity-50">identificativo : </td>
                <td>{{process.id}}</td>
              </tr>
              <tr>
                <td class="fst-italic opacity-50">chiave : </td>
                <td>{{process.key}}</td>
              </tr>
              <tr>
                <td class="fst-italic opacity-50">nome : </td>
                <td>{{process.name}}</td>
              </tr>
              <tr>
                <td class="fst-italic opacity-50">descrizione : </td>
                <td>{{process.description}}</td>
              </tr>
            </tbody>
          </table>
        </div>
        <div class="col mb-3">
          <h4>Distribuzioni</h4>
          <table class="table table-striped table-bordered">
            <tbody>
              <tr *ngFor="let item of process.distributions" class="align-middle">
                <td class="fst-italic opacity-50">{{item.id}}</td>
                <td><engine-modelling-distribution-version [distribution]="item"></engine-modelling-distribution-version></td>
                <td>{{item.createdDateTime | date}}</td>
                <td><a class="btn btn-default btn-sm" [routerLink]="['..','distribution', item.id]">dettaglio</a></td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>
  `
})
export class AppEngineGeneralComponent implements OnInit {

  public process?: Process;

  constructor(
    private pc: EngineModellingProcessLoadComponent
  ) {
    this.process = this.pc.process;
  }

  ngOnInit(): void { }
}
