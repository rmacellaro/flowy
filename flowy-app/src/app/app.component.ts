import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Dropdown } from 'bootstrap';
import { LayoutModule } from './modules/layout/layout.module';
import { LayoutService } from './modules/layout/services/layout.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    LayoutModule,
    RouterModule
  ],
  template: `
    <div class="layout-body">
      <!-- LeftBar -->
      <layout-leftbar>
        <div class="text-center p-2">
          <a (click)="layout.sidebarToggle()" href="javascript:;" class="text-color">
            <i class="bi bi-list"></i>
          </a>
        </div>
        <div class="text-center p-2">
          <a [routerLink]="['/']" title="Flowy">
            <img src="/assets/img/logo.svg" />
          </a>
          <!--<span class="text-3xl font-semibold mx-2 md:hidden">Flowy</span>-->
        </div>
        <div class="text-center p-2">
          <a href="javascript:;"
            [routerLink]="['/','engine']"
            pTooltip="Motore dei processi Flowy"
            class="text-color">
            <i class="bi bi-diagram-3"></i>
          </a>
        </div>
        <div class="text-center p-2">
          <a href="javascript:;"
            [routerLink]="['/','camunda']"
            pTooltip="Motore dei processi Camunda"
            class="text-color">
            <i class="bi bi-c-square"></i>
          </a>
        </div>
        <div class="flex-grow-1"></div>
        <div class="text-center p-2">
          <a href="javascript:;" [routerLink]="['/','console']" class="text-color">
            <i class="bi bi-gear"></i>
          </a>
        </div>
      </layout-leftbar>
      <!-- Main -->
      <div class="layout-content">
        <router-outlet />
      </div>
    </div>
  `
})
export class AppComponent implements OnInit{


  constructor(public layout: LayoutService) {
    console.log(Dropdown.VERSION);
  }

  ngOnInit() {

  }
}
