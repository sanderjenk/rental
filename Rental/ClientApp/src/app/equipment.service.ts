import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class EquipmentService {

  constructor(private http: HttpClient) { }

  public getEquipments() {
    return this.http.get("https://localhost:44373/api/equipments");
  }
  public getEquipment(id) {
    return this.http.get(`https://localhost:44373/api/equipments${id}`);
  }
}
