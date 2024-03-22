import { Type } from '@angular/core';

export interface ActivityDefinition {
  group: string;
  key: string;
  description?: string;
  version?: string;

  component?: Type<any>
}
