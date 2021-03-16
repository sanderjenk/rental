import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EquipmentService {

  constructor(private http: HttpClient) { }

  public getEquipments(parameters) {
    return this.http.get(`${environment.apiUrl}equipments`, {params: parameters, observe: "response"});
  }
}
