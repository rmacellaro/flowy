import { Injectable } from '@angular/core';
import { HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Node } from '../../modelling/models/node.model';
import { Instance } from '../models/instance.model';
import { EngineBaseService } from '../../common/services/base.service';

@Injectable({ providedIn: 'root' })
export class EngineProcessingService extends EngineBaseService {

  public GetStartNodeByIdDistribution(idDistribution: number): Observable<Node> {
    var params: HttpParams = new HttpParams();
    params = params.set('idDistribution', idDistribution.toString());
    return this.http.get<Node>(this.baseApi + 'Processing/GetStartNodeByIdDistribution',{
      params
    });
  }

  /*public GetInteractionWithConfigurationsById(idInteraction: number): Observable<Interaction> {
    var params: HttpParams = new HttpParams();
    params = params.set('idInteraction', idInteraction.toString());
    return this.http.get<Interaction>(this.baseApi + 'Processing/GetInteractionWithConfigurationsById',{
      params
    });
  }*/

  public GetInstanceByIdWire(idWire: number): Observable<Instance> {
    var params: HttpParams = new HttpParams();
    params = params.set('idWire', idWire.toString());
    return this.http.get<Instance>(this.baseApi + 'Processing/GetInstanceByIdWire',{
      params
    });
  }

  public GetInstancesByIdProcess(idProcess: number): Observable<Array<Instance>> {
    var params: HttpParams = new HttpParams();
    params = params.set('idProcess', idProcess.toString());
    return this.http.get<Array<Instance>>(this.baseApi + 'Processing/GetInstancesByIdProcess',{
      params
    });
  }

  public Start(data: any): Observable<Instance> {
    return this.http.post<Instance>(this.baseApi + 'Processing/Start', data);
  }

  public Continue(data: any): Observable<Instance> {
    return this.http.post<Instance>(this.baseApi + 'Processing/Continue', data);
  }
}
