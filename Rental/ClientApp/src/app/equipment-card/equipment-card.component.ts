import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-equipment-card',
  templateUrl: './equipment-card.component.html',
  styleUrls: ['./equipment-card.component.css']
})
export class EquipmentCardComponent implements OnInit {
  @Input() equipment = {
    name: "kamaz",
    type: "heavy",
    image: "http://rusautonews.com/wp-content/uploads/2019/06/KAMAZ-plans-to-launch-the-mass-assembly-of-trucks-at-its-factory-in-Vietnam-800x500_c.jpg"
  }
  constructor() { }

  ngOnInit(): void {
  }

}
