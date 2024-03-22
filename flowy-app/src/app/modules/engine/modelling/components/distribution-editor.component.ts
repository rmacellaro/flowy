import { Component, Input, OnInit } from '@angular/core';
import { Distribution } from '../models/distribution.model';
import { EngineModellingDistributionLoadComponent } from './distribution-load.component';
import { Node } from '../models/node.model';
import { Interaction } from '../models/interaction.model';

@Component({
  selector: 'engine-modelling-distribution-editor',
  template: `
    <div class="d-flex h-100 w-100" *ngIf="distribution">
      <div class="border-end p-3 overflow-auto flex-grow-1">

        <!-- Intestazione -->
        <div class="card mb-2 p-2">
          <div class="row align-items-center m-0">
            <div class="col-auto">
              <engine-modelling-distribution-version [distribution]="distribution"></engine-modelling-distribution-version>
            </div>
            <div class="col">
            </div>
            <div class="col-auto">
              <div class="dropdown">
                <a class="btn btn-link p-0"
                  href="javascript:;" role="button"
                  data-bs-toggle="dropdown" aria-expanded="false">
                  <i class="bi bi-three-dots-vertical"></i>
                </a>

                <ul class="dropdown-menu">
                  <li><a class="dropdown-item" href="javascript:;" (click)="editDistribution()">Modifica distribuzione</a></li>
                  <li><a class="dropdown-item" href="javascript:;" (click)="addNode()">Aggiungi nodo alla distribuzione</a></li>
                  <li><hr class="dropdown-divider"></li>
                  <li><a class="dropdown-item" href="javascript:;" (click)="expand(true)">Espandi tutti</a></li>
                  <li><a class="dropdown-item" href="javascript:;" (click)="expand(false)">Comprimi tutti</a></li>
                </ul>
              </div>
            </div>
          </div>
        </div>

        <!-- nodo -->
        <div class="mb-3" *ngFor="let item of distribution?.nodes">
          <div class="border rounded border-primary p-2" [ngClass]="{'bg-selected': node == item}" >
            <div class="row align-items-center m-0">
              <div class="col text-ellipsis">
                <div class="fw-semibold">{{item.title}}</div>
                <!--<div class="fst-italic opacity-50 f-s-08 text-ellipsis">{{item.description}}</div>-->
              </div>
              <div class="col-auto">
                <span class="rounded bg-primary p-1 px-2 text-white f-s-06">{{item.key}}</span>
              </div>
              <div class="col-auto">
                <div class="dropdown">
                  <a class="btn btn-link p-0"
                    href="javascript:;" role="button"
                    data-bs-toggle="dropdown" aria-expanded="false">
                    <i class="bi bi-three-dots-vertical"></i>
                  </a>

                  <ul class="dropdown-menu">
                    <li><a class="dropdown-item" href="javascript:;" (click)="editNode(item)">Modifica nodo</a></li>
                    <li><a class="dropdown-item" href="javascript:;" (click)="addInteraction(item)">Aggiungi interazione al nodo</a></li>
                  </ul>
                </div>
              </div>
              <div class="col-auto">
                <a class="btn btn-link p-0" (click)="item.isExpanded = !item.isExpanded">
                  <i class="bi bi-chevron-down" *ngIf="!item.isExpanded"></i>
                  <i class="bi bi-chevron-up" *ngIf="item.isExpanded"></i>
                </a>
              </div>
            </div>
          </div>

          <div class="ms-3" *ngIf="item.isExpanded">
            <div class="rounded p-2 mt-2"
              [ngClass]="{
                'bg-selected': interaction == inter,
                'bg-body-secondary': interaction != inter
              }"
              *ngFor="let inter of item.interactions">
              <div class="row align-items-center m-0">
                <div class="col-auto opacity-50">
                  {{inter.order}}
                </div>
                <div class="col">
                  <div class="fw-semibold">{{inter.name}}</div>
                  <!--<div class="fst-italic opacity-50 f-s-08">{{inter.description}}</div>-->
                  <div>
                    <span *ngFor="let nex of inter.nexts"
                      class="rounded bg-primary p-1 px-2 text-white f-s-06 opacity-60">
                      <i class="gi f-s-1 me-1">prompt_suggestion</i>
                      <span>{{nex}}</span>
                    </span>
                  </div>
                </div>
                <div class="col-auto">
                  <span class="font-light f-s-08 p-1 fw-light">{{inter.type}}</span>
                </div>
                <div class="col-auto">
                  <div class="dropdown">
                    <a class="btn btn-link p-0"
                      href="javascript:;" role="button"
                      data-bs-toggle="dropdown" aria-expanded="false">
                      <i class="bi bi-three-dots-vertical"></i>
                    </a>

                    <ul class="dropdown-menu">
                      <li><a class="dropdown-item" href="javascript:;" (click)="editInteraction(item, inter)">Modifica interazione</a></li>
                    </ul>
                  </div>
                </div>
              </div>
            </div>
          </div>

        </div>
      </div>
      <div class="flex-grow-0 overflow-auto p-3 w-50">
        {{mode}}
      </div>
    </div>
  `
})
export class EngineModellingDistributionEditorComponent implements OnInit {

  public distribution?: Distribution;
  public node?: Node;
  public interaction?: Interaction;

  public mode: 'NONE' | 'EDIT_DISTRIBUTION' | 'ADD_NODE' | 'EDIT_NODE' | 'EDIT_INTERACTION' | 'ADD_INTERACTION' = 'NONE';

  constructor(
    private distLoad: EngineModellingDistributionLoadComponent
  ) {
    this.setDistribution(this.distLoad.distribution);
    this.distLoad.onLoadedDistribution.subscribe(dis => {
      this.setDistribution(dis);
    });
  }

  ngOnInit(): void {

  }

  setDistribution(dis?: Distribution): void {
    this.distribution = dis;
    if(!this.distribution) { return; }
    if (this.distribution.nodes){
      this.distribution.nodes.forEach(n => {
        n.interactions?.sort((a,b) => {
          if (!b.order || !a.order) { return 0; }
          if (a.order > b.order) { return 1; }
          if (a.order < b.order) { return -1; }
          return 0;
        });
        n.interactions?.forEach(i => {
          var c = i.configurations?.find(c => c.type == 'System.BE' && c.name == 'Nexts');
          if (c && c.value) {
            i.nexts = JSON.parse(c.value);
          }
        });
      });
    }
    console.log('EDITOR', this.distribution);
  }

  editDistribution(): void {
    this.mode = 'EDIT_DISTRIBUTION';
    this.node = undefined;
    this.interaction = undefined;
  }

  addNode(): void {
    if (this.node) { this.node.isExpanded = false; }
    this.node = undefined;
    this.interaction = undefined;
    this.mode = 'ADD_NODE';
  }

  editNode(node: Node): void {
    if (this.node) { this.node.isExpanded = false; }
    this.node = node;
    node.isExpanded = true;
    this.interaction = undefined;
    this.mode= 'EDIT_NODE';
  }

  addInteraction(node: Node): void {
    if (this.node) { this.node.isExpanded = false; }
    this.node = node;
    node.isExpanded = true;
    this.interaction = undefined;
    this.mode = 'ADD_INTERACTION';
  }

  editInteraction(node: Node, interaction: Interaction): void {
    if (this.node) { this.node.isExpanded = false; }
    this.node = node;
    node.isExpanded = true;
    this.interaction = interaction;
    this.mode = 'EDIT_INTERACTION';
  }

  expand(isExpanded: boolean): void {
    if (this.distribution){
      this.distribution.nodes?.forEach(n => {
        n.isExpanded = isExpanded;
      });
    }
  }
}
