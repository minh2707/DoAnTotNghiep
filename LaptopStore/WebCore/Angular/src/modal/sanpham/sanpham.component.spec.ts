import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { SanphamModalComponent } from './sanpham.component';


describe('SanphamModalComponent', () => {
  let component: SanphamModalComponent;
  let fixture: ComponentFixture<SanphamModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SanphamModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SanphamModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
