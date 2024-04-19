import { Data } from '../models/data.model';
import { ActivityData } from '../../modelling/models/activity-data.model';
import { NodeData } from '../../modelling/models/node-data.model';

export class DataHelper {

  constructor() {}

  public findOrDefault<T extends NodeData | ActivityData>(configs: Array<T>, name: string): T | undefined {
    return configs.find(c => c.name == name);
  }

  /*public updateOrAdd<T extends NodeData | ActivityData>(configs: Array<T>, name: string, value: any){
    var find: T | undefined = this.findOrDefault<T>(configs, name);
    if (!find) { find = { }}
  }*/

  /*public findOrAdd<T extends NodeData | ActivityData>(
    configs: Array<T>,
    name: string,
    value: any = undefined
  ): T {
    //var configType: new () => T;
    var find = this.findOrDefault(configs, name);
    if (!find) {
      //console.log('not found');
      find = { name: name, value: this.anyToString(value) };
      configs.push(find as T);
    }
    return find as T;
  }*/

  /*static setNodeConfig(node: Node, name: string, value: any): NodeConfig | undefined {
    if (!node.configs) { return;}
    if (value == null || value == undefined) { return; }
    var config = node.configs.find(nc => nc.name == name);
    if (config) { config.value = this.anyToString(value); }
    else {
      config = {
        idNode: node.id,
        name: name,
        value: this.anyToString(value)
      };
      node.configs.push(config);
    }
    return config;
  }*/

  public anyToString(value: any): string {
    if (value == null || value == undefined) { return ''; }
    if (value != null && value != undefined && value.constructor.name === 'Object'){ return JSON.stringify(value);}
    return value.toString();
  }
}
