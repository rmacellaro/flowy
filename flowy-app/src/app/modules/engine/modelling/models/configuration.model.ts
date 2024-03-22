import { Interaction } from './interaction.model';

export class Configuration {
  public id?: number;
  public idInteraction?: number;
  public interaction?: Interaction;
  public type?: string;
  public name?: string;
  public value?: string;
}
