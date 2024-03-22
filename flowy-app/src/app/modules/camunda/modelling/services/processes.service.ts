import { HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CamundaBaseService } from '../../common/services/base.service';
import { Process } from '../models/process.model';


@Injectable({
  providedIn: 'root'
})
export class CamundaModellingProcessesService extends CamundaBaseService {

  public GetProcessesByIdScope(idScope: number): Observable<Array<Process>> {
    var params: HttpParams = new HttpParams();
    params = params.set('idScope', idScope.toString());
    return this.http.get<Array<Process>>(this.baseApi + 'Processes/GetProcessesByIdScope',{
      params
    });
  }

  /*public GetStatisticsByIdProcess(idProcess: number): Observable<Array<FlowNodeStatistics>> {
    var params: HttpParams = new HttpParams();
    params = params.set('idProcess', idProcess.toString());
    return this.http.get<Array<FlowNodeStatistics>>(environment.api_flowy + 'Processes/GetStatisticsByIdProcess',{
      params
    });
  }*/

  public GetSchemaByIdProcess(idProcess: number): Observable<string> {
    var params: HttpParams = new HttpParams();
    params = params.set('idProcess', idProcess.toString());
    var headers = new HttpHeaders();
    headers.append('Accept', 'text/plain');
    headers.append('Content-Type', 'text/plain');
    return this.http.get(this.baseApi + 'Processes/GetSchemaByIdProcess',{
      params,
      responseType: 'text'
    });
  }
}
