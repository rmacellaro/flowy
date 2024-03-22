import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { CamundaBaseService } from '../../common/services/base.service';
import { Interaction } from '../models/interaction.model';
import { InteractionTrack } from '../models/interaction-track.model';

@Injectable({ providedIn: 'root' })
export class CamundaModellingInteractionsService extends CamundaBaseService {

  public GetInteractionsByIdScope(idScope: number): Observable<Array<Interaction>> {
    var params: HttpParams = new HttpParams();
    params = params.set('idScope', idScope.toString());
    return this.http.get<Array<Interaction>>(this.baseApi + 'Interactions/GetInteractionsByIdScope',{
      params
    });
  }

  public GetInteractionById(id: number): Observable<Interaction> {
    var params: HttpParams = new HttpParams();
    params = params.set('id', id.toString());
    return this.http.get<Interaction>(this.baseApi + 'Interactions/GetInteractionById',{
      params
    });
  }

  public GetInteractionTracksByIdInteraction(idInteraction: number): Observable<Array<InteractionTrack>> {
    var params: HttpParams = new HttpParams();
    params = params.set('idInteraction', idInteraction.toString());
    return this.http.get<Array<InteractionTrack>>(this.baseApi + 'Interactions/GetInteractionTracksByIdInteraction',{
      params
    });
  }

  public UpdateInteractionData(interaction: Interaction): Observable<any> {
    return this.http.post(
      this.baseApi + 'Interactions/UpdateInteractionData',
      interaction
    );
  }

  public UpdateInteractionInfo(interaction: Interaction): Observable<any> {
    return this.http.post(
      this.baseApi + 'Interactions/UpdateInteractionInfo',
      interaction
    );
  }

  public CloneInteraction(idInteraction: number): Observable<Interaction> {
    var params: HttpParams = new HttpParams();
    params = params.set('idInteraction', idInteraction.toString());
    return this.http.put<Interaction>(this.baseApi + 'Interactions/CloneInteraction',{},{
      params
    });
  }

  public NewInteraction(interaction: Interaction): Observable<Interaction> {
    return this.http.post(
      this.baseApi + 'Interactions/NewInteraction',
      interaction
    );
  }
}
