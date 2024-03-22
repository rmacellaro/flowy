import { Draft } from './draft.model';

export class DraftTrack {

  public id?: number;
  public idDraft?: number;
  public draft?: Draft;
  public eventAt?: Date;
  public userIdentifier?: string;
  public operation?: string;
  public description?: string;
  public schemaBackup?: string;

}