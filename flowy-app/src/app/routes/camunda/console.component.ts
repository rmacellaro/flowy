import { Component, OnInit } from '@angular/core';
import { AppScopeHelper } from './scope.helper';

@Component({
  selector: 'app-scope-console-start',
  template: `
    <div class="p-5">
      <div>Start</div>
      <span subtitle>Avvia la lavorazione di una nuova istanza</span>
    </div>
    <div class="p-5">
      <camunda-processing-console-main [idScope]="helper.scope?.id"></camunda-processing-console-main>
    </div>
  `
})
export class AppCamundaScopeConsoleStartComponent implements OnInit {

  constructor(
    public helper: AppScopeHelper
  ) { }

  ngOnInit(): void { }
}
