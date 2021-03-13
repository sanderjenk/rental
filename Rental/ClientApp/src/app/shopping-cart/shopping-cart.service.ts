import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { CartItem } from './cart-item';
import pdfMake from 'pdfmake/build/pdfmake';
import pdfFonts from 'pdfmake/build/vfs_fonts';
pdfMake.vfs = pdfFonts.pdfMake.vfs;

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
    let existingItem = currentCart.find(x => x.equipment.id == item.equipment.id);
    if (existingItem) {
      existingItem.days += item.days;
    } else {
      currentCart.push(item)
    }
    this.updateCart(currentCart);
  }

  public getInvoiceData() {
    const test = this.cartSubject.value.map(x => {
      return { equipmentId: x.equipment.id, days: x.days }
    })
    return this.http.post("https://localhost:44373/api/invoices", test);
  }

  public generatePdf() {
    this.getInvoiceData().subscribe(data => {
      const docDefinition = this.getDocDefinition(data);
      pdfMake.createPdf(docDefinition).open();
    });
  }

  public getDocDefinition(data) {
    let equipmentsTable: any[] = data.invoiceLines.map(x => [x.equipment.name, x.equipment.type, x.days, x.bonusPoints, x.price])
    
    equipmentsTable.unshift(["Name", "Type", "Days", "BonusPoints", "Price"])
    
    equipmentsTable.push(["", "", "", `Total: ${data.totalBonusPoints}`, `Total: ${data.totalPrice}`])
    
    const docDefinition = {
      content: [
        { text: 'Invoice', fontSize: 16, bold: true, style: 'header', margin: [0,0, 20, 20]},
        { text: "Date: " + new Date().toDateString(), alignment: "right"},
        { text: 'Renting Company - Your favourite company for renting stuff', fontSize: 12, style: 'subheader', margin: [0,0, 20, 20]},
        {
          style: 'tableExample',
          margin: [0,0,0,20],
          table: {
            body: equipmentsTable
          },
        },
        { text: 'Thank you for ordering', fontSize: 12, margin: [0,0, 0, 0]},
        { text: 'If you have any questions, then please contact renting.company@gmail.com ', fontSize: 12, margin: [0,0, 20, 20]},
      ]
    }
    return docDefinition;
  }

}
