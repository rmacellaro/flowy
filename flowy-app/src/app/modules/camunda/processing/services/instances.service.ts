import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { CamundaBaseService } from '../../common/services/base.service';
import { Result } from '../../common/models/result.model';
import { Instance } from '../models/instance.model';
import { InstanceData } from '../models/instance-data.model';
import { InstanceTask } from '../models/instance-task.model';
import { InstanceTrack } from '../models/instance-track.model';
import { Request } from '../../common/models/request.model';

@Injectable({
  providedIn: 'root'
})
export class CamundaInstancesService extends CamundaBaseService {

  public GetInstancesByIdProcess(request: Request): Observable<Result<Instance>> {
    return this.http.post(this.baseApi + 'Instances/GetInstancesByIdProcess', request);
  }

  public GetInstanceById(id: number): Observable<Instance> {
    var params = new HttpParams();
    params = params.set("id", id.toString());
    return this.http.get<Instance>(this.baseApi + 'Instances/GetInstanceById', {
      params
    });
  }

  public GetInstanceDatasByIdInstance(idInstance: number): Observable<Array<InstanceData>> {
    var params = new HttpParams();
    params = params.set("idInstance", idInstance.toString());
    return this.http.get<Array<InstanceData>>(this.baseApi + 'Instances/GetInstanceDatasByIdInstance', {
      params
    });
  }

  public GetInstanceTasksByIdInstance(idInstance: number): Observable<Array<InstanceTask>> {
    var params = new HttpParams();
    params = params.set("idInstance", idInstance.toString());
    return this.http.get<Array<InstanceTask>>(this.baseApi + 'Instances/GetInstanceTasksByIdInstance', {
      params
    });
  }

  public GetInstanceTracksByIdInstance(idInstance: number): Observable<Array<InstanceTrack>> {
    var params = new HttpParams();
    params = params.set("idInstance", idInstance.toString());
    return this.http.get<Array<InstanceTrack>>(this.baseApi + 'Instances/GetInstanceTracksByIdInstance', {
      params
    });
  }

}
