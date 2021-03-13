import { Component, OnInit } from '@angular/core';
import { EquipmentService } from '../equipment.service';

@Component({
  selector: 'app-equipment-list',
  templateUrl: './equipment-list.component.html',
  styleUrls: ['./equipment-list.component.css'],
  providers: [EquipmentService]
})
export class EquipmentListComponent implements OnInit {
  equipments: any;
  constructor(private equipmentService: EquipmentService) { }

  ngOnInit(): void {
    this.getEquipments();
  }

  getEquipments() {
    this.equipmentService.getEquipments()
    .subscribe(items => this.equipments = items, error => console.log(error));
  }
}
