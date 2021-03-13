import { Component, OnInit } from '@angular/core';
import { CartDataSource } from './cart-datasource';
import { ShoppingCartService } from './shopping-cart.service';

@Component({
  selector: 'app-shopping-cart',
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.css']
})
export class ShoppingCartComponent implements OnInit {
  displayedColumns: string[] = ['name', 'type', 'days', 'price'];

  dataSource = new CartDataSource(this.shoppingCartService);

  constructor(private shoppingCartService: ShoppingCartService) { }

  ngOnInit(): void {
  }

  discardShoppingCart() {
    this.shoppingCartService.updateCart([]);
    this.dataSource = null;
  }
}
