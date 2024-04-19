export interface ActivityDecorator {
  version?: string;
}

export function Activity(params?: ActivityDecorator) {

  return function (target: any) {
    //console.log('[°] decorator ActivityDefinition:', params);
    target.ActivityDecorator = params;
    //Object.assign(target, { flowy: params}, params);
  }
}
