import { AfterViewInit, Component, ElementRef, EventEmitter, Input, OnInit, Output, Type, ViewChild, ViewContainerRef } from '@angular/core';
import { Data } from '../models/data.model';
import { Modal } from 'bootstrap';

import { DataTypeBaseComponent } from './data-type-base.component';
import { DataType } from '../models/data-type.model';
import { DataMap } from '../models/data-map.model';
import { DataTypeIconEditComponent } from './data-type-icon.component';
import { DataTypeTextEditComponent } from './data-type-text.component';

@Component({
  selector: 'data-maps-list',
  template: `
    <div class="list-group" *ngIf="dataMaps">
      <ng-container *ngFor="let item of dataMaps">
        <div class="list-group-item" *ngIf="item.dataType">
          <div class="row align-items-center">
            <div class="col">
              <div class="f-s-08 fst-italic opacity-60">
                {{item.dataType.name}}
              </div>
            </div>
          </div>

          <div class="row align-items-center">
            <div class="col text-ellipsis">
              <ng-container [ngSwitch]="item.dataType.type">
                <data-type-icon-show *ngSwitchCase="'Icon'" [dataMap]="item"></data-type-icon-show>
                <data-type-text-show *ngSwitchCase="'Text'" [dataMap]="item"></data-type-text-show>
                <div *ngSwitchDefault>...</div>
              </ng-container>
            </div>
            <div class="col-auto">
              <a class="btn btn-sm btn-secondary" href="javascript:;" (click)="edit(item)" *ngIf="editMode">
                <i class="bi bi-pencil"></i>
              </a>
            </div>
          </div>
        </div>
      </ng-container>
    </div>


    <div class="modal" tabindex="-1" #modalEdit>
      <div class="modal-dialog modal-dialog-scrollable {{modalSize}}">
        <div class="modal-content">
          <div class="modal-header border-0">
            <h5 class="modal-title">{{dataMap?.dataType?.name}}</h5>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>
          <div class="modal-body">
            <ng-container #container></ng-container>
          </div>
        </div>
      </div>
    </div>
  `
})
export class DataMapsListComponent implements OnInit, AfterViewInit {

  @ViewChild('modalEdit') modalEdit?: ElementRef;
  @ViewChild('container', {read: ViewContainerRef}) container?: ViewContainerRef

  //@Input() datas?: Array<Data>;
  //@Input() dataTypes?: Array<DataType>;
  @Input() editMode?: boolean = false;
  @Input() dataMaps?: Array<DataMap>;

  @Output() onUpdateDataMap: EventEmitter<DataMap> = new EventEmitter();

  public dataMap?: DataMap;
  public modalSize?: string;
  public modal?: Modal;

  constructor() { }

  ngAfterViewInit(): void {
    this.modal = new Modal(this.modalEdit?.nativeElement, {
      backdrop: 'static',
      focus: true,
      keyboard: true
    });
    this.modalEdit?.nativeElement.addEventListener('hidden.bs.modal', (event: any) => {
      this.dataMap = undefined;
      this.container?.clear();
    });
  }

  ngOnInit(): void {
    //console.log('eccomi', this.currentDataMaps);
  }

  edit(dataMap: DataMap): void {
    this.container?.clear();
    this.modal?.show();
    this.dataMap = dataMap;
    if (!this.dataMap.dataType) { return; }
    var comp: Type<DataTypeBaseComponent> = DataTypeBaseComponent;
    switch(this.dataMap.dataType.type){
      case 'Icon' : comp = DataTypeIconEditComponent; break;
      case 'Text' : comp = DataTypeTextEditComponent; break;
    }
    const compRef = this.container?.createComponent(comp);
    if (!compRef) { return; }
    compRef.instance.dataMap = this.dataMap;
    compRef.instance.OnSave = (value?: string) => {
      //console.log('save', this.dataMap);
      if (!this.dataMap || !this.dataMap.dataType){ return;}

      if(this.dataMap.data && this.dataMap.data?.value != value) {
        this.dataMap.data.value = value;
        this.dataMap.state = 'EDITED';
      } else {
        this.dataMap.data = {
          name: dataMap.dataType?.name,
          value: value
        };
        this.dataMap.state = 'NEW';
      }
      this.onUpdateDataMap.emit(this.dataMap);
      this.modal?.hide();
    };
    this.modalSize = 'modal-' + compRef.instance.size;
  }
}
