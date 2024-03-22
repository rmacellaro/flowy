import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'camunda-processing-instance-choose-state',
  template: `
    <div class="dropdown w-100">
      <div class="border rounded px-3 py-2 position-relative w-100"
        type="button"
        data-bs-toggle="dropdown"
        aria-expanded="false">
        <div class="row m-0 align-items-center">
          <div class="col">
            <span *ngIf="state">
              <ng-container *ngTemplateOutlet="tmpItem; context: {item:state}"></ng-container>
            </span>
            <span *ngIf="!state" class="text-muted opacity-50 f-s-09">Seleziona stato</span>
          </div>
          <div class="col-auto">
            <i class="bi bi-chevron-down"></i>
          </div>
        </div>
      </div>
      <ul class="dropdown-menu">
        <li *ngFor="let item of states">
          <a class="dropdown-item" href="javascript:;" (click)="select(item)">
            <ng-container *ngTemplateOutlet="tmpItem; context: {item:item}"></ng-container>
          </a>
        </li>
        <li class="dropdown-divider"></li>
        <li>
          <a class="dropdown-item text-muted" href="javascript:;" (click)="select(undefined)">Nessuno</a>
        </li>
      </ul>
    </div>


    <ng-template #tmpItem let-item="item">
      <camunda-processing-instance-state [state]="item"></camunda-processing-instance-state>
    </ng-template>
  `
})
export class CamundaProcessingInstanceChooseStateComponent implements OnInit {

  @Output() OnChoose: EventEmitter<string> = new EventEmitter();
  public states: Array<string> = ['ACTIVE', 'COMPLETED', 'CANCELED'];
  @Output()
  public selected?: string;
  @Input()
  public state?: string;

  constructor() { }

  ngOnInit(): void {
    if(this.state) {
      var sel = this.states.find(s => s === this.state);
      if (sel) {
        this.select(sel);
      }
    }
  }

  select(item?: string) : void {
    this.selected = item;
    this.OnChoose.emit(item);
  }
}
