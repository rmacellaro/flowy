import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { Process } from '../models/process.model';
import { EngineBaseService } from '../../common/base.service';
import { HttpParams } from '@angular/common/http';

@Injectable({ providedIn: 'root' })
export class EngineModellingProcessesService extends EngineBaseService {

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
}
