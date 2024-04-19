import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Node } from '../models/node.model';

@Component({
  selector: 'engine-modelling-links-list',
  template: `
    <div class="row m-0 mb-2 align-items-center">
      <div class="col">
        Links
      </div>
      <div class="col-auto">
        <a class="btn btn-link" href="javascript:;"><i class="bi bi-plus"></i></a>
      </div>
    </div>
   <div class="bg-primary-subtle rounded p-2 mb-2" *ngFor="let item of node?.outputLinks">
      <div class="row m-0 align-items-center">
        <div class="col text-primary">
          {{item.key}}
        </div>
        <div class="col-auto" *ngIf="item.targetNode">
          <a href="javascript:;"
            class="btn btn-secondary f-s-08" (click)="goToNode(item.targetNode.id)">
            {{item.targetNode.key}}
          </a>
        </div>
      </div>
    </div>
  `
})
export class EngineModellingListsListComponent implements OnInit {

  @Input() node?: Node;
  @Output() onGoToNode: EventEmitter<number> = new EventEmitter();

  constructor() { }

  ngOnInit(): void { }

  goToNode(id?: number): void {
    this.onGoToNode.emit(id);
  }
}
