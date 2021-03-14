import { Component, OnInit } from '@angular/core';
import { CartDataSource } from './cart-datasource';
import { ShoppingCartService } from './shopping-cart.service';

@Component({
  selector: 'app-shopping-cart',
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.css']
})
export class ShoppingCartComponent implements OnInit {
  displayedColumns: string[] = ['name', 'type', 'days'];

  dataSource = new CartDataSource(this.shoppingCartService);

  shoppingCartEmpty: boolean;
  
  constructor(private shoppingCartService: ShoppingCartService) { }

  ngOnInit(): void {
    this.shoppingCartService.cartItemsObs.subscribe(cart => this.shoppingCartEmpty = cart.length == 0);
  }

  discardShoppingCart() {
    this.shoppingCartService.updateCart([]);
    this.dataSource = null;
  }

  generateInvoice() {
    this.shoppingCartService.generatePdf();
  }
}
