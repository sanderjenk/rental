import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
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
  length = 100;
  pageSize = 3;
  pageNumber =1;
  pageSizeOptions: number[] = [3, 6, 9];

  pageEvent: PageEvent;

  equipments: any[];
  constructor(
    private equipmentService: EquipmentService,
    private shoppingCartService: ShoppingCartService,
    private _snackBar: MatSnackBar
    ) { }

  ngOnInit(): void {
    this.getEquipments();
  }

  getEquipments() {
    const parameters = {
      pageNumber: this.pageNumber, 
      pageSize: this.pageSize
    }
    this.equipmentService.getEquipments(parameters)
    .subscribe((resp: any) => {
      const paginationHeader = JSON.parse(resp.headers.get("X-Pagination"));
      this.length = paginationHeader.totalCount;
      this.equipments = resp.body; 
    });
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
      duration: 1000,
    });
  }

  pageChanged($event) {
    console.debug($event)
    this.pageSize = $event.pageSize;
    this.pageNumber = $event.pageIndex + 1;
    this.getEquipments();
  }
}
