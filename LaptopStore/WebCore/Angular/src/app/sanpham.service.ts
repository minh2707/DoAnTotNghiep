import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable, of } from 'rxjs';
import {tap, map,catchError, finalize} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class SanphamService {
  DEFAULT_API: string = 'http://localhost:49595/api/';
  api: string;

  constructor(private httpClient: HttpClient) { 
  }

  laytatcasanpham(dulieu?:any): Observable<any>{
    let api = this.DEFAULT_API + 'LayVaTimSanPham/?';

    if (dulieu) {
      if (dulieu.idLoai) {
        api = api + '&IdLoai=' + dulieu.idLoai;
      }

      if (dulieu.giatu) {
        api = api + '&GiaTu=' + dulieu.giatu;
      }

      if (dulieu.giaden) {
        api = api + '&GiaDen=' + dulieu.giaden;
      }

      if (dulieu.giamgia) {
        api = api + '&GiamGia=' + dulieu.giamgia;
      }

      if (dulieu.trangso) {
        api = api + '&TrangSo= ' + dulieu.trangso;
      }
    }

    return this.httpClient.get(api)
    .pipe(
      tap(_ => console.log('Dang Lay Tat Ca San Pham')),
      catchError((err,  result?: any): Observable<any> => {
        return of(result);
      })
    );
  }

  laynamsanphammoinhat(): Observable<any>{
    let api = this.DEFAULT_API;
    api += 'SanPhamMoiNhat';
  
    return this.httpClient.get(api);
  }

  laytatcaloaisanpham():Observable<any>{
    let api = this.DEFAULT_API;
    api += 'LayHetLoaiSanPham';

    return this.httpClient.get(api);
  }

  demsanpham():Observable<any>{
    let api = this.DEFAULT_API;
    api += 'DemTatCaSanPham';

    return this.httpClient.get(api);
  }

  laysanphamchitiet(idsanpham:String):Observable<any>{
    let api = this.DEFAULT_API;
    api += 'LayMotSanPham/' + idsanpham;

    return this.httpClient.get(api);
  }

  laygiatiennhonhat():Observable<any>{
    let api = this.DEFAULT_API;
    api += 'LayGiaThapNhat';

    return this.httpClient.get(api);
  }

  laygiatienlonnhat():Observable<any>{
    let api = this.DEFAULT_API;
    api += 'LayGiaCaoNhat';

    return this.httpClient.get(api);
  }

  taokhachhang(kh:any):Observable<any>{
    let api = this.DEFAULT_API;
    api += 'TaoKhachHang';

    return this.httpClient.post(api, kh);
  }

  taohoadon(hoadon:any):Observable<any>{
    let api = this.DEFAULT_API;
    api += 'TaoDonHang';

    return this.httpClient.post(api, hoadon);
  }

  taochitietdonhang(ctdh:any):Observable<any>{
    let api = this.DEFAULT_API;
    api += 'TaoChiTietDonHang';

    return this.httpClient.post(api, ctdh);
  }

  timkiemsanpham(ten:string):Observable<any>{
    let api = this.DEFAULT_API;
    api += 'TimKiemSanPhamTheoTen' +'?tensanpham=' + ten;

    return this.httpClient.get(api);
  }
}