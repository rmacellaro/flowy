import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Distribution } from '../models/distribution.model';
import { HttpParams } from '@angular/common/http';
import { EngineBaseService } from '../../common/base.service';

@Injectable({
  providedIn: 'root'
})
export class EngineModellingDistributionsService extends EngineBaseService {

  public GetDistributionById(idDistribution: number): Observable<Distribution> {
    var params: HttpParams = new HttpParams();
    params = params.set('idDistribution', idDistribution.toString());
    return this.http.get<Distribution>(this.baseApi + 'Modelling/GetDistributionById',{
      params
    });
  }
}
