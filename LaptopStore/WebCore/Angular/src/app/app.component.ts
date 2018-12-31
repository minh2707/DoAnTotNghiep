import { Component, AfterViewInit } from '@angular/core';
import { LocalStorageService, SessionStorageService } from 'ngx-webstorage';
import * as $ from 'jquery';

import { GioHangModel } from '../model/GioHangModel';
import { isNullOrUndefined } from 'util';

declare let Layout: any;

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements AfterViewInit {
  taikhoan = this.storage.retrieve('taikhoan');
  giohang = this.storage.retrieve('giohang');
  constructor(private storage: SessionStorageService) {

  }
  ngAfterViewInit(): void {

    $(document).ready(function () {
      Layout.init();
      Layout.initOWL();
      Layout.initImageZoom();
      Layout.initTouchspin();
      //Layout.initTwitter();

      Layout.initNavScrolling();
  });
  }

  xoasanphamtronggiohang = function (sanpham) {
    let sanphamtronggiohang: GioHangModel = this.storage.retrieve('giohang');
    let stt = null;

    if (!isNullOrUndefined(sanphamtronggiohang)) {
      sanphamtronggiohang.sanpham.forEach((p, i) => {
        if (p.id === sanpham.id) {
          sanphamtronggiohang.sanpham.splice(i, 1);
          this.giohang.sanpham.splice(i, 1);
        }
      });
    };
    this.storage.store('giohang', sanphamtronggiohang);

    this.tinhtoanlaitongtien(sanphamtronggiohang);
  };

  tinhtoanlaitongtien(sanphamtronggiohang): void {
    let tong = 0;

    if (!isNullOrUndefined(sanphamtronggiohang)) {
      if (sanphamtronggiohang.sanpham.length > 0) {
        sanphamtronggiohang.sanpham.forEach(sanpham => {
          tong += sanpham.gia * sanpham.soluong;
        });

        this.giohang.tongtien = tong;
      }
    }
  }

  dangxuat = function () {

  };

  if(taikhoan) {
    //$http.defaults.headers.common.Authorization = 'Bearer ' + $rootScope.taikhoan.token;
  }


}
