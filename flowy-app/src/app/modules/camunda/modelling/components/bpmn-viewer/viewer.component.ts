import { AfterContentInit, Component, ElementRef, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';

import BpmnViewer from 'bpmn-js/lib/Viewer';
import Canvas from 'diagram-js/lib/core/Canvas';
import EventBus from 'diagram-js/lib/core/EventBus';
import Overlay from 'diagram-js/lib/features/overlays/Overlays';

import * as zeebeModdleDescriptor from 'zeebe-bpmn-moddle/resources/zeebe.json';

@Component({
  selector: 'camunda-modelling-bpmn-viewer',
  template: `
    <div class="d-flex flex-column w-100 h-100 bg-bpmn-grid">
      <div class="flex-grow-0 p-2">
        <div class="row m-0">
          <div class="col-auto">
            <div class="btn-group">
              <a class="btn btn-secondary" href="javascript:;" (click)="zoom('IN')"><i class="bi bi-zoom-in"></i></a>
              <a class="btn btn-secondary" href="javascript:;" (click)="zoom('OUT')"><i class="bi bi-zoom-out"></i></a>
              <a class="btn btn-secondary" href="javascript:;" (click)="zoom('RESET')"><i class="bi bi-aspect-ratio"></i></a>
            </div>
          </div>
          <div class="col">
          </div>
        </div>
      </div>
      <div class="flex-grow-1" #viewer></div>
    </div>
  `,
  styleUrls: [ '../../common/bpmn.scss' ]
})
export class CamundaModellingBpmnViewerComponent implements OnInit, OnDestroy, AfterContentInit {

  @ViewChild('viewer', { static: true }) private viewer?: ElementRef;

  @Input() public showDeploymentsLink?: boolean = false;
  @Input() public set schema(value: string | undefined){
    this.loadXml(value);
  }

  public currentZoom: number = 1;

  private bpmnViewer?: BpmnViewer;
  private bpmnEventBus?:EventBus;
  private bpmnCanvas?:Canvas;
  private bpmnOverlays?:Overlay;
  private bpmnElementRegistry?: any;

  constructor() {
    this.bpmnViewer = new BpmnViewer({
      container: this.viewer?.nativeElement,
      keyboard: {
        bindTo: document
      },
      moddleExtensions: {
        zeebe: zeebeModdleDescriptor
      }
    });
    this.bpmnEventBus = this.bpmnViewer.get('eventBus');
    this.bpmnCanvas = this.bpmnViewer.get('canvas');
    this.bpmnOverlays = this.bpmnViewer.get('overlays');
    this.bpmnElementRegistry = this.bpmnViewer.get('elementRegistry');
  }

  ngAfterContentInit(): void {
    this.bpmnViewer?.attachTo(this.viewer?.nativeElement);

    //this.bpmnCanvas?.zoom('fit-viewport');
    this.bpmnCanvas?.zoom(this.currentZoom);
    // attach an overlay to a node


    // add marker
    //canvas.addMarker('Activity_1rwsog2', 'needs-discussion');
    // you may hook into any of the following events
    /*var events = [
      'element.hover',
      'element.out',
      'element.click',
      'element.dblclick',
      'element.mousedown',
      'element.mouseup'
    ];

    var th = this;
    events.forEach(function(event) {

      th.bpmnEventBus?.on(event, function(e: any) {
        // e.element = the model element
        // e.gfx = the graphical element

        //console.log(event, 'on', e.element, e.element.id);
      });
    });*/

  }

  ngOnInit(): void {
  }

  loadXml(xml?: string): void {
    if (!xml) { return; }
    this.bpmnViewer?.importXML(xml).then((xml) => {
      //console.log(xml);
      /*this.bpmnOverlays?.add('Activity_1rwsog2', {
        position: {
          bottom: 15,
          right: 15
        },
        html: '<div class="diagram-budge">10</div>'
      });
      this.bpmnCanvas?.addMarker('Activity_1rwsog2', 'aaa-s-s---s');*/

    });
  }

  ngOnDestroy(): void {
    // destroy BpmnJS instance
    this.bpmnViewer?.destroy();
  }

  zoom(mode: 'IN' | 'OUT' | 'RESET') {
    if (mode == 'RESET') { this.currentZoom = 1; }
    else if (mode == 'IN') { this.currentZoom += 0.1; }
    else if (mode == 'OUT') { this.currentZoom -= 0.1; }
    this.bpmnCanvas?.zoom(this.currentZoom);

    //this.centerElement('ActivityInterazioneUtente');
    //this.addOverlay('ActivityInterazioneUtente');
  }

  centerElement(elementId: string): void  {
    console.log('centerElement', elementId);
    // assuming we center on a shape.
    // for connections we must compute the bounding box
    // based on the connection's waypoints

    var bbox = this.bpmnElementRegistry.get(elementId);

    var currentViewbox = this.bpmnCanvas?.viewbox();
    if (!currentViewbox) { return; }
    var elementMid = {
      x: bbox.x + bbox.width / 2,
      y: bbox.y + bbox.height / 2
    };

    this.bpmnCanvas?.viewbox({
      x: elementMid.x - currentViewbox.width / 2,
      y: elementMid.y - currentViewbox.height / 2,
      width: currentViewbox.width,
      height: currentViewbox.height
    });
    this.currentZoom = 1.5;
    this.zoom('IN');
  }

  private overlays: Array<{element: string, offser: number}> = [];

  addOverlay(
    element: string,
    html: string
  ): void {
    var pos = { left: 0, bottom: 10 };

    var exist = this.overlays.find(o => o.element == element);
    if (exist) {
      exist.offser += 10;
      pos.left = exist.offser;
    } else {
      this.overlays.push({
        element: element,
        offser: pos.left
      });
    }

    this.bpmnOverlays?.add(element, {
      html: html,
      position: pos
    });
  }

  clearOverlay(): void {
    this.overlays = [];
    this.bpmnOverlays?.clear();
  }
}
