import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { CamundaBaseService } from '../../common/services/base.service';
import { Instance } from '../models/instance.model';
import { InstanceTask } from '../models/instance-task.model';
import { Interaction } from '../../modelling/models/interaction.model';

@Injectable({
  providedIn: 'root'
})
export class CamundaProcessingService extends CamundaBaseService {

  public Start(idProcess: number): Observable<Instance> {
    var params: HttpParams = new HttpParams();
    params = params.set('idProcess', idProcess.toString());
    return this.http.post(this.baseApi + 'Processing/Start', {

    }, { params});
  }

  public GetInstanceTaskById(idTask: number): Observable<InstanceTask> {
    var params: HttpParams = new HttpParams();
    params = params.set('idTask', idTask.toString());
    return this.http.get(this.baseApi + 'Processing/GetInstanceTaskById', { params });
  }

  public GetInteractionByIdTask(idTask: number): Observable<Interaction> {
    var params: HttpParams = new HttpParams();
    params = params.set('idTask', idTask.toString());
    return this.http.get(this.baseApi + 'Processing/GetInteractionByIdTask', { params });
  }
}
