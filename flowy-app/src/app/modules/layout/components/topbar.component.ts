import { Component, OnInit } from '@angular/core';
import { LayoutService } from '../services/layout.service';

@Component({
  selector: 'layout-topbar',
  template: `
    <div class="flex align-content-center flex-wrap h-full mx-3">
      <div class="flex align-items-center justify-content-center px-2">
        <a (click)="layout.sidebarToggle()" href="javascript:;" class="text-color">
          <i class="pi pi-bars"></i>
        </a>
      </div>
      <div class="flex align-items-center justify-content-center">
        <a [routerLink]="['/']">
          <img src="/assets/img/logo.svg" style="height: 3rem; width: 3rem;"/>
        </a>
        <span class="text-3xl font-semibold mx-2">Flowy</span>
      </div>
      <div class="flex-grow-1"></div>
      <div class="flex align-items-center px-4">
        <a href="javascript:;" [routerLink]="['/','console']">
          <img src="/assets/img/console-logo.svg" height="25" />
        </a>
      </div>
      <!--<div class="flex align-items-center">

      </div>-->
    </div>
  `
})
export class LayoutTopbarComponent implements OnInit {

  constructor(
    public layout: LayoutService
  ) { }

  ngOnInit(): void { }
}
