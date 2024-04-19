import { Component, Input, OnInit } from '@angular/core';


@Component({
  selector: 'ui-button-info',
  template: `
    <div *ngIf="isShow" class="bg-body-tertiary rounded p-1">
      <div class="d-flex align-items-center">
        <div class="flex-grow-1 p-1">
          <ng-content select="[button]"></ng-content>
        </div>
        <div class="flex-grow-0 p-1">
          <div class="dropdown">
            <button class="btn btn-link text-muted" type="button" data-bs-toggle="dropdown" aria-expanded="false">
              <i class="bi bi-info-circle"></i>
            </button>
            <ul class="dropdown-menu p-3 f-s-08 fw-light text-muted">
              <ng-content select="[info]"></ng-content>
            </ul>
          </div>
        </div>
      </div>
    </div>
  `
})
export class UiButtonInfoComponent implements OnInit {

  @Input() isShow: number | boolean | undefined = true;

  constructor() {

  }

  ngOnInit(): void {
    
  }
}
