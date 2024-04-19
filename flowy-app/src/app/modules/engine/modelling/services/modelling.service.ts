import { Injectable } from '@angular/core';
import { HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Process } from '../models/process.model';
import { EngineBaseService } from '../../common/services/base.service';
import { Distribution } from '../models/distribution.model';
import { NodeData } from '../models/node-data.model';
import { NodeDataType } from '../models/node-data-type.model';
import { ActivityDefinition } from '../../activities/models/activity-definition.model';

@Injectable({ providedIn: 'root' })
export class EngineModellingService extends EngineBaseService {

  public GetProcesses(): Observable<Array<Process>> {
    return this.http.get<Array<Process>>(this.baseApi + 'Modelling/GetProcesses');
  }

  public GetProcessById(id: number): Observable<Process> {
    var params: HttpParams = new HttpParams();
    params = params.set('id', id.toString());
    return this.http.get<Process>(this.baseApi + 'Modelling/GetProcessById',{
      params
    });
  }

  public GetDistributionById(idDistribution: number): Observable<Distribution> {
    var params: HttpParams = new HttpParams();
    params = params.set('idDistribution', idDistribution.toString());
    return this.http.get<Distribution>(this.baseApi + 'Modelling/GetDistributionById',{
      params
    });
  }

  private bufferNodeDataTypes?: Array<NodeDataType>;

  public GetNodeDataTypes(): Observable<Array<NodeDataType>> {
    return new Observable(observe => {
      if (this.bufferNodeDataTypes) {
        observe.next(this.bufferNodeDataTypes);
        observe.complete();
        return;
      }
      this.http.get<Array<NodeDataType>>(this.baseApi + 'Modelling/GetNodeDataTypes').subscribe({
        next: (result) => {
          this.bufferNodeDataTypes = result;
          observe.next(this.bufferNodeDataTypes);
          observe.complete();
        },
        error: observe.error
      });
    });
  }

  public SaveNodeDatas(nodeDatas: Array<NodeData>): Observable<Array<NodeData>> {
    return this.http.post<Array<NodeData>>(this.baseApi + 'Modelling/SaveNodeDatas', nodeDatas);
  }

  public SaveNodeData(nodeData: NodeData): Observable<NodeData> {
    return this.http.post<NodeData>(this.baseApi + 'Modelling/SaveNodeData', nodeData);
  }

}
