import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Scope } from '../../modules/camunda/modelling/models/scope.model';

@Component({
  selector: 'app-camunda-scopes',
  template: `
    <div class="container py-4">
      <camunda-modelling-scopes-list (onSelect)="selectScope($event)"></camunda-modelling-scopes-list>
    </div>
  `
})
export class AppCamundaScopesComponent implements OnInit {


  constructor(
    private router: Router,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void { }

  selectScope(item?: Scope): void {
    this.router.navigate([item?.id], {relativeTo: this.route});
  }
}
