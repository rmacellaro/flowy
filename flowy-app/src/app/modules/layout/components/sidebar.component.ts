import { Component, OnInit } from '@angular/core';
import { LayoutService } from '../services/layout.service';

@Component({
  selector: 'layout-sidebar',
  host: {
    class: 'layout-sidebar',
    '[style.display]':"layout.isShowSidebar ? '' : 'none' "
  },
  template: `<ng-content></ng-content>`
})
export class LayoutSidebarComponent implements OnInit {

  constructor(public layout: LayoutService) { }

  ngOnInit(): void { }
}
