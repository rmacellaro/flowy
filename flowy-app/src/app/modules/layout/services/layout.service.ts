import { EventEmitter, Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class LayoutService {

  public isShowSidebar: boolean = true;
  public onSidebarToggle: EventEmitter<boolean> = new EventEmitter();

  public theme: string = '';
  public onThemeSwitch: EventEmitter<string> = new EventEmitter();

  constructor() {
    // get theme grom localstore
    console.log('COSTRUTTORE LAYOUT SERVICE');
  }

  public sidebarToggle(force: boolean | undefined = undefined): boolean {
    if (force != undefined) {
      this.isShowSidebar = force;
    } else {
      this.isShowSidebar = !this.isShowSidebar;
    }
    this.onSidebarToggle.emit(this.isShowSidebar);
    return this.isShowSidebar;
  }

  public switchTheme(newTheme: string) : string {

    return this.theme;
  }
}
