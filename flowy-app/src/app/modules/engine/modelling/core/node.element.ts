import { dia, elementTools, util } from '@joint/core';
import { Node } from '../models/node.model';
import { ActivityElement } from './activity.element';

export class NodeElement extends dia.Element {

  public static DefaultWidth: number = 200;
  public static DefaultHeight: number = 50;

  public Id: number;
  public Title: string;
  public IsRoot: boolean;
  public Icon: string;
  public Node: Node;
  public Outputs: Array<number> = [];
  public Activities: Array<ActivityElement> = [];

  public PositionX: number;
  public PositionY: number;

  constructor(node: Node) {
    super({
      id: node.id,
      z: 10
    });

    // set default value
    this.Id = 0;
    this.Title = 'Node';
    this.IsRoot = false;
    this.Icon = 'bi bi-bookmark';
    this.Node = node;
    this.PositionX = 0;
    this.PositionY = 0;

    this.UpdateNode(node);

    if (!this.IsRoot) {
      this.addPort({
        id: 'IN',
        group: 'in',
        attrs: { label: { html: 'IN' }}
      });
    }
  }

  UpdateNode(node: Node): void {
    this.Node = node;
    this.Id = node.id ?? 0;
    this.Title = '' + node.key;
    this.IsRoot = node.key == 'START';

    node.datas?.forEach((data) => {
      if (data.name == 'Schema.Attributes.Icon' && data.value){ this.Icon = data.value; }
      if (data.name == 'Schema.Attributes.Title' && data.value){ this.Title = data.value; }
      if (data.name == 'Schema.PositionX'){ this.PositionX = parseInt(data.value ?? '0');}
      if (data.name == 'Schema.Position.Y'){ this.PositionY = parseInt(data.value ?? '0');}
      if (data.name == 'Schema.Position.Point' && data.value){
        var point = JSON.parse(data.value);
        //console.log('point - - - - - ', point);
        if(point.x) { this.PositionX = point.x; }
        if(point.y) { this.PositionY = point.y; }
      }
    });
    //console.log('node configs', node.datas);

    this.position(this.PositionX, this.PositionY);
    //this.SetPosition(this.PositionX, this.PositionY);
    //console.log("SET POSITION", this.PositionX, this.PositionY, this.GetPosition());

    this.attr('title', { html: this.Title });
    this.attr('icon', { class: this.Icon });

    var cssClass = 'border border-2 rounded p-2 bg-body';
    if (this.IsRoot) { cssClass += ' border-success'; /*' bg-success-subtle';*/}
    this.attr('foreignObject', { class: cssClass});
  }

  AddActivity(activityElement: ActivityElement): void {
    activityElement.position(this.PositionX, this.PositionY);

    var offsetx = 10;
    var offsety = NodeElement.DefaultHeight - 10;
    if (this.Activities.length > 0) {
      offsety += ((ActivityElement.DefaultHeight + 5) * this.Activities.length);
    }

    activityElement.translate(offsetx, offsety);
    //this.size()
    this.Activities.push(activityElement);
    this.embed(activityElement);

    var h = NodeElement.DefaultHeight - 5;
    h += ((ActivityElement.DefaultHeight + 5) * this.Activities.length);
    this.size(200, h);
  }

  SetPosition(x: number, y: number): void {
    this.PositionX = x;
    this.PositionY = y;
    this.position(this.PositionX, this.PositionY);

    var offsetx = 10;
    var offsety = NodeElement.DefaultHeight - 10;
    this.Activities.forEach((a, index) => {
      var y = offsety + ((ActivityElement.DefaultHeight + 5) * index);
      a.position(this.PositionX, this.PositionY);
      a.translate(offsetx, y);
    });
  }

  GetPosition(): {x:number, y: number} {
    return this.position();
  }

  Select(paper: dia.Paper): void {
    this.findView(paper).addTools(new dia.ToolsView({
      tools: [
        new elementTools.Boundary({
          padding: 4,
          rotate: true,
          focusOpacity: 0.5,
          useModelGeometry: true,
          attributes: {
            stroke: '#ffc107',
            fill: 'none',
            'stroke-dasharray': '0 4 0',
            'stroke-width' : '2',
            'stroke-opacity' : '80%',
            'rx' : '10'
          }
        })
      ]
    }));
  }

  Unselect(paper: dia.Paper): void {
    this.findView(paper).hideTools();
  }

  override defaults() {
    //console.log("DEFAULTS ELEMENT");
    return {
      ...super.defaults,
      type: "Node",
      size: {
        width: NodeElement.DefaultWidth,
        height: NodeElement.DefaultHeight
      },
      ports: {
        groups: {
          in: {
            position: { name: 'top' },
            attrs: {
              portBody: { magnet: true, r: 5, fill: '#023047', stroke: '#023047' }
            },
            markup: [
              { tagName: 'circle', selector: 'portBody' }
            ]
          },
          out: {
            position: { name: 'right' },
            attrs: {
              portBody: { magnet: true, r: 5, fill: '#E6A502', stroke:'#023047' }
            },
            markup: [
              { tagName: 'circle', selector: 'portBody' }
            ],
            label: {
              position: {
                name: 'right',
                args: { x: 5, y: -25 }
              },
              markup: util.svg`
              <foreignObject width="60" height="20" overflow="hidden" class="rounded f-s-08 bg-primary-subtle position-absolute text-primary">
                <div class="d-flex flex-row align-items-center w-100 h-100 px-2">
                  <div @selector="label" class="text-ellipsis f-s-08">..</div>
                </div>
              </foreignObject>`
            }
          }
        }
      },
      attrs: {
        foreignObject: {
          width: "calc(w)",
          height: "calc(h)"
        }
      }
    };
  }

  override preinitialize() {
    //console.log("PREINITIALIZE ELEMENT");
    /*this.markup = util.svg `
      <foreignObject @selector="foreignObject" overflow="hidden" class="border border-2 rounded p-2 bg-body">
        <div class="d-flex px-2">
          <div class="p-1">
            <i class="" @selector="icon"></i>
          </div>
          <div class="align-self-center text-ellipsis px-2">
            <div class="text-ellipsis fw-bold" @selector="title">...</div>
            <div class="text-ellipsis f-s-07 text-muted" @selector="subtitle">...</div>
          </div>
        </div>
      </foreignObject>
    `;*/
    this.markup = util.svg `
      <foreignObject @selector="foreignObject" overflow="hidden" class="border border-2 rounded p-2 bg-body">
        <div class="d-flex px-2">
          <div class="p-1">
            <i class="" @selector="icon"></i>
          </div>
          <div class="align-self-center text-ellipsis px-2">
            <div class="text-ellipsis fw-bold" @selector="title">...</div>
          </div>
        </div>
      </foreignObject>
    `;
  }
}
