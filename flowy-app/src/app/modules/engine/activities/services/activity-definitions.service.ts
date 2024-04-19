import { Injectable } from '@angular/core';
import { EngineBaseService } from '../../common/services/base.service';
import { ActivityDefinition } from '../models/activity-definition.model';
import { Observable } from 'rxjs';
import { HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ActivityDefinitionsService extends EngineBaseService {

  private bufferActivityDefinitions?: Array<ActivityDefinition>;

  public GetActivityDefinitions(): Observable<Array<ActivityDefinition>> {
    return new Observable(observe => {
      if (this.bufferActivityDefinitions) {
        observe.next(this.bufferActivityDefinitions);
        observe.complete();
        return;
      }
      this.http.get<Array<ActivityDefinition>>(this.baseApi + 'Modelling/GetActivityDefinitions').subscribe({
        next: (result) => {
          this.bufferActivityDefinitions = result;
          observe.next(this.bufferActivityDefinitions);
          observe.complete();
        },
        error: observe.error
      });
    });
  }

  public GetActivityDefinitionById(idActivityDefinition: number): Observable<ActivityDefinition> {
    return new Observable(observe => {
      var find = this.bufferActivityDefinitions?.find(a => a.id == idActivityDefinition);
      if (find) {
        observe.next(find);
        observe.complete();
        return;
      }
      var params: HttpParams = new HttpParams();
      params = params.set('idActivityDefinition', idActivityDefinition.toString());

      this.http.get<ActivityDefinition>(this.baseApi + 'Modelling/GetActivityDefinitionById', { params }).subscribe({
        next: (result) => {
          this.bufferActivityDefinitions?.push(result);
          observe.next(result);
          observe.complete();
        },
        error: observe.error
      });
    });
  }
}
