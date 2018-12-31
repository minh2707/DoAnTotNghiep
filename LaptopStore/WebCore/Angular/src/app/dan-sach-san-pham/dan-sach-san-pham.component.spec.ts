import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DanSachSanPhamComponent } from './dan-sach-san-pham.component';

describe('DanSachSanPhamComponent', () => {
  let component: DanSachSanPhamComponent;
  let fixture: ComponentFixture<DanSachSanPhamComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DanSachSanPhamComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DanSachSanPhamComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
