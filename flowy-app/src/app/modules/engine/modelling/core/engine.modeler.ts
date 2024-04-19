import { dia, shapes, g, elementTools } from '@joint/core';

import { LinkElement } from './link.element';
import { NodeElement } from './node.element';
import { ActivityElement } from './activity.element';

import { Node } from '../models/node.model';
import { Link } from '../models/link.model';
import { Activity } from '../models/activity.model';

export class EngineModeler {

  //public onCurrent?: (type: 'NODE' | 'ACTIVITY', idNode?: number | undefined, idActivity?: number | undefined) => void;
  //public onUpdated?: (type: 'NODE' | 'ACTIVITY', idNode: number, property: string, value: any) => void;

  public onSelectNode?: (idNode?: number | undefined) => void;
  public onSelectActivity?: (idNode?: number | undefined, idActivity?: number | undefined) => void;
  public onRenderDone?: () => void;

  private Paper?: dia.Paper;
  private Graph?: dia.Graph;

  private zoomOffset: number = 0.05;
  private zoomLevel: number = 1;

  private currentNodes: { [any:number]: NodeElement } = {};
  private currentNode?: NodeElement;
  private currentActivity?: ActivityElement;

  constructor(diagramDiv: any) {
    var bgImageDataURL = 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAMgAAADICAMAAACahl6sAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAyVpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuNi1jMTQ4IDc5LjE2NDAzNiwgMjAxOS8wOC8xMy0wMTowNjo1NyAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RSZWY9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZVJlZiMiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIDIxLjAgKE1hY2ludG9zaCkiIHhtcE1NOkluc3RhbmNlSUQ9InhtcC5paWQ6OTcwQkI2N0VGNjczMTFFRTk1NENDMzhDQ0IwNTRFNDUiIHhtcE1NOkRvY3VtZW50SUQ9InhtcC5kaWQ6OTcwQkI2N0ZGNjczMTFFRTk1NENDMzhDQ0IwNTRFNDUiPiA8eG1wTU06RGVyaXZlZEZyb20gc3RSZWY6aW5zdGFuY2VJRD0ieG1wLmlpZDpEQTQ0RUM1N0Y2NEIxMUVFOTU0Q0MzOENDQjA1NEU0NSIgc3RSZWY6ZG9jdW1lbnRJRD0ieG1wLmRpZDpEQTQ0RUM1OEY2NEIxMUVFOTU0Q0MzOENDQjA1NEU0NSIvPiA8L3JkZjpEZXNjcmlwdGlvbj4gPC9yZGY6UkRGPiA8L3g6eG1wbWV0YT4gPD94cGFja2V0IGVuZD0iciI/PtMnRewAAABIUExURQAAAL+/vyAgIO/v7xAQEDAwMH9/f2tra2xsbA4ODmBgYPf399/f38/Pz6+vr3JyckBAQHNzc5+fn/j4+FBQUPb29g0NDf///6xEwpMAAAAYdFJOU///////////////////////////////AM0TLuoAAAFSSURBVHja7NbJTsMwEADQyb60pSyF/P+fgilCyQEOSEWx++bgOI4U50kzGcfyDxGF7AGyO8gCIrVAFDsICAgICIjODgKi2EHUCAiIYgcBAQEBcdYCAQEBAQEBAXHWAgEBAQHREEFAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBA7hLSzG10Vf6Qpu6b5bl9yB4yz2ms6uwhdbW65Axpr5Aue8ilT+OxbXKHnFJSHbs+/99vVXeP0RfREE/VZ14dxinGlwKa7lN8xLkAyJQg0+0hEV+XWK9db78n6/VfFv8aP79s+2QzWVbfvVm7VQxpl6GA1Hod32I4OMaDgICAgICAgICAgICAgICAgICAgICAgICAgICAgIDsKEBAQEBAyoXo7CAgICAgICAgDo0gICAgIBoiCIhiBwFR7CAg+ggICAgICIjODgKi2EFAFPvuNnkXYABdoO2RGKtfJgAAAABJRU5ErkJggg=='


    this.Graph = new dia.Graph({ }, { cellNamespace: shapes });
    this.Paper = new dia.Paper({
      model: this.Graph,
      cellViewNamespace: shapes,
      width: '100%',
      height: '100%',
      /*width: 600,
      height: 400,*/
      gridSize: 10,
      drawGrid: true,
      /*width: 2480,
      height: 3508,
      background: {
        color: '#dfdfdf',
      },*/
      //drawGrid: { name: "mesh" },
      background: {
        image: bgImageDataURL,
        position: { x: -100, y: -100 },
        opacity: .2
      },
      async: true,
      overflow: false,
      snapLinks: true,
      linkPinning: false,
      interactive: {
        stopDelegation: false,
        linkMove: false,
        elementMove: true,
        addLinkFromMagnet: false,
        labelMove: false
      },
      sorting: dia.Paper.sorting.APPROX,
      defaultConnector: { name: 'rounded' },
      defaultRouter: { name: 'manhattan'}
    });

    this.Paper.on('element:pointerdblclick', (elementView) => {
      if (elementView.model instanceof NodeElement) {
        this.SetCurrentNode(elementView.model as NodeElement);
      } else if (elementView.model instanceof ActivityElement) {
        this.SetCurrentActivity(elementView.model as ActivityElement);
      }
    });

    this.Paper.on('element:mouseover', () => {
      if (!this.Paper) { return;}
      this.Paper.el.style.cursor = 'pointer';
    });
    this.Paper.on('blank:pointerdown', (evt) => {
      var current = this.Paper?.translate();
      if (!evt.clientX || !evt.clientY || !current || !this.Paper) { return; }
      evt.data = {
        scrollX: current.tx, clientX: evt.clientX,
        scrollY: current.ty, clientY: evt.clientY
      };
      this.Paper.el.style.cursor = 'grabbing';
    });
    this.Paper.on('blank:mouseover', () => {
      if (!this.Paper) { return;}
      this.Paper.el.style.cursor = 'grab';
    });
    this.Paper.on('blank:pointerup', () => {
      if (!this.Paper) { return;}
      this.Paper.el.style.cursor = 'grab';
    });
    this.Paper.on('blank:pointermove', (evt) => {
      if (!evt.clientX || !evt.clientY) { return; }
      var x = (evt.data.scrollX - (evt.data.clientX - evt.clientX));
      var y = (evt.data.scrollY - (evt.data.clientY - evt.clientY));
      this.Paper?.translate(x, y);
    });
    this.Paper.on('blank:pointerdblclick', () => {
      this.SetCurrentNode();
      this.SetCurrentActivity();
    });
    this.Paper.on('blank:mousewheel', (evt, x, y, delta) => {
      evt.preventDefault();
      var point = new g.Point(x, y);
      this.Zoom(delta, point);
      //console.log('ssssss', evt, x, y, delta);
    });
    this.Paper.on('cell:mousewheel', (cellView, evt, x, y, delta) => {
      evt.preventDefault();
      var point = new g.Point(x, y);
      this.Zoom(delta, point);
      //console.log('xxxx');
    });

    this.Paper.on('render:done', (stats) => {
      //console.log('RRRRRRRRRRRRR', stats);
      this.onRenderDone?.call(this);
    });

    /*this.Paper.on('element:pointerup', (elementView) => {
      var element = elementView.model as NodeElement;
      //console.log();
      if (element.IsChangedPosition){
        console.log('Aggiorna posizione nodo', element.X, element.Y);
        element.IsChangedPosition = false;
      }
    });*/

    diagramDiv.appendChild(this.Paper.el);
  }

  public GoToNode(id: number): void {
    //console.log('gotonode',id);
    if (!this.currentNodes) { return; }
    var node = this.currentNodes[id];
    //console.log('gotonode',node);
    if (!node) { return; }
    this.SetCurrentNode(node);
  }

  public GoToActivity(idNode: number, idActivity: number): void {
    //console.log('gotonode',id);
    if (!this.currentNodes) { return; }
    var nodEle = this.currentNodes[idNode];
    //console.log('gotonode',node);
    if (!nodEle) { return; }
    var actEle = nodEle.Activities.find(a => a.Id == idActivity);
    if (!actEle) { return; }
    this.SetCurrentActivity(actEle);
  }

  public SetCurrentNode(newElement: NodeElement | undefined = undefined): void {
    if (!this.Paper) { return; }
    if (this.currentNode) { this.currentNode.Unselect(this.Paper); }
    if (this.currentActivity) { this.currentActivity.Unselect(this.Paper); }

    this.currentNode = newElement;

    this.currentNode?.Select(this.Paper);
    this.onSelectNode?.call(this, this.currentNode?.Id);
  }

  public SetCurrentActivity(newActivity: ActivityElement | undefined = undefined): void {
    if (!this.Paper) { return; }
    if (this.currentNode) { this.currentNode?.Unselect(this.Paper); }
    if (this.currentActivity) { this.currentActivity?.Unselect(this.Paper); }

    this.currentActivity = newActivity;
    this.currentActivity?.Select(this.Paper);
    this.onSelectActivity?.call(this, this.currentActivity?.IdNode, this.currentActivity?.Id);
  }

  public Fit(): void {
    this.Paper?.transformToFitContent({
      useModelGeometry: false,
      padding: 10,
      maxScale: 1,
      verticalAlign: "middle",
      horizontalAlign: "middle"
    });
    //console.log('ci siamo', this.Paper?.scale());
    this.zoomLevel = this.Paper?.scale().sx ?? 1;
  }

  public Zoom(delta: number, center?: g.Point): void {
    if (!this.Paper) { return;}
    if (delta > 0) {
      this.Paper.el.style.cursor = 'zoom-in';
      this.ZoomIn(center);
    }
    else {
      this.Paper.el.style.cursor = 'zoom-out';
      this.ZoomOut(center);
    }
  }

  public ZoomReset(): void {
    var center = this.Paper?.getArea().center();
    if (!center) { return;}
    this.zoomLevel = 1;
    this.Paper?.scaleUniformAtPoint(this.zoomLevel, center);
  }

  public ZoomIn(center?: g.Point): void {
    if (!center) { center = this.Paper?.getArea().center();}
    if (!center) { return;}
    this.zoomLevel = Math.min(3, this.zoomLevel + this.zoomOffset);
    this.Paper?.scaleUniformAtPoint(this.zoomLevel, center);
  }

  public ZoomOut(center?: g.Point): void {
    if (!center) { center = this.Paper?.getArea().center();}
    if (!center) { return;}
    this.zoomLevel = Math.max(0.2, this.zoomLevel - 0.2);
    this.Paper?.scaleUniformAtPoint(this.zoomLevel, center);
  }

  public UpdateNode(node: Node): void {
    if (!node.id) { return; }
    var nodEle = this.currentNodes[node.id];
    if (!nodEle) { return; }
    nodEle.UpdateNode(node);
  }

  public UpdateActivity(activity: Activity): void {
    if (!activity.id) { return; }
    if (!activity.idNode) { return; }
    var nodEle = this.currentNodes[activity.idNode];
    if (!nodEle) { return; }
    var actEle = nodEle.Activities.find(a => a.Id == activity.id);
    if (!actEle) { return; }
    actEle.UpdateActivity(activity);
  }

  public AddNode(node: Node): void {
    if (!node.id) { return; }
    if (!this.Graph) { return; }
    if (!this.Paper) { return; }

    var element = new NodeElement(node);

    this.Graph.addCell(element);
    this.currentNodes[element.Id] = element;
  }

  public AddActivityToNode(activity: Activity): void {
    if (!activity.idNode) { return; }
    var element = this.currentNodes[activity.idNode];
    if (!element) { return; }
    if (!this.Graph) { return; }

    var activityElement = new ActivityElement(activity);
    this.Graph.addCell(activityElement);
    element.AddActivity(activityElement);
  }

  public AddOutPortToNode(idNode?: number, key?: string): void {
    if (!idNode) { return; }
    if (!key) { return; }
    var element = this.currentNodes[idNode];
    if (!element) { return; }
    const port: dia.Element.Port = {
      id: key,
      group: 'out',
      attrs: { label: { html: key}}
    };
    element.addPort(port);
  }

  public AddLink(link: Link): void {
    if (!this.Graph) { return; }
    if (!this.Paper) { return; }
    if (!link.idSourceNode) { return; }
    if (!link.idTargetNode) { return; }
    var source = this.currentNodes[link.idSourceNode];
    var target = this.currentNodes[link.idTargetNode];

    source.Outputs.push(link.idTargetNode);
    const linkElement = new LinkElement({
      z: 40,
      source: { id: source.Id, port: link.key },
      target: { id: target.Id, port: 'IN' },
      router: { name: 'manhattan' },
      connector: { name: 'rounded' },
      attrs: {
        line: { stroke: '#666666', strokeWidth: 2 }
      }
    });
    linkElement.addTo(this.Graph).reparent();
  }

  /*public Positioning(): void {
    // prendo il nodo start
    let start: any = undefined;

    Object.entries(this.currentNodes).forEach(([key, value]) => {
      if(value.IsRoot){ start = value; }
    });
    if (start == undefined) { return; }

    let offsetY: number = 100;

    const recursive = (element: NodeElement, deep: number) => {
      //console.log('Recursive', modEle.element.size());
      let x = (deep * 200 );
      let y = offsetY;
      //console.log('?????', x, deep);
      //console.log('eccolo', element.PositionX, element.PositionY);
      if (element.PositionX != 0) { x = element.PositionX; }
      if (element.PositionY != 0) { y = element.PositionY; }

      //element.translate(x, y);
      element.SetPosition(x,y);
      offsetY += element.size().height + 50;

      element.Outputs.forEach(link => {
        var find = this.currentNodes[link];
        recursive(find, deep + 1);
      });
    };

    recursive(start, 1);
    this.Paper?.hideTools();
  }*/

  public GetNodeElement(idNode: number): NodeElement | undefined {
    return this.currentNodes[idNode];
  }
}
