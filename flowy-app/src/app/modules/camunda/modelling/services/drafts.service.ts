import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { CamundaBaseService } from '../../common/services/base.service';
import { Draft } from '../models/draft.model';
import { DraftTrack } from '../models/draft-track.model';

@Injectable({ providedIn: 'root' })
export class CamundaModellingDraftsService extends CamundaBaseService {

  public GetDraftsByIdScope(idScope: number): Observable<Array<Draft>> {
    var params: HttpParams = new HttpParams();
    params = params.set('idScope', idScope.toString());
    return this.http.get<Array<Draft>>(this.baseApi + 'Drafts/GetDraftsByIdScope',{
      params
    });
  }

  public GetDraftById(idDraft: number): Observable<Draft> {
    var params: HttpParams = new HttpParams();
    params = params.set('idDraft', idDraft.toString());
    return this.http.get<Draft>(this.baseApi + 'Drafts/GetDraftById',{
      params
    });
  }

  public GetDraftTracksByIdDraft(idDraft: number): Observable<Array<DraftTrack>> {
    var params: HttpParams = new HttpParams();
    params = params.set('idDraft', idDraft.toString());
    return this.http.get<Array<DraftTrack>>(this.baseApi+ 'Drafts/GetDraftTracksByIdDraft',{
      params
    });
  }

  public UpdateDeployment(draft: Draft): Observable<any> {
    return this.http.post(
      this.baseApi + 'Drafts/UpdateDraftSchema',
      draft
    );
  }

  public UpdateDraftSchema(draft: Draft): Observable<any> {
    return this.http.post(
      this.baseApi + 'Drafts/UpdateDraftSchema',
      draft
    );
  }

  public UpdateDraftInfo(draft: Draft): Observable<any> {
    return this.http.post(
      this.baseApi + 'Drafts/UpdateDraftInfo',
      draft
    );
  }

  public CloneDraft(idDraft: number): Observable<Draft> {
    var params: HttpParams = new HttpParams();
    params = params.set('idDraft', idDraft.toString());
    return this.http.put<Draft>(this.baseApi+ 'Drafts/CloneDraft',{},{
      params
    });
  }

  public NewDraft(draft: Draft): Observable<Draft> {
    return this.http.post(
      this.baseApi + 'Drafts/NewDraft',
      draft
    );
  }

  public DeployDraft(idDraft: number): Observable<any> {
    var params: HttpParams = new HttpParams();
    params = params.set('idDraft', idDraft.toString());
    return this.http.put<any>(this.baseApi + 'Drafts/DeployDraft',{},{
      params
    });
  }

}
