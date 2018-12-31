import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DanSachSanPhamComponent } from './dan-sach-san-pham/dan-sach-san-pham.component';
import {SanphamComponent} from './sanpham/sanpham.component';

const routes: Routes = [
  {path: 'danhsachsanpham' , component: DanSachSanPhamComponent},
  {path: 'sanpham/:id', component: SanphamComponent},
  { path: '', redirectTo: '/danhsachsanpham', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
