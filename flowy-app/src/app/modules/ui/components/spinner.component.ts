import { Component, Input, OnInit } from "@angular/core";

@Component({
  selector: 'ui-spinner',
  template: `
    <div class="spinner {{contentClass}}" *ngIf="isInLoading">
      <div class="spinner-container">
        <div class="{{mode}} {{color}}" [style]="style" role="status">
          <span class="visually-hidden">Loading...</span>
        </div>
        <ng-content></ng-content>
      </div>
    </div>
  `,
  styles: [`
    .spinner {
      position: absolute;
      top: 0;
      left: 0;
      bottom: 0;
      right: 0;
      background: #ffffffdc;
      width: 100%;
      height: 100%;
      display: grid;
      place-items: center;
      border-radius: 4px;
      z-index: 10;
    }
    .spinner-container {
      text-align: center;
    }
  `]
})
export class UiSpinnerComponent implements OnInit {

  @Input() public isInLoading: boolean = true;
  @Input() public color: string = '';
  @Input() public mode: string = 'spinner-border';
  @Input() public style: string = '';
  @Input() public contentClass: string = 'text-content';

  constructor(){}

  ngOnInit(): void { }

}