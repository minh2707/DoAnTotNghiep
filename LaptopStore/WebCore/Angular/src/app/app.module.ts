import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {NgxWebstorageModule} from 'ngx-webstorage';
import { FormsModule } from '@angular/forms';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatSliderModule,MatDialogModule,MatButtonModule, MatCheckboxModule, MatInputModule, MatTabsModule, MAT_DIALOG_DEFAULT_OPTIONS} from '@angular/material';
import { HttpClientModule }    from '@angular/common/http';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DanSachSanPhamComponent } from './dan-sach-san-pham/dan-sach-san-pham.component';
import { SanphamComponent } from './sanpham/sanpham.component';
import { SanPhamModalModule } from 'src/modal/sanpham/sanpham.module';

@NgModule({
  declarations: [
    AppComponent,
    DanSachSanPhamComponent,
    SanphamComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule,
    NgxWebstorageModule.forRoot(),
    SanPhamModalModule,
    MatSliderModule,MatButtonModule, MatCheckboxModule, MatInputModule,MatDialogModule,MatTabsModule
  ],
  providers: [
    {provide: MAT_DIALOG_DEFAULT_OPTIONS, useValue: {hasBackdrop: false}}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
