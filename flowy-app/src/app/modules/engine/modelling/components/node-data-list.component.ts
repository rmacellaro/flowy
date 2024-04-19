import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { EngineModellingService } from '../services/modelling.service';
import { NodeDataType } from '../models/node-data-type.model';
import { DataMap } from '../../datas/models/data-map.model';
import { NodeData } from '../models/node-data.model';
import { Node } from '../models/node.model';

@Component({
  selector: 'modelling-node-data-list',
  template: `
    <div class="position-relative">
      <ui-spinner [isInLoading]="isInLoading"></ui-spinner>

      <data-maps-list
        *ngIf="nodeDataMaps"
        [dataMaps]="nodeDataMaps"
        [editMode]="editMode"
        (onUpdateDataMap)="onUpdateDataMap($event)"
      ></data-maps-list>
    </div>
  `
})
export class ModellingNodeDataListComponent implements OnInit {

  @Input() set node(value: Node){
    //console.log('SET NODE DATA TYPES LIST', value);
    this.currentNode = value;
    this.mapping();
  };
  @Input() public editMode: boolean = false;

  @Output() public onSaved: EventEmitter<boolean> = new EventEmitter();

  public isInLoading: boolean = false;
  public nodeDataTypes?: Array<NodeDataType>;
  public nodeDataMaps?: Array<DataMap>;
  public currentNode?: Node;

  constructor(
    private modellingService: EngineModellingService
  ) { }

  ngOnInit(): void {
    this.loadNodeDataTypes();
  }

  loadNodeDataTypes(): void {
    this.isInLoading = true;
    this.modellingService.GetNodeDataTypes().subscribe({
      next: (ndt) => {
        this.isInLoading = false;
        this.nodeDataTypes = ndt;
        this.mapping();
      },
      error: (err) => {
        this.isInLoading = false;
        console.error(err);
      }
    });
  }

  /**
   * fa il mapping del datatype con data
   */
  mapping(): void {
    //console.log('NNNNNNNNNNDDDDDD', this.currentNode?.datas);
    if (!this.nodeDataTypes) { return; }
    this.currentNode?.datas ?? [];
    this.nodeDataMaps = [];
    this.nodeDataTypes.forEach(dataType => {
      this.nodeDataMaps?.push({
        dataType : dataType,
        data : this.currentNode?.datas?.find(d => d.name == dataType.name),
        state : 'NONE'
      });
    });
  }

  /**
   * se presente nei dati del nodo, sostituisco il dato
   * se non presente nel dati del nodo il nuovo dato lo inserisco
   */
  marge(nd: NodeData): void {
    if (!this.currentNode) { return; }
    var isMarge: boolean = false;
    this.currentNode.datas = this.currentNode.datas?.map(cnd => {
      if (cnd.id === nd.id) {
        isMarge = true;
        return nd;
      }
      return cnd;
    });
    if (!isMarge) { this.currentNode.datas?.push(nd);}
  }

  onUpdateDataMap(dataMap: DataMap): void {
    if (!this.editMode) { return; }
    if (!this.currentNode || !this.currentNode.id){ return; }
    if (!dataMap.data) { return; }
    var data = dataMap.data as NodeData;
    if (dataMap.state == 'NEW') {
      data.idNode = this.currentNode.id;
      data.idNodeDataType = dataMap.dataType?.id;
    }

    this.isInLoading = true;
    console.log('To save', data);
    this.modellingService.SaveNodeData(data).subscribe({
      next: (nd) => {
        dataMap.data = nd;
        dataMap.state = 'SAVED';
        this.marge(nd);
        this.isInLoading = false;
        this.onSaved.emit();
        console.log('saved', nd);
      },
      error: (err) => {
        console.error(err);
        this.isInLoading = false;
      }
    });
  }

}
