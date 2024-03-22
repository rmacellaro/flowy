import { EventEmitter, Injectable } from '@angular/core';
import { Scope } from '../../modules/camunda/modelling/models/scope.model';

@Injectable({
  providedIn: 'root'
})
export class AppScopeHelper {

  public scope?: Scope;
  public onScope: EventEmitter<Scope> = new EventEmitter();

  public setScope(scope: Scope | undefined): void {
    console.log('setScope', scope);
    setTimeout(()=> {
      this.scope = scope;
      this.onScope.emit(scope);
    }, 10);
  }

  public reset(): void {
    this.setScope(undefined);
  }
}
