import { Component, OnInit } from '@angular/core';
import {SanphamService} from '../sanpham.service';
import {Observable, concat} from 'rxjs';
import {MatDialog} from '@angular/material';
import {SanphamComponent} from '../sanpham/sanpham.component'
import { SanphamModalComponent } from 'src/modal/sanpham/sanpham.component';
import { isNullOrUndefined } from 'util';
import { CauTruyVanTimSanPham } from 'src/model/TruyVanTimSanPhamModel';

@Component({
  selector: 'app-dan-sach-san-pham',
  templateUrl: './dan-sach-san-pham.component.html',
  styleUrls: ['./dan-sach-san-pham.component.css']
})
export class DanSachSanPhamComponent implements OnInit {
  sanpham: any;
  sanphammoinhat: any = null;
  cautruyvandelocsanpham: CauTruyVanTimSanPham = new CauTruyVanTimSanPham();
  tongsosanpham:number = 0;
  thanhlocgia:any = {
      gialonnhat: 0,
      gianhonhat: 999999,
      tuychon: {
          buoc: 1000000
      }
  };
  sapxep:object;
  tukhoa:string;
  loaisanpham: any;
  giaduocchon:any;

  constructor(private sanPhamService: SanphamService,
    private dialog: MatDialog) { }

  ngOnInit() {
    this.laytatcasanpham();
    this.laynamsanphammoinhat();
    this.layhetloaisanpham();
    this.demtongsosanpham();
    this.laygialonnhatvanhonhat();
  }

  laytatcasanpham(): void {
    this.sanPhamService.laytatcasanpham()
    .subscribe(ketqua => {
      this.sanpham = ketqua;
      console.log(this.sanpham, '1');
    });
  }

  laynamsanphammoinhat(): void {
    this.sanPhamService.laynamsanphammoinhat()
    .subscribe(ketqua => {
      this.sanphammoinhat = ketqua;
      console.log(this.sanphammoinhat, '2');
    })
  }

  layhetloaisanpham(): void {
    this.sanPhamService.laytatcaloaisanpham()
    .subscribe(ketqua => {
      this.loaisanpham = ketqua;
      console.log(this.loaisanpham, '3');
    })
  }

  demtongsosanpham(): void {
    this.sanPhamService.demsanpham()
    .subscribe(ketqua => {
      this.tongsosanpham = ketqua;
    })
  }

  mosanphamchitietcuaso(sp) {
    const dialogRef = this.dialog.open(SanphamModalComponent,{
      width: '800px',
      data: {sanpham: sp}
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('Dong Dialog');
      
    });
  }

  timkiemsanpham(tukhoa): void {
    if (!isNullOrUndefined(tukhoa)) {
      this.sanPhamService.timkiemsanpham(tukhoa)
      .subscribe(ketqua => {
        this.sanpham = ketqua;
      })
    } else {
      this.laytatcasanpham();
    }
  }

  laygialonnhatvanhonhat(): void {
    let laygianhonhat = () => {
      this.sanPhamService.laygiatiennhonhat()
      .subscribe(ketqua => {
        this.thanhlocgia.gianhonhat = ketqua;
        laygiatienlonnhat(); 
      });
    };

    let laygiatienlonnhat = () => {
      this.sanPhamService.laygiatienlonnhat()
      .subscribe(ketqua => {
        this.thanhlocgia.gialonnhat =ketqua;
      })
    };

    laygianhonhat();
  }

  dinhdanggiatien(giatri: number | null) {
    if (!giatri) {
      return 0;
    }

    if (giatri >= 1000000) {
      return Math.round(giatri / 1000000) + 'M';
    }

    return giatri;
  }

  laysanphamtheoloai(idloai): void {
    this.cautruyvandelocsanpham.idLoai = idloai;
    this.sanPhamService.laytatcasanpham(this.cautruyvandelocsanpham)
    .subscribe(ketqua => {
      this.sanpham = ketqua;
    })
  }

  laysanphamtheogia(): void {
    this.cautruyvandelocsanpham.giaden = this.giaduocchon;
    this.cautruyvandelocsanpham.giatu = this.thanhlocgia.gianhonhat;

    this.sanPhamService.laytatcasanpham(this.cautruyvandelocsanpham)
    .subscribe(ketqua => {
      this.sanpham = ketqua;
    })
  }
}
