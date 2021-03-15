import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { CartDataSource } from './cart-datasource';
import { ShoppingCartService } from './shopping-cart.service';

@Component({
  selector: 'app-shopping-cart',
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.css']
})
export class ShoppingCartComponent implements OnInit, OnDestroy {
  displayedColumns: string[] = ['name', 'type', 'days'];

  dataSource = new CartDataSource(this.shoppingCartService);

  shoppingCartEmpty: boolean;

  subscription: Subscription;
  
  constructor(private shoppingCartService: ShoppingCartService) { }
  
  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  ngOnInit(): void {
    this.subscription = this.shoppingCartService.cartItemsObs.subscribe(cart => this.shoppingCartEmpty = cart.length == 0);
  }

  discardShoppingCart() {
    this.shoppingCartService.updateCart([]);
    this.dataSource = null;
  }

  generateInvoice() {
    this.shoppingCartService.generatePdf();
  }
}
