import { dia, util } from '@joint/core';

export class LinkElement extends dia.Link {

  override defaults() {

    var from: string = '';
    var to: string = '';

    return {
      ...super.defaults,
      type: 'ngv.NodeLink',
      bidirectional: false,
      labels: [{
        attrs:{ text: { text: from }},
        position: { distance: 0.15 }
      },{
        attrs:{ text: { text: to }},
        position: { distance: 0.80 }
      }],
      attrs: {
        line: {
          connection: true,
          stroke: '#666666',
          strokeWidth: 2,
          strokeLinejoin: 'round',
          targetMarker: {
            markup: util.svg`
              <path d="M 8 -3 -1 0 8 3 z" stroke-linejoin="round" />
            `
          },
        },
        wrapper: {
          connection: true,
          strokeWidth: 10,
          strokeLinejoin: 'round',
        },
      },
    };

    /*// tool
    var InfoButton = joint.linkTools.Button.extend({
      name: 'info-button',
      attrs: {
        label: { html: 'xxx'}
      },
      options: {
        markup: joint.util.svg`
          <foreignObject width="30" height="20" overflow="hidden" class="rounded f-s-08 bg-primary position-absolute">
            <div class="d-flex flex-row align-items-center w-100 h-100 px-2 text-white">
              <div @selector="label" class="text-ellipsis f-s-08">..</div>
            </div>
          </foreignObject>`,
        distance: 0,
        offset: 0,
        action: (evt: any) => {
          console.log(evt);
        }
      }
    });

    var infoButton = new InfoButton({
      attrs: {
        label: { html: 'xxx'}
      }
    });
    var toolsView = new joint.dia.ToolsView({
        tools: [infoButton]
    });
    var linkView = linkElement.findView(this.Paper);
    linkView.addTools(toolsView);*/
  }

  override preinitialize(...args: any[]): void {
    super.preinitialize(...args);
    this.markup = util.svg`
      <path @selector="wrapper" fill="none" cursor="pointer" stroke="transparent" stroke-linecap="round"/>
      <path @selector="line" fill="none" pointer-events="none" />
    `;
  }

  toBidirectional(opt?: dia.Cell.Options) {
    this.prop(
      {
        bidirectional: true,
        attrs: {
          line: {
            sourceMarker: {
              markup: util.svg`
                <path d="M 8 -3 -1 0 8 3 z" stroke-linejoin="round" />
              `
            },
          },
        },
      },
      opt
    );
  }

  isBidirectional(): boolean {
    return Boolean(this.get('bidirectional'));
  }

  static isNodeLink(cell: dia.Cell): cell is LinkElement {
    // return cell.get('type') === 'ngv.NodeLink';
    return cell instanceof LinkElement;
  }
}
