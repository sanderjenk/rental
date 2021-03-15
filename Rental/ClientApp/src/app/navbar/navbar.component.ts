import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { ShoppingCartService } from '../shopping-cart/shopping-cart.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit, OnDestroy {
  cartCount = 0;
  subscription: Subscription;
  constructor(private shoppingCartService: ShoppingCartService) { }

  ngOnInit(): void {
    this.subscription = this.shoppingCartService.cartItemsObs.subscribe(items => {
      this.cartCount = items.length;
    });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

}
