import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DonateListComponent } from './donate-list.component';

describe('DonateListComponent', () => {
  let component: DonateListComponent;
  let fixture: ComponentFixture<DonateListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DonateListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DonateListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
