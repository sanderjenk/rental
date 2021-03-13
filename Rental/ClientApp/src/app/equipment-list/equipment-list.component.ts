import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { EquipmentService } from '../equipment.service';
import { ShoppingCartService } from '../shopping-cart/shopping-cart.service';
import { Equipment } from './equipment';

@Component({
  selector: 'app-equipment-list',
  templateUrl: './equipment-list.component.html',
  styleUrls: ['./equipment-list.component.css'],
  providers: [EquipmentService]
})
export class EquipmentListComponent implements OnInit {
  equipments: any;
  constructor(
    private equipmentService: EquipmentService,
    private shoppingCartService: ShoppingCartService,
    private _snackBar: MatSnackBar
    ) { }

  ngOnInit(): void {
    this.getEquipments();
  }

  getEquipments() {
    this.equipmentService.getEquipments()
    .subscribe(items => this.equipments = items, error => console.log(error));
  }

  addToCart(equipment: Equipment, days: string) {
    const parsedDays = parseInt(days);
    if (!days || days == "") {
      this.openSnackBar("\"Days\" is required", "Dismiss");
      return
    }
    if (isNaN(parsedDays)) {
      this.openSnackBar("Not a valid number", "Dismiss");
      return
    } 
    if (parsedDays < 1) {
      this.openSnackBar("\"Days\" can't be negative", "Dismiss");
    }
    else {
      this.shoppingCartService.addItemToCart({equipment: equipment, days: parsedDays})
      this.openSnackBar("Equipment added to cart", "Dismiss");
    }
  }

  openSnackBar(message: string, action: string) {
    this._snackBar.open(message, action, {
      duration: 2000,
    });
  }
}
