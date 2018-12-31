import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {SanphamService} from '../sanpham.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { isUndefined, isNullOrUndefined } from 'util';
import { SessionStorageService } from 'ngx-webstorage';


@Component({
  selector: 'app-sanpham',
  templateUrl: './sanpham.component.html',
  styleUrls: ['./sanpham.component.css']
})
export class SanphamComponent implements OnInit {
  idSanPham:string;
  soluongspthemvaogiohang:number = 1;
  chitietsanpham = {
    soluong: 1
  };

  constructor(
    private sanphamService:SanphamService,
    private route: ActivatedRoute,
    private storage: SessionStorageService
  ) { }

  ngOnInit() {
    this.idSanPham = this.route.snapshot.paramMap.get('id');
    this.laySanPhamChiTiet();
  }

  laySanPhamChiTiet(): void{
    if (this.idSanPham && this.idSanPham != '') {
      this.sanphamService.laysanphamchitiet(this.idSanPham)
      .subscribe(ketqua => {
        this.chitietsanpham = ketqua;
        this.chitietsanpham.soluong = 1;
      }); 
    }
  }

  themvaogiohang(sanpham): void {
    let sanphamtronggiohang = this.storage.retrieve('giohang');

    if (isNullOrUndefined(sanphamtronggiohang)) {
      /* 
        Khi chua co san pham nao trong gio hang
        Thi them 1 san pham vao
        Va Tong Tien Se La: Gia Cua San Pham Nhân Với Số Lượng
      */

      sanpham.soluong = 1;
      sanphamtronggiohang = {
        sanpham: [sanpham],
        tongtien: sanpham.gia
      }
    } else {
      /* Trường Hợp:Đã có sản phẩm trong giỏ hàng*/
      let truyvan = [];
      /* Sử dụng id của sản phẩm để kiểm tra có bị trùng lắp sản phẩm ko. */
      sanphamtronggiohang.sanpham.forEach(item => {
        if (item.id == sanpham.id) {
          truyvan.push(item);
        }
      });

      if (truyvan && truyvan.length !== 0) {
        /* Nếu trùng thì tăng số lượng và tổng tiền */
        sanphamtronggiohang.sanpham.forEach(p => {
          if (p.id === sanpham.id) {
            if (sanpham.soluong > 1) {
              p.soluong = p.soluong + sanpham.soluong;
              sanphamtronggiohang.tongtien += p.gia * p.soluong;
            } else {
              p.soluong++;
              sanphamtronggiohang.tongtien += p.gia;
            }

          }
        });
      } else {
        /* Không trùng thì thêm sản phẩm đó vào */
        if (sanpham.soluong == null || isUndefined(sanpham.soluong)) {
          sanpham.soluong = 1;
        }
        sanphamtronggiohang.sanpham.push(sanpham);
        sanphamtronggiohang.tongtien = sanphamtronggiohang.tongtien + (sanpham.gia * sanpham.soluong);
      }

    }
    this.storage.store('giohang', sanphamtronggiohang);
  }

}
