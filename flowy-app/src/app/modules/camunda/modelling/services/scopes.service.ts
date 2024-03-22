import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { CamundaBaseService } from '../../common/services/base.service';
import { Scope } from '../models/scope.model';
import { Result } from '../../common/models/result.model';
import { Request } from '../../common/models/request.model';


@Injectable({
  providedIn: 'root'
})
export class CamundaModellingScopesService extends CamundaBaseService {

  public Search(request: Request): Observable<Result<Scope>> {
    return this.http.post(this.baseApi + 'Scopes/Search', request);
  }

  public GetScopeById(id: number): Observable<Scope> {
    var params: HttpParams = new HttpParams();
    params = params.set('id', id.toString());
    return this.http.get(this.baseApi + 'Scopes/GetScopeById',{
      params
    });
  }
}
