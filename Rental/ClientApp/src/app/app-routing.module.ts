import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EquipmentListComponent } from './equipment-list/equipment-list.component';
import { HomeComponent } from './home/home.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { ShoppingCartComponent } from './shopping-cart/shopping-cart.component';

const routes: Routes = [
  { path: 'equipments', component: EquipmentListComponent },
  { path: 'cart', component: ShoppingCartComponent },
  { path: '404', component: PageNotFoundComponent },
  { path: '', pathMatch: "full", component: HomeComponent },
  { path: '**', redirectTo: '/404' }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
