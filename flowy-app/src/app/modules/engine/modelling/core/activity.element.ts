import { dia, elementTools, util } from '@joint/core';
import { Activity } from '../models/activity.model';

export class ActivityElement extends dia.Element {

  public static DefaultWidth: number = 180;
  public static DefaultHeight: number = 30;

  public Id: number;
  public IdNode: number;
  public Title: string;
  public Icon: string;
  public Outputs: Array<number> = [];
  public Activity: Activity;

  constructor(activity: Activity) {
    super({
      z: 20
    });

    this.Id = 0;
    this.IdNode = 0;
    this.Title = 'Node';
    this.Icon = 'bi bi-tag';
    this.Activity = activity;

    this.UpdateActivity(activity);
  }

  UpdateActivity(activity: Activity): void {
    this.Activity = activity;
    this.Id = activity.id ?? 0;
    this.IdNode = activity.idNode ?? 0;
    this.Icon = 'bi bi-tag';
    this.Title = '' + activity.key;

    activity.datas?.forEach((config) => {
      if (config.name == 'Activity.Base.Icon' && config.value){ this.Icon = config.value; }
      if (config.name == 'Activity.Base.Title' && config.value){ this.Title = config.value; }
    });

    this.attr('title', { html: this.Title });
    this.attr('icon', { class: this.Icon });
  }

  Select(paper: dia.Paper): void {
    this.findView(paper).addTools(new dia.ToolsView({
      tools: [
        new elementTools.Boundary({
          padding: 2,
          rotate: true,
          focusOpacity: 0.5,
          useModelGeometry: true,
          attributes: {
            stroke: '#ffc107',
            fill: 'none',
            'stroke-dasharray': '0 4 0',
            'stroke-width' : '2',
            'stroke-opacity' : '80%',
            'rx' : '5'
          }
        })
      ]
    }));
  }

  Unselect(paper: dia.Paper): void {
    this.findView(paper).hideTools();
  }

  override defaults() {
    return {
      ...super.defaults,
      type: "Activity",
      size: {
        width: ActivityElement.DefaultWidth,
        height: ActivityElement.DefaultHeight
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
    this.markup = util.svg `
      <foreignObject @selector="foreignObject" overflow="hidden" class="bg-body-tertiary rounded p-3">
        <div class="d-flex flex-row align-items-center w-100 h-100">
          <i class="bi bi-tag" @selector="icon"></i>
          <div class="text-ellipsis f-s-09 px-2" @selector="title">...</div>
        </div>
      </foreignObject>
    `;
  }
}
