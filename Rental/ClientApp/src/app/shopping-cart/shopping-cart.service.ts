import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { CartItem } from './cart-item';

@Injectable({
  providedIn: 'root'
})
export class ShoppingCartService {
  private cartSubject: BehaviorSubject<CartItem[]> = new BehaviorSubject([]);
  public cartItemsObs = this.cartSubject.asObservable();
  constructor(private http: HttpClient) { }

  public updateCart(items: CartItem[]) {
    this.cartSubject.next(items);
  }

  public addItemToCart(item: CartItem) {
    let currentCart = this.cartSubject.value;
    let existingItem = currentCart.find(x => x.equipmentId == item.equipmentId);
    if (existingItem) {
      existingItem.days += item.days; 
    } else {
      currentCart.push(item)
    }
    this.updateCart(currentCart);
  }

  public getCalculatedShoppingCart() {
    return this.http.post("https://localhost:44373/api/shoppingCart", this.cartSubject.value);
  }
}
