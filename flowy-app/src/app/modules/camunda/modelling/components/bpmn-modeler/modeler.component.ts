import { AfterContentInit, Component, ElementRef, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';

import { Observable } from 'rxjs';

import { EventEmitter } from '@angular/core';

import BpmnModeler from 'bpmn-js/lib/Modeler';
import CommandStack from 'diagram-js/lib/command/CommandStack';
import EventBus from 'diagram-js/lib/core/EventBus';
import ZoomScroll from 'diagram-js/lib/navigation/zoomscroll/ZoomScroll';

//import propertiesPanelModule from "bpmn-js-properties-panel";
//import { propertiesPanelModule } from "bpmn-js-properties-panel";
//import * as pp from 'bpmn-js-properties-panel';
import * as  zeebeModdleDescriptor from 'zeebe-bpmn-moddle/resources/zeebe.json';
import * as pp from 'bpmn-js-properties-panel';
//import * as camundaModdleDescriptor from 'camunda-bpmn-moddle/resources/camunda.json';
//console.log('PPPPPPPPPPPPPPPPPPPPPPPPPPP', pp);

import { Element, Root } from 'bpmn-js/lib/model/Types';

@Component({
  selector: 'camunda-modelling-bpmn-modeler',
  template: `
    <div class="w-100 h-100 d-flex flex-row">
      <div class="flex-grow-1 bg-bpmn-grid">
        <div class="d-flex flex-column h-100 w-100">
          <div class="flex-grow-0 p-1">
            <div class="row m-0 mt-1">
              <div class="col-auto">
                <!--<div class="rounded-1 border px-2 py-0 bg-body-tertiary">
                  <div class="d-flex flex-row align-items-center">
                    <a class="btn btn-link p-1 mx-1" href="javascript:;" (click)="selectRoot()"><i class="bi bi-gear-wide-connected"></i></a>
                  </div>
                </div>  -->
              </div>
              <div class="col">
              </div>
              <div class="col-auto">
                <div class="rounded-1 border px-2 py-0 bg-body-tertiary">
                  <div class="d-flex flex-row align-items-center">
                    <a class="btn btn-link p-1 mx-1" [ngClass]="{'disabled': !canUndo}" href="javascript:;" (click)="executeStack('UNDO')">
                      <i class="bi bi-arrow-counterclockwise"></i>
                    </a>
                    <a class="btn btn-link p-1 mx-1" [ngClass]="{'disabled': !canRedo}" href="javascript:;" (click)="executeStack('REDO')">
                      <i class="bi bi-arrow-clockwise"></i>
                    </a>
                  </div>
                </div>
              </div>
              <div class="col-auto">
                <div class="rounded-1 border px-2 py-0 bg-body-tertiary">
                  <div class="d-flex flex-row align-items-center">
                    <a class="btn btn-link p-1 mx-1" href="javascript:;" (click)="download('SVG')"><i class="bi bi-filetype-svg"></i></a>
                    <a class="btn btn-link p-1 mx-1" href="javascript:;" (click)="download('XML')"><i class="bi bi-filetype-xml"></i></a>
                  </div>
                </div>
              </div>
              <div class="col-auto">
                <div class="rounded-1 border px-2 py-0 bg-body-tertiary">
                  <div class="d-flex flex-row align-items-center">
                    <a class="btn btn-link p-1 mx-1" href="javascript:;" (click)="zoom('IN')"><i class="bi bi-zoom-in"></i></a>
                    <a class="btn btn-link p-1 mx-1" href="javascript:;" (click)="zoom('OUT')"><i class="bi bi-zoom-out"></i></a>
                    <a class="btn btn-link p-1 mx-1" href="javascript:;" (click)="zoom('RESET')"><i class="bi bi-aspect-ratio"></i></a>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="flex-grow-1" #divModeler>
          </div>
        </div>
      </div>
      <div class="flex-grow-0 border-start overflow-auto" #divProperties>
      </div>
    </div>
  `,
  styleUrls: [ '../../common/bpmn.scss' ]
})
export class CamundaModellingBpmnModelerComponent implements OnInit, OnDestroy, AfterContentInit {

  @ViewChild('divModeler', { static: true }) private divModeler?: ElementRef;
  @ViewChild('divProperties', { static: true }) private divProperties?: ElementRef;

  @Input() public schema?: string;

  public bpmnModeler?: BpmnModeler;
  public bpmnZoomScroll?: ZoomScroll;
  public bpmnCommandStack?: CommandStack;
  public bpmnEventBus?: EventBus;
  public bpmnModeling?: any;
  public bpmnModdle?: any;
  public bpmnCanvas?: any;
  public bpmnPropertiesPanel?: any;

  public canRedo: boolean = false;
  public canUndo: boolean = false;
  public stateSelect: 'NONE' | 'SINGLE' | 'MULTIPLE' = 'NONE';

  public element?: Element;
  public root?: Root;

  public onCurrentElement: EventEmitter<Element> = new EventEmitter();

  constructor(
  ) {
    console.log(':::::::: customPropertiesProvider');

    this.bpmnModeler = new BpmnModeler({
      container: this.divModeler?.nativeElement,
      propertiesPanel: {
        parent: this.divProperties?.nativeElement
      },
      additionalModules: [
        pp.BpmnPropertiesPanelModule,
        pp.BpmnPropertiesProviderModule,
        //pp.CamundaPlatformPropertiesProviderModule,
        pp.ZeebePropertiesProviderModule
      ],
      keyboard: {
        bindTo: document
      },
      moddleExtensions: {
        zeebe: zeebeModdleDescriptor
      }
    });

    this.bpmnZoomScroll = this.bpmnModeler.get<any>('zoomScroll');
    this.bpmnEventBus = this.bpmnModeler?.get('eventBus');
    this.bpmnCommandStack = this.bpmnModeler?.get('commandStack');
    this.bpmnModeling = this.bpmnModeler?.get<any>('modeling');
    this.bpmnModdle = this.bpmnModeler?.get('moddle');
    this.bpmnCanvas = this.bpmnModeler?.get('canvas');
    this.bpmnPropertiesPanel = this.bpmnModeler?.get('propertiesPanel');

    this.bpmnEventBus?.on('element.changed', (event: any) => {
      //console.log('CHANGE ', event.element);
      setTimeout(()=> {
        this.canRedo = this.bpmnCommandStack?.canRedo() ?? false;
        this.canUndo = this.bpmnCommandStack?.canUndo() ?? false;
      }, 100);
    });

    /*this.bpmnEventBus?.on('selection.changed', (event: any) => {
      //console.log('============== SELECT', event.newSelection);

      if (event.newSelection.length <= 0) {
        this.stateSelect = 'NONE';
        //this.stateSelect = 'SINGLE';
        //this.root = this.bpmnCanvas.getRootElement();
        //this.element = this.root;
        return;
      }
      else if(event.newSelection.length == 1) {
        this.stateSelect = 'SINGLE';
        this.element = event.newSelection[0];
      }
      else {
        this.stateSelect = 'MULTIPLE';
        this.element = event.newSelection[0];
      }

      this.sendElement();

    });

    this.bpmnEventBus?.on('directEditing.complete', (event: any) => {
      console.log('directEditing.complete', event);
      this.sendElement();
    });*/
  }

  ngAfterContentInit(): void {
    console.log('--------', this.divProperties?.nativeElement);
    this.bpmnModeler?.detach();
    this.bpmnModeler?.attachTo(this.divModeler?.nativeElement);
    this.bpmnPropertiesPanel?.detach();
    this.bpmnPropertiesPanel?.attachTo(this.divProperties?.nativeElement);
  }

  ngOnInit(): void {
    this.importXml(this.schema);
  }

  ngOnDestroy(): void {
    // destroy BpmnJS instance
    this.bpmnModeler?.destroy();
  }

  public importXml(schema?: string) {
    if (!schema) { return; }
    setTimeout(()=> {
      this.bpmnModeler?.importXML(schema);
    }, 50);
  }

  public selectRoot(): void {
    //this.stateSelect = 'SINGLE';
    //this.root = this.bpmnCanvas.getRootElement();
    //this.element = this.root;
    //this.sendElement();
    this.root = this.bpmnCanvas.getRootElement();
    const selection: any = this.bpmnModeler?.get('selection');
    selection.select(this.root);
  }

  public sendElement(): void {
    setTimeout(()=> {
      console.log('======== SEND ELEMENT', this.element);
      this.onCurrentElement.emit(this.element);
    }, 10);
  }

  public zoom(mode: 'IN' | 'OUT' | 'RESET'): void{
    if(mode == 'IN') { this.bpmnZoomScroll?.stepZoom(1, { x: 100, y: 100}); }
    if(mode == 'OUT') { this.bpmnZoomScroll?.stepZoom(-1, { x: 100, y: 100}); }
    if(mode == 'RESET') { this.bpmnZoomScroll?.reset(); }
  }

  public executeStack(mode: 'UNDO' | 'REDO'): void {
    if (mode == 'UNDO') { this.bpmnCommandStack?.undo(); }
    if (mode == 'REDO') { this.bpmnCommandStack?.redo(); }
  }

  public getXml(): Observable<string> {
    return new Observable((observe) => {
      this.bpmnModeler?.saveXML({
        format: true,
        preamble: true
      }).then(result => {
        if (result.error) { observe.error(result.error);}
        else { observe.next(result.xml);}
      }).catch((result) => {
        observe.error(result);
      });
    });
  }

  public download(mode: 'SVG' | 'XML'): void {
    if (mode == 'SVG') {
      this.bpmnModeler?.saveSVG().then((result) => {
        this.open('image/svg+xml', result.svg);
      });
    }
    if (mode == 'XML') {
      this.bpmnModeler?.saveXML({
        format: true,
        preamble: true
      }).then((result) => {
        this.open('text/xml', result.xml);
      });
    }
  }

  private open(type: string, content?: string) {
    if (!content) { return; }
    let blob = new Blob([content], {type: type});
    let url = URL.createObjectURL(blob);
    let win = window.open(url);
    if (win){
      win.onload = () => {
        URL.revokeObjectURL(url); //Releases the resources
      };
    }
  }
}
