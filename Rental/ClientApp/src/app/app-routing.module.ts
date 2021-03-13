import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EquipmentListComponent } from './equipment-list/equipment-list.component';
import { ShoppingCartComponent } from './shopping-cart/shopping-cart.component';

const routes: Routes = [
  { path: 'equipments', component: EquipmentListComponent },
  { path: 'cart', component: ShoppingCartComponent },
  { path: '', pathMatch: "full", redirectTo: "equipments" },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
