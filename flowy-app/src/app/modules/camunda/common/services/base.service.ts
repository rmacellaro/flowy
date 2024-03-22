import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CamundaBaseService {

  public baseApi: string;

  constructor(
    public http: HttpClient
  ) {
    this.baseApi = 'http://localhost:5111/';
  }
}
