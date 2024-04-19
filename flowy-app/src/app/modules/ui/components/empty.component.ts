import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'ui-empty',
  template: `
    <div class="w-100 text-center opacity-50">
      <div class="f-s-3 text-muted" *ngIf="showImg">
        <i class="bi bi-cloud-slash"></i>
      </div>
      <div class="text-muted" *ngIf="showText">Nessun dato</div>
    </div>
  `,
  styles: [``]
})
export class UiEmptyComponent implements OnInit {

  @Input() showImg: boolean = true;
  @Input() showText: boolean = true;

  constructor() { }

  ngOnInit(): void { }
}
