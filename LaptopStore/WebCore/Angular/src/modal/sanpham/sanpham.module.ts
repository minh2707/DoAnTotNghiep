import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {NgxWebstorageModule} from 'ngx-webstorage';
import { FormsModule } from '@angular/forms';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatDialogModule,MatButtonModule, MatCheckboxModule, MatInputModule, MatTabsModule} from '@angular/material';
import { HttpClientModule }    from '@angular/common/http';


import { SanphamModalComponent } from './sanpham.component';

@NgModule({
  declarations: [
    SanphamModalComponent
  ],
  entryComponents: [
    SanphamModalComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule,
    NgxWebstorageModule.forRoot(),
    MatButtonModule, MatCheckboxModule, MatInputModule,MatDialogModule,MatTabsModule
  ],
  providers: [],
})
export class SanPhamModalModule { }
