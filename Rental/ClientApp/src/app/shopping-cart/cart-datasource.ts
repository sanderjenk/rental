import { DataSource } from "@angular/cdk/table";
import { Observable, from } from "rxjs";
import { CartItem } from "./cart-item";
import { ShoppingCartService } from "./shopping-cart.service";
import { map, mergeAll, mergeMap } from 'rxjs/operators';
import { EquipmentService } from "../equipment.service";

export class CartDataSource extends DataSource<any> {
  constructor(
    private shoppingCartService: ShoppingCartService) {
    super();
  }
  connect(): Observable<any> {
    return this.shoppingCartService.getCalculatedShoppingCart().pipe(map((x: any) => x.cartItems));
  }

  disconnect() { }
}