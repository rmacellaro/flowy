import { Component, OnInit } from '@angular/core';
import { LayoutService } from '../services/layout.service';

@Component({
  selector: 'layout-leftbar',
  template: `
    <div class="h-100 w-100 d-flex flex-row flex-md-column align-items-center p-2">
      <ng-content></ng-content>
    </div>
  `,
  host: {
    class: 'layout-leftbar'
  }
})
export class LayoutLeftbarComponent implements OnInit {

  constructor(
    public layout: LayoutService
  ) { }

  ngOnInit(): void { }
}
