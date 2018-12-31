import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { SanphamService } from 'src/app/sanpham.service';


@Component({
  selector: 'app-sanpham',
  templateUrl: './sanpham.component.html',
  styleUrls: ['./sanpham.component.css']
})
export class SanphamModalComponent implements OnInit {
  idSanPham:string;
  soluongspthemvaogiohang:number = 1;
  sanphamchitiet:any;

  constructor(
    public dialogRef: MatDialogRef<SanphamModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any

    ) { }

  ngOnInit() {
    this.sanphamchitiet = this.data.sanpham;
    this.sanphamchitiet.soluong = 1;
    console.log(this.sanphamchitiet)
  }
}
