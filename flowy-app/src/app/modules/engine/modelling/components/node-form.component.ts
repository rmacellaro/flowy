import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Node } from '../models/node.model';

@Component({
  selector: 'engine-modelling-node-form',
  template: `
    <div class="position-relative" *ngIf="currentNode">
      <ui-spinner [isInLoading]="isInLoading"></ui-spinner>

      <div class="mb-3">
        <div class="row align-items-center">
          <div class="col">
            <span class="fw-semibold me-2">Node Id:</span>
            <span class="fw-light fst-italic f-s-09">
              <span *ngIf="!currentNode.id">nuovo</span>
              <span *ngIf="currentNode.id">{{currentNode.id}}</span>
            </span>
          </div>
          <div class="col-auto">
            <div class="btn-group" role="group" aria-label="Basic example" title="Elimina nodo">
              <a class="btn btn-secondary"><i class="bi bi-trash"></i></a>
            </div>
          </div>
        </div>
      </div>

      <form>
        <div class="form-floating mb-3">
          <input type="text" class="form-control" id="key" placeholder="unique key">
          <label for="key">Key</label>
        </div>
      </form>

      <div class="mb-3" *ngIf="currentNode.id">
        <modelling-node-data-list [node]="currentNode" [editMode]="true" (onSaved)="onUpdated.emit()">
        </modelling-node-data-list>
      </div>
    </div>
  `
})
export class EngineModellingNodeFormComponent implements OnInit {

  @Input() set node(value: Node){
    this.currentNode = value;
  }

  @Output() onUpdated: EventEmitter<boolean> = new EventEmitter();

  public currentNode?: Node;
  public isInLoading: boolean = false;

  constructor() { }

  ngOnInit(): void {  }

}
